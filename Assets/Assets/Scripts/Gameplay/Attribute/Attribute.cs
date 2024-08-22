using Collect.Core.Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "Att_", menuName = "Scriptable Objects/ Attribute")]
public class Attribute : ScriptableObject
{
    #region Enums
    public enum AttributeTypes
    {
        Base = 0,
        Red,
        Blue,
        Speed,
        Light,
        Key,
        Strength,
        Completer
    }
    #endregion

    #region Properties
    public AttributeTypes Type => _type;
    public IHoldCollectable.HoldingType HoldingType => _holdingType;

    public Color Color => _color;
    public Material Material => _material;
    #endregion

    #region Fields
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
