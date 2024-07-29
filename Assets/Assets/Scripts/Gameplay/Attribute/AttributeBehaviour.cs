using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public abstract class AttributeBehaviour : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        protected Collectable _collectable;
        protected GameManager _gameManager;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void Initialize(Collectable collectable, GameManager gameManager)
        {
            _collectable = collectable;
            _gameManager = gameManager;
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
        #endregion

        #region Protected Methods
        protected virtual void OnPickUpAttempt(IHoldCollectable holder, Collectable collectable)
        {
            //default is to not act if we're not the one
            if (collectable != _collectable)
            {
                return;
            }
        }
        protected virtual void OnPickUpComplete(IHoldCollectable holder, Collectable collectable)
        {
            //default is to not act if we're not the one
            if (collectable != _collectable)
            {
                return;
            }
        }
        #endregion

        #region Private Methods
        private void SubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp += OnPickUpAttempt;
            CollectableEvents.PickUpComplete += OnPickUpComplete;
        }

        private void UnsubscribeToEvents()
        {
            CollectableEvents.AttemptAtPickUp -= OnPickUpAttempt;
            CollectableEvents.PickUpComplete -= OnPickUpComplete;
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}
