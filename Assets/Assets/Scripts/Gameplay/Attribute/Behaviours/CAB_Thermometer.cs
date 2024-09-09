using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Once collected, the thermometer behaviour activates and colour changes based on current temp in location
    /// </summary>

    public class CAB_Thermometer : CollectableAttributeBehaviour
    {
        #region Properties
        private float CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                if (_currentTemperature == value)
                {
                    return;
                }

                _currentTemperature = value;
                SetColor();
            }
        }

        #endregion

        #region Fields
        private Color[] _colorRange;

        private bool _canDetectTemperature;

        private float _currentTemperature;
        private float _goalTemperature;

        [SerializeField] private float _tempChangeSpeed = 6f;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public override void Initialize(Collectable collectable, GameManager gameManager)
        {
            base.Initialize(collectable, gameManager);

            DS_ColorSetSO colorSetData = _collectable.Attribute.DataSet as DS_ColorSetSO;
            _colorRange = colorSetData.ColorSet;
        }

        public override void Activate()
        {
            base.Activate();

            _canDetectTemperature = false;
            SetInactiveColor();
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _canDetectTemperature = false;
            SetInactiveColor();
        }

        protected override void AttributeConditionCompleted()
        {
            //Don't currently run base.ACC because we don't want to update appearance
            _canDetectTemperature = true;

            //Set color
            _goalTemperature = _gameManager.TemperatureManager.GetTemperatureAtPosition(_collectable.transform.position);
            CurrentTemperature = _goalTemperature;
            SetColor();
        }

        public override void UpdateBehaviour()
        {
            if (!_canDetectTemperature)
            {
                return;
            }

            _goalTemperature = _gameManager.TemperatureManager.GetTemperatureAtPosition(_collectable.transform.position);

            //If we're not at goal temperature, start moving towards it
            if(_goalTemperature == CurrentTemperature)
            {
                return;
            }
            float difference = _goalTemperature - CurrentTemperature;

            float sign = Mathf.Sign(difference);
            float delta = sign * _tempChangeSpeed * Time.deltaTime;

            if(Mathf.Abs(delta) > Mathf.Abs(difference))
            {
                CurrentTemperature = _goalTemperature;
            }
            else
            {
                CurrentTemperature += delta;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void SetInactiveColor()
        {
            _collectable.AttributeMaterial.color = Color.black;
        }

        private void SetColor()
        {
            //Get it as percentage of possible temp range
            float tempPercent = Mathf.InverseLerp(TemperatureManager.MinTemp, TemperatureManager.MaxTemp, CurrentTemperature);

            //map percent to a color + lerp value
            float colorPercent = (_colorRange.Length - 1) * tempPercent;

            int lowerColor = Mathf.FloorToInt(colorPercent);

            Color currentColor;
            if(colorPercent % 1.0 == 0)
            {
                currentColor = _colorRange[lowerColor];
            }
            else
            {
                currentColor = Color.Lerp(_colorRange[lowerColor], _colorRange[lowerColor + 1], colorPercent - lowerColor);
            }

            _collectable.AttributeMaterial.color = currentColor;
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}