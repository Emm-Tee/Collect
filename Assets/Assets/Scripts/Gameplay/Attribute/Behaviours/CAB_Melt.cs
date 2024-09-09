using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Timer starts when player picks them up, and resets collectable if not collected by timer expiring
    /// </summary>
    public class CAB_Melt : CollectableAttributeBehaviour
    {
        #region Const
        #endregion

        #region Properties
        //Set timer here so colour changes too
        private float MeltValue
        {
            get => _meltValue;
            set
            {
                _meltValue = value;
                UpdateMeltAppearance();
            }
        }
        #endregion

        #region Fields

        [SerializeField] private float _maxMeltTime = 30f;

        private float _meltValue;
        private bool _canMelt;

        private Color _collectableColor;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public override void Initialize(Collectable collectable, GameManager gameManager)
        {
            base.Initialize(collectable, gameManager);

            _collectableColor = _collectable.AttributeMaterial.color;
        }
        public override void Activate()
        {
            base.Activate();
            _canMelt = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _canMelt = false;
        }

        public override void GetCollected()
        {
            base.GetCollected();
            MeltValue = 0;
        }

        public override void BeReleased()
        {
            base.BeReleased();
            MeltValue = 0;
        }

        protected override void AttributeConditionCompleted()
        {
            base.AttributeConditionCompleted();
            _canMelt = false;
        }

        protected override void AttributeConditionIncompleted()
        {
            base.AttributeConditionIncompleted();
            _canMelt = true;
        }

        public override void UpdateBehaviour()
        {
            if(_canMelt)
            {
                float currentTemp = _gameManager.TemperatureManager.GetTemperatureAtPosition(_collectable.transform.position);
                MeltValue += currentTemp * Time.deltaTime * _gameManager.TemperatureManager.MeltRate;
            }

            if(MeltValue >=  _maxMeltTime)
            {
                if (_collectable.IsHeld)
                {
                    CollectableEvents.AttemptReleaseCollectable?.Invoke(_collectable.CurrentHolder, _collectable);
                }
                _collectable.TotalReset();
            }
        }

        public override void Reset()
        {
            _canMelt = true;
            MeltValue = 0;

        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void UpdateMeltAppearance()
        {
            float percent = 1 - (MeltValue / _maxMeltTime);

            Color lerpedColor = Color.Lerp(Color.white, _collectableColor, percent);

            float size = Mathf.Lerp(.2f, 1f, percent);

            _collectable.AttributeMaterial.color = lerpedColor;
            _collectable.transform.localScale = Vector3.one * size;
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}