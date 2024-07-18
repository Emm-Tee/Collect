using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectionManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private AttributeBehaviourManager _behaviourManager;
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
        Events.AttemptAtPickUp += OnAttemptPickup;

        Events.PickUpComplete += OnPickUpComplete;
    }

    private void UnsubscribeToEvents()
    {
        Events.AttemptAtPickUp -= OnAttemptPickup;
    }
    #endregion

    #region Event Callbacks
    private void OnAttemptPickup(IHoldCollectable repository, Collectable collectable)
    {
        if (collectable.CurrentHolder != null)
        {
            collectable.CurrentHolder.ReleaseCollectable(collectable);
        }

        repository.PickUpCollectable(collectable);

        collectable.GetPickedUp(repository);
    }

    private void OnPickUpComplete(IHoldCollectable repository, Collectable collectable)
    {
        if (repository.GetAttribute().Equals(collectable.Attribute.Type))
        {
            _behaviourManager.StartCompleteBehaviour(collectable.Attribute.Type);
        }
    }
    #endregion
}
