using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        Complete
    }
    #endregion

    #region Properties
    public AttributeTypes Type => _type;

    public Color Color => _color;
    public Material Material => _material;

    #endregion

    #region Fields
    [SerializeField] private AttributeTypes _type;
    [Space]
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Material _material;

    #endregion

    public bool Equals(AttributeTypes other)
    {
        return _type == other;
    }
}
