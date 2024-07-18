using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region Properties
    public Attribute Attribute => _attribute;
    #endregion

    #region Fields
    [SerializeField] protected Attribute _attribute;

    [Space]

    [Header("Appearance")]
    [SerializeField] private MeshRenderer[] _decoratedRenderers;

    [Space]

    private Material _decorationMaterial;

    protected CollectionManager _collectionManager;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        InitialiseAppearance();
    }
    #endregion

    #region Public Methods
    public void Initialise(CollectionManager collectionManager)
    {
        _collectionManager = collectionManager;
    }

    public void SetAttribute(Attribute attribute)
    {
        _attribute = attribute;
        InitialiseAppearance();
    }
    #endregion

    #region Protected Methods
    protected void InitialiseAppearance()
    {
        _decorationMaterial = new Material(_attribute.Material)
        {
            color = _attribute.Color
        };

        foreach ( MeshRenderer renderer in _decoratedRenderers)
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
