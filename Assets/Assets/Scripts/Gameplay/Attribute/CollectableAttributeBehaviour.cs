using UnityEngine;

namespace Collect.Core.Gameplay
{
    public abstract class CollectableAttributeBehaviour : MonoBehaviour
    {
        #region Properties
        public bool ConditionIsComplete => _conditionComplete;
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
        #endregion

        #region Protected Methods    
        #endregion

        #region Private Methods
        protected virtual void SubscribeToEvents()
        {
            CollectableEvents.PickUpComplete += OnPickupComplete;
            CollectableEvents.CollectableReleased += OnCollectableReleased;

            CollectableEvents.CollectableCompleted += OnCollectableCompleted;
            CollectableEvents.CollectableIncompleted += OnCollectableIncompleted;
        }

        protected virtual void UnsubscribeToEvents()
        {
            CollectableEvents.PickUpComplete -= OnPickupComplete;
            CollectableEvents.CollectableReleased -= OnCollectableReleased;

            CollectableEvents.CollectableCompleted -= OnCollectableCompleted;
            CollectableEvents.CollectableIncompleted -= OnCollectableIncompleted;
        }

        //What the attribute does when its complete condition is met
        protected virtual void AttributeConditionCompleted()
        {

        }

        protected virtual void AttributeConditionIncompleted()
        {

        }
        #endregion

        #region Event Callbacks
        protected virtual void OnPickupComplete(IHoldCollectable holder, Collectable collectable)
        {
            if(collectable != _collectable)
            {
                return;
            }

            if (_collectable.Attribute.HoldingType == holder.GetAttribute().HoldingType)
            {
                CollectableEvents.CollectableCompleted?.Invoke(_collectable);
            }
        }

        protected virtual void OnCollectableReleased(IHoldCollectable holder, Collectable collectable)
        {
            // default is to not act if we're not the collectable being picked up
            if (collectable != _collectable)
            {
                return;
            }

            if (_conditionComplete)
            {
                Debug.Log($"Invoking collectable incompleted");
                CollectableEvents.CollectableIncompleted?.Invoke(collectable);
            }
        }

        protected virtual void OnCollectableCompleted(Collectable collectable)
        {
            if(collectable != _collectable)
            {
                return;
            }

            _conditionComplete = true;
            _collectable.ConditionComplete();

            AttributeConditionCompleted();
        }

        protected virtual void OnCollectableIncompleted(Collectable collectable)
        {
            if (collectable != _collectable)
            {
                return;
            }

            _conditionComplete = false;
            _collectable.ConditionIncomplete();

            AttributeConditionIncompleted();
        }
        #endregion
    }
}
