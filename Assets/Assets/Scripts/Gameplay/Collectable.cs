using UnityEngine;

namespace Collect.Core.Gameplay
{
    public partial class Collectable : Interactable
    {
        #region Properties
        public IHoldCollectable CurrentHolder => _holder;
        #endregion

        #region Fields
        private CollectableAttributeBehaviour _behaviour;

        private IHoldCollectable _holder;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void Initialise(Attribute attribute, GameManager gameManager)
        {
            base.Initialise(attribute);

            _behaviour = CreateBehaviour();
            _behaviour.Initialize(this, gameManager);

            ToggleCollider(false);
        }

        public override void Activate()
        {
            base.Activate();
            _behaviour?.Activate();
            SetKinematicStasis();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _behaviour?.Deactivate();
        }

        public bool CanCollect(IHoldCollectable repository)
        {
            return _pickUpBuffer <= 0f;
        }

        public void BeDropped()
        {
            _holder = null;
            _holderHoldingPosition = null;

            SetRigidbodySettings(false);
        }

        public bool AmICompleteWithYou(IHoldCollectable holder)
        {
            return holder == _holder;
        }
        #endregion

        #region Protected Methods
        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            CollectableEvents.PickUpComplete += OnPickupComplete;
        }

        protected override void UnsubscribeToEvents()
        {
            base.UnsubscribeToEvents();
            CollectableEvents.PickUpComplete -= OnPickupComplete;
        }

        protected override bool IsRelevantToCompletion(Collectable collectable)
        {
            return collectable == this;
        }
        #endregion

        #region Private Methods
        private CollectableAttributeBehaviour CreateBehaviour()
        {
            switch (_attribute.Type)
            {
                case Attribute.AttributeTypes.Complete:
                    return gameObject.AddComponent<CAB_Complete>();
                default:
                    return gameObject.AddComponent<AB_Base>();
            }
        }

        private void SetKinematicStasis()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
            _inStasis = true;
            ToggleCollider(true);
        }

        private void ToggleCollider(bool enable)
        {
            _collider.enabled = enable;
        }
        #endregion

        #region Event Callbacks
        private void OnPickupComplete(IHoldCollectable holder, Collectable collectable)
        {
            if(collectable != this)
            {
                return;
            }

            _holder = holder;

            //becomes kinematic after being picked up
            SetRigidbodySettings(true);

            _behaviour.CheckConditionComplete();

            //Set up movement
            _holderHoldingPosition = _holder.GetHoldingTransform();
            _holdingType = _holder.GetHoldingType();
            _pickUpBuffer = PickUpCooldownDuration;
            _goalPosition = transform.position;
        }
        #endregion
    }
}