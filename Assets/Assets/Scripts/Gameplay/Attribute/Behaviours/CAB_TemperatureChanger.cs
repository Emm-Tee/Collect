using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Collectable emits strong temperature in radius and once collected effects room's ambient temperature
    /// Temperature and radius defined in Attribute data set object
    /// </summary>
    public class CAB_TemperatureChanger : CollectableAttributeBehaviour, IInfluenceTemperature
    {
        #region Properties
        private float TemperatureRadius => _temperatureData.TemperatureRadius;
        private float CollectableTemperature => _temperatureData.PreCollectionTemperature;
        private float CompletedTemperature => _temperatureData.PostCollectionTemperature;
        private AnimationCurve TemperatureDropoffCurve => _temperatureData.TemperatureDropoffCurve;
        #endregion

        #region Fields
        private DS_TemperatureSO _temperatureData;

        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public override void Initialize(Collectable collectable, GameManager gameManager)
        {
            base.Initialize(collectable, gameManager);
            _temperatureData = _collectable.Attribute.DataSet as DS_TemperatureSO;
            Debug.Log($"Trying to get attribute data here and it is :: {_temperatureData != null}",this);
        }

        public override void Activate()
        {
            base.Activate();
            _gameManager.TemperatureManager.RegisterContributor(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _gameManager.TemperatureManager.DeregisterContributor(this);
        }

        //Whilst the collectable is active and not collected, it influences the temperature thusly
        public bool TryGetTemperatureInfluenceAtPosition(Vector3 position, out float maxTemp, out float distanceWeight)
        {
            distanceWeight = 0;
            maxTemp = _conditionComplete? CompletedTemperature : CollectableTemperature;

            float sqrDistance = (position - transform.position).sqrMagnitude;
            float sqrRadius = TemperatureRadius * TemperatureRadius;

            //too far away, not influencing temp
            if (!_conditionComplete && sqrDistance > sqrRadius)
            {
                return false;
            }

            float distPercent = Mathf.InverseLerp(0, sqrRadius, sqrDistance);
            distanceWeight = _conditionComplete? 1 : TemperatureDropoffCurve.Evaluate(distPercent);

            return true;
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