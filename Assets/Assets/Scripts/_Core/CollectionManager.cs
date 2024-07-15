using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectionManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [HideInInspector] public UnityEvent<IHoldCollectable, Collectable> CollectablePickedUp;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
    #endregion

    #region Public Methods
    public static bool CanCollect(Collectable collectable, IHoldCollectable repository)
    {
        return collectable.CanCollect(repository);
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void SubscribeToEvents()
    {
        CollectablePickedUp.AddListener(OnCollectablePickedUp);
    }

    private void UnsubscribeToEvents()
    {
        CollectablePickedUp.RemoveAllListeners();
    }
    #endregion

    #region Event Callbacks
    private void OnCollectablePickedUp(IHoldCollectable repository, Collectable collectable)
    {
        if (collectable.CurrentHolder != null)
        {
            collectable.CurrentHolder.ReleaseCollectable(collectable);
        }

        repository.PickUpCollectable(collectable);

        collectable.GetPickedUp(repository);
    }
    #endregion
}
