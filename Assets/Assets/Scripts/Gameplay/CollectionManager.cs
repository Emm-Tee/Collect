using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class CollectionManager : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
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
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void SubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp += OnAttemptPickup;
            CollectableEvents.CollectableDropped += OnCollectableDropped;
        }

        private void UnsubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp -= OnAttemptPickup;
        }
        #endregion

        #region Event Callbacks
        private void OnAttemptPickup(IHoldCollectable holder, Collectable collectable)
        {
            if (collectable.CanCollect(holder))
            {
                CollectableEvents.PickUpComplete?.Invoke(holder, collectable);
            }
        }

        private void OnCollectableDropped(IHoldCollectable holder, Collectable collectable)
        {
            collectable.BeDropped();
            holder.ReleaseCollectable(collectable);
        }
        #endregion
    }

}