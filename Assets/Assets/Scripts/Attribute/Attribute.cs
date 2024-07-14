using System.Collections;
using System.Collections.Generic;
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
        Complete
    }
    #endregion

    #region Properties
    public AttributeTypes Type => _type;

    public Color Color => _color;
    #endregion

    #region Fields
    [SerializeField] private AttributeTypes _type;
    [Space]
    [SerializeField] private Color _color = Color.white;
    #endregion
}
