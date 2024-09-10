using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    [CreateAssetMenu(fileName = "DS_", menuName = "Scriptable Objects/ Data Sets/ TemperatureCollection")]
    public class DS_TemperatureSO : AttributeDataSetSO
    {
        #region Properties
        public AnimationCurve TemperatureDropoffCurve => _temperatureDropoffCurve;
        public float TemperatureRadius => _temperatureRadius;

        public float PreCollectionTemperature => _preCollectionTemperature;
        public float PostCollectionTemperature => _postCollectionTemperature;
        #endregion

        #region Fields
        [SerializeField] private AnimationCurve _temperatureDropoffCurve;
        [SerializeField] private float _temperatureRadius;

        [Space]

        [SerializeField] private float _preCollectionTemperature;
        [SerializeField] private float _postCollectionTemperature;
        #endregion
    }
}
