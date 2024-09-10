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
        private float _ambiantTemperature;

        private List<IInfluenceTemperature> _contributors = new List<IInfluenceTemperature>();

        [SerializeField] private float _meltRate = 0.2f;

        [SerializeField] private float _debugTempMod = 0f;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void Initialise()
        {
            _ambiantTemperature = BaseTemp;
        }

        public void RegisterContributor(IInfluenceTemperature contributor)
        {
            if (_contributors.Contains(contributor))
            {
                Debug.LogWarning($"Trying to registor temperature contributor that is already registered.");
                return;
            }

            _contributors.Add(contributor);
        }

        public void DeregisterContributor(IInfluenceTemperature contributor)
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
            //Temperatures with greater weights have greater influence on the current temperature, achieved by adding it to the value to be averaged more times per weight

            float tempAtPosition = _ambiantTemperature;

            foreach (IInfluenceTemperature contributor in _contributors)
            {
                if(contributor.TryGetTemperatureInfluenceAtPosition(position, out float temperature, out float distanceWeight))
                {
                    float tempDelta = temperature - _ambiantTemperature;

                    float tempInfluence = tempDelta * distanceWeight;

                    tempAtPosition += tempInfluence;
                }
            }

            return tempAtPosition + _debugTempMod;
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

