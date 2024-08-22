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
        }

        private void UnsubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp -= OnAttemptPickup;
        }

        private void ReleaseCollectable(IHoldCollectable holder, Collectable collectable)
        {
            holder.ReleaseCollectable();
            collectable.BeReleased();

            CollectableEvents.CollectableReleased?.Invoke(holder, collectable);
        }
        #endregion

        #region Event Callbacks
        private void OnAttemptPickup(IHoldCollectable holder, Collectable collectable)
        {
            if (!collectable.CanCollect(holder))
            {
                return;
            }

            if (collectable.IsHeld)
            {
                ReleaseCollectable(collectable.CurrentHolder, collectable);
            }

            //Set data
            collectable.GetCollected(holder);
            holder.PickUpCollectable(collectable);

            CollectableEvents.PickUpComplete?.Invoke(holder, collectable);
        }
        #endregion
    }

}