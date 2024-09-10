using UnityEngine;

namespace Collect.Core.Gameplay
{
    public interface IInfluenceTemperature
    {
        #region Properties
        #endregion

        #region Fields
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void RegisterSelfToTemperatureManager(TemperatureManager temperatureManager)
        {
            temperatureManager.RegisterContributor(this);
        }

        public void DeregisterSelfFromTemperatureManager(TemperatureManager temperatureManager)
        {
            temperatureManager.DeregisterContributor(this);
        }

        /// <summary>
        /// Gets the temperature of a temperature influencer, as well as a weight between 1 and 0 showing how strongly that temperature is influencing the temperature at our position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="temp"></param>
        /// <param name="distanceWeight"></param>
        /// <returns></returns>
        public bool TryGetTemperatureInfluenceAtPosition(Vector3 position, out float temp, out float distanceWeight);
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}