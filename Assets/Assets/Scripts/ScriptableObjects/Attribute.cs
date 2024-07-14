using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : ScriptableObject
{
    #region Enums
    #endregion

    #region Properties
    public Color Color => _color;
    #endregion

    #region Fields
    [SerializeField] private Color _color = Color.white;
    #endregion
}
