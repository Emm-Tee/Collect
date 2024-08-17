using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public abstract class CollectableAttributeBehaviour : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        protected Collectable _collectable;
        protected GameManager _gameManager;

        protected bool _heldByCorrectRepository;
        protected bool _conditionComplete;
        #endregion

        #region Unity Methods
        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
        #endregion

        #region Public Methods
        public virtual void Initialize(Collectable collectable, GameManager gameManager)
        {
            _collectable = collectable;
            _gameManager = gameManager;
        }

        public virtual void Activate()
        {
            SubscribeToEvents();
        }

        public virtual void Deactivate()
        {
            UnsubscribeToEvents();
        }

        //Base behvaiour is just when we're in the right repository
        public virtual void CheckConditionComplete()
        {
            if (_heldByCorrectRepository)
            {
                _conditionComplete = true;
                Debug.Log($"Collectable complete invoked");
                CollectableEvents.CollectableCompleted?.Invoke(_collectable);
            }
        }
        #endregion

        #region Protected Methods    
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
        protected virtual void OnPickUpAttempt(IHoldCollectable holder, Collectable collectable)
        {
            //default is to not act if we're not the collectable being picked up
            if (collectable != _collectable)
            {
                return;
            }
        }

        protected virtual void OnPickUpComplete(IHoldCollectable holder, Collectable collectable)
        {
            //default is to not act if we're not the collectable being picked up
            if (collectable != _collectable)
            {
                return;
            }

            if(holder.GetAttribute().Type == _collectable.Attribute.Type)
            {
                _heldByCorrectRepository = true;
                Debug.Log($"Collectable Attribute behaviour Held by collectable = true");
            }
            CheckConditionComplete();
        }

        #endregion
    }
}
