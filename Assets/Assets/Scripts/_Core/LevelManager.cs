using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private CollectionManager _collectionManager;

    [SerializeField] private Interactable[] _interactables;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        InitializeInteractables();
    }
    #endregion

    #region Public Methods

    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void InitializeInteractables()
    {
        foreach(Interactable interactable in _interactables)
        {
            interactable.Initialise(_collectionManager);
        }
    }
    #endregion

    #region Event Callbacks
    #endregion
}
