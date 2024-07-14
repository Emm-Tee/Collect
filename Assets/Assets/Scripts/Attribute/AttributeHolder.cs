using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeHolder : MonoBehaviour
{
    #region Properties
    public Attribute Attribute => _attribute;
    #endregion

    #region Fields
    [SerializeField] protected Attribute _attribute;

    [Space]

    [Header("Appearance")]
    [SerializeField] private MeshRenderer[] _decoratedRenderers;
    [SerializeField] private Material _materialPrefab;

    [Space]

    private Material _decorationMaterial;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        InitialiseAppearance();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    protected void InitialiseAppearance()
    {
        _decorationMaterial = new Material(_materialPrefab);

        _decorationMaterial.color = _attribute.Color;

        foreach( MeshRenderer renderer in _decoratedRenderers)
        {
            renderer.material = _decorationMaterial;
        }
    }
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
