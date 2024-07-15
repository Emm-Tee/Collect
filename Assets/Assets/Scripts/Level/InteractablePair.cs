using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePair : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] Interactable[] _interactables;
    [SerializeField] Attribute _attribute;
    #endregion

    #region Unity Methods
    private void OnDrawGizmos()
    {
        if (_interactables.Length < 2)
        {
            return;
        }

        for (int i = 1; i < _interactables.Length; i++)
        {
            Debug.DrawLine(_interactables[i - 1].transform.position, _interactables[i].transform.position);
        }
    }
    #endregion

    #region Public Methods
    public void InitialisePairing(CollectionManager collectionManager)
    {
        foreach(Interactable interactable in _interactables)
        {
            interactable.SetAttribute(_attribute);
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
