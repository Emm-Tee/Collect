using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class TemperatureManager : MonoBehaviour
    {
        #region Constants
        public static float MaxTemp = 50;
        public static float MinTemp = -20;

        private const float BaseTemp = 15f;
        #endregion

        #region Properties
        public float MeltRate => _meltRate;
        #endregion

        #region Fields
        private float _baseRoomTemperature;
        private float _currentTemperature;

        private List<IContributeToTemperature> _contributors = new List<IContributeToTemperature>();

        [SerializeField] private float _meltRate = 0.2f;

        [SerializeField] private float _debugTempMod = 0f;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void Initialise()
        {
            _currentTemperature = BaseTemp;
        }

        public void RegisterContributor(IContributeToTemperature contributor)
        {
            if (_contributors.Contains(contributor))
            {
                Debug.LogWarning($"Trying to registor temperature contributor that is already registered.");
                return;
            }

            _contributors.Add(contributor);
        }

        public void DeregisterContributor(IContributeToTemperature contributor)
        {
            if (!_contributors.Contains(contributor))
            {
                Debug.LogWarning($"Trying to deregistor temperature contributor that is not registered.");
                return;
            }

            _contributors.Remove(contributor);
        }
        public float GetTemperatureAtPosition(Vector3 position)
        {
            float tempAtPosition = _currentTemperature + _debugTempMod;
            foreach(IContributeToTemperature contributor in  _contributors)
            {
                tempAtPosition += contributor.GetTempModifierAtPosition(position);
            }
            return tempAtPosition;
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}

