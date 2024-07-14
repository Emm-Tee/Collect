using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionManager 
{
    #region Properties
    #endregion

    #region Fields
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public static bool CanCollect(Collectable collectable, IHoldCollectable repository)
    {
        return collectable.CanCollect(repository);
    }

    public static void CollectablePickedUp(Collectable collectable, IHoldCollectable repository)
    {
        if(collectable.CurrentHolder != null)
        {
            collectable.CurrentHolder.ReleaseCollectable(collectable);
        }

        repository.PickUpCollectable(collectable);

        collectable.GetPickedUp(repository);        
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
