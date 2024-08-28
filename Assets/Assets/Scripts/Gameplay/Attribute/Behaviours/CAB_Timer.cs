using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Timer starts when player picks them up, and resets collectable if not collected by timer expiring
    /// </summary>
    public class CAB_Timer : CollectableAttributeBehaviour
    {
        #region Properties
        //Set timer here so colour changes too
        private float TimerValue
        {
            get => _timerValue;
            set
            {
                _timerValue = value;
                UpdateTimerAppearance();
            }
        }
        #endregion

        #region Fields
        [SerializeField] private float _totalTime = 5f;

        private float _timerValue;
        private bool _heldByPlayer;

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

        public override void GetCollected()
        {
            base.GetCollected();
            _heldByPlayer = _collectable.CurrentHolder.GetIsPlayer();
            TimerValue = 0;
        }

        public override void BeReleased()
        {
            base.BeReleased();
            _heldByPlayer = false;
            TimerValue = 0;
        }

        public override void UpdateBehaviour()
        {
            if(_heldByPlayer)
            {
                TimerValue += Time.deltaTime;
            }

            if(TimerValue >=  _totalTime)
            {
                CollectableEvents.AttemptReleaseCollectable?.Invoke(_collectable.CurrentHolder, _collectable);
                _collectable.TotalReset();
            }

            
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void UpdateTimerAppearance()
        {
            float percent = 1 - (TimerValue / _totalTime);

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