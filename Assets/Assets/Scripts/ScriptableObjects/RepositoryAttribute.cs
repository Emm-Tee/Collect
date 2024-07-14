using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RA_", menuName = "Attributes/Repository")]
public class RepositoryAttribute : Attribute
{
    #region Enums
    public enum RepositoryAttributes
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
    public RepositoryAttributes Type => _type;
    #endregion

    #region Fields
    [SerializeField] private RepositoryAttributes _type;
    #endregion
}
