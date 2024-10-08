using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Handles bespoke behaviour of the attribute.
    /// Created manually in Collectable.cs CreateBehaviour() - each new CAB_ class needs to be added there
    /// </summary>
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

        public virtual void Reset()
        {
            _heldByCorrectRepository = false;
            _conditionComplete = false;
        }

        public virtual void UpdateBehaviour()
        {
            //Base does nothing
        }

        public virtual void GetCollected()
        {

        }

        public virtual void BeReleased()
        {

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

        /// <summary>
        /// Called internally when the complete condition is met, inherited behaviours add to it
        /// </summary>
        protected virtual void AttributeConditionCompleted()
        {
            _collectable.UpdateAppearance(true);
        }

        /// <summary>
        /// Called internally when the complete condition gets unmet, inherited behaviours add to it
        /// </summary>
        protected virtual void AttributeConditionIncompleted()
        {
            _collectable.UpdateAppearance(false);
        }

        protected bool IsMatchingPair(IHoldCollectable holder, Collectable collectable)
        {
            return collectable.Attribute.Type == holder.GetAttribute().Type;
        }
        #endregion

        #region Event Callbacks
        /// <summary>
        /// Subscribes to event that triggers when the Collection Manager finished the process of a collectable being picked up by an IHoldCollectable holder
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="collectable"></param>
        protected virtual void OnPickupComplete(IHoldCollectable holder, Collectable collectable)
        {
            if(collectable != _collectable)
            {
                return;
            }

            if (IsMatchingPair(holder, collectable))
            {
                CollectableEvents.CollectableCompleted?.Invoke(_collectable);
            }
        }

        /// <summary>
        /// SUbscribes to event triggered when an IHoldCollectable holder stops holding a collectable
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="collectable"></param>
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

        /// <summary>
        /// Subscribes to event triggered when a collectable's end condition is completed. 
        /// AttributeConditionCompleted is what a behaviour should use to do things when our condition is completed
        /// </summary>
        /// <param name="collectable"></param>
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

        /// <summary>
        /// Subscribes to event triggered when a collectable whose end condition was completed becomes incomplete. 
        /// AttributeConditionIncompleted is what a behaviour should probably use to do things when our condition is incompleted
        /// </summary>
        /// <param name="collectable"></param>
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
