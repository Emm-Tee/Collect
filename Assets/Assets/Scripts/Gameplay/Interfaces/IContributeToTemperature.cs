using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public interface IContributeToTemperature
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

        public float GetTempModifierAtPosition(Vector3 position);
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}