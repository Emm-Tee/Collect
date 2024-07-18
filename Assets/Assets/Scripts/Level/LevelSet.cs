using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet : MonoBehaviour
{
    #region Properties
    public bool LevelComplete
    {
        get
        {
            foreach (InteractablePair pair in _interactablePairings)
            {
                if (!pair.IsMatched)
                {
                    return false;
                }
            }
            return true;
        }
    }
    #endregion

    #region Fields
    [SerializeField] private InteractablePair[] _interactablePairings;
    [SerializeField] private Interactable[] _soloInteractables;
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public void Initialise(CollectionManager collectionManager)
    {
        foreach(InteractablePair pair in  _interactablePairings)
        {
            pair.InitialisePairing(collectionManager);
        }
        foreach (Interactable interactable in _soloInteractables)
        {
            interactable.Initialise(collectionManager);
        }
    }

    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
