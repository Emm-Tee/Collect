using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            CollectableEvents.AttemptAtPickUp += OnAttemptPickup;
        }

        private void UnsubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp -= OnAttemptPickup;
        }
        #endregion

        #region Event Callbacks
        private void OnAttemptPickup(IHoldCollectable repository, Collectable collectable)
        {
            if (collectable.CurrentHolder != null)
            {
                collectable.CurrentHolder.ReleaseCollectable(collectable);
            }

            collectable.GetPickedUp(repository);
        }
        #endregion
    }

}