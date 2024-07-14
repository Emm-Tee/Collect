using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CA_", menuName = "Attributes/Collectable")]
public class CollectableAttribute : Attribute
{
    #region Enums
    public enum CollectableAttributes
    {
        Base = 0,
        Speed,
        Light,
        Key,
        Strength,
        NextLevel
    }
    #endregion

    #region Properties
    public CollectableAttributes Type => _type;
    #endregion

    #region Fields

    [SerializeField] private CollectableAttributes _type;
    #endregion
}
