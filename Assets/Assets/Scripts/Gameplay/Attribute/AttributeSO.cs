using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Scriptable object for attribute which holds editor set data and behaviour
    /// </summary>

    [CreateAssetMenu(fileName = "Att_", menuName = "Scriptable Objects/ Attribute")]
    public class AttributeSO : ScriptableObject
    {
        #region Enums
        public enum AttributeTypes
        {
            Base = 0,
            Red,
            Blue,
            Speed,
            Light,
            Melt,
            Thermometer,
            TemperatureChanger,
            Key,
            Strength,
            Completer
        }
        #endregion

        #region Properties
        public AttributeDataSetSO DataSet => _dataSet;
        public AttributeTypes Type => _type;
        public IHoldCollectable.HoldingType HoldingType => _holdingType;

        public Color Color => _color;
        public Material Material => _material;
        #endregion

        #region Fields
        [SerializeField] private AttributeDataSetSO _dataSet;
        [Space]
        [SerializeField] private AttributeTypes _type;
        [SerializeField] private IHoldCollectable.HoldingType _holdingType;
        [Space]
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Material _material;

        #endregion

        public bool Equals(AttributeTypes other)
        {
            return _type == other;
        }
    }
}
