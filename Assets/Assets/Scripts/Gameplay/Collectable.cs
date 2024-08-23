using UnityEngine;

namespace Collect.Core.Gameplay
{
    public partial class Collectable : Interactable
    {
        #region Properties
        public IHoldCollectable CurrentHolder => _holder;
        public IHoldCollectable CompletedWithHolder => _completedWithHolder;

        public bool IsComplete => _behaviour.ConditionIsComplete;
        public bool IsHeld => _holder != null;
        #endregion

        #region Fields
        private CollectableAttributeBehaviour _behaviour;

        private IHoldCollectable _holder;
        private IHoldCollectable _completedWithHolder;
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

            _originalPosition = transform.position;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _behaviour?.Deactivate();
            ToggleCollider(false);
        }

        public bool CanCollect(IHoldCollectable repository)
        {
            return _pickUpBuffer <= 0f;
        }

        public void BeReleased()
        {
            _holder = null;
            _holderHoldingTransform = null;

            SetRigidbodySettings(false);
        }

        public void GetCollected(IHoldCollectable holder)
        {
            _holder = holder;

            //becomes kinematic after being picked up
            SetRigidbodySettings(true);

            //Set up movement
            _holderHoldingTransform = _holder.GetHoldingTransform();
            _holdingType = _holder.GetHoldingType();
            _pickUpBuffer = PickUpCooldownDuration;
            _goalPosition = transform.position;
        }

        public void ConditionComplete()
        {
            _holder.ConditionComplete();

            _completedWithHolder = _holder;
            UpdateAppearance(true);
        }

        public void ConditionIncomplete()
        {
            _completedWithHolder.ConditionIncomplete();
            _completedWithHolder = null;
            UpdateAppearance(false);
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private CollectableAttributeBehaviour CreateBehaviour()
        {
            switch (_attribute.Type)
            {
                case Attribute.AttributeTypes.Completer:
                    return gameObject.AddComponent<CAB_Completer>();
                default:
                    return gameObject.AddComponent<AB_Base>();
            }
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}