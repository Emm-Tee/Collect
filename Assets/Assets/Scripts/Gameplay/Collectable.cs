using UnityEngine;
using static Attribute.AttributeTypes;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// Base partial class contains the core interactable things - collectable specific additions in other files
    /// </summary>
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

        private bool _active = false;
        #endregion

        #region Unity Methods
        private void Update()
        {
            if (!_active)
            {
                return;
            }

            _behaviour.UpdateBehaviour();

            if (_pickUpBuffer > 0f)
            {
                _pickUpBuffer -= Time.deltaTime;
            }

            HandleMovement();
        }
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

            _active = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _behaviour?.Deactivate();
            ToggleCollider(false);

            _active = false;
        }

        public bool CanCollect(IHoldCollectable repository)
        {
            return _pickUpBuffer <= 0f;
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

            _behaviour.GetCollected();
        }

        public void BeReleased()
        {
            _holder = null;
            _holderHoldingTransform = null;

            SetRigidbodySettings(false);

            _behaviour.BeReleased();
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
                case Completer:
                    return gameObject.AddComponent<CAB_Completer>();
                case Timer:
                    return gameObject.AddComponent<CAB_Melt>();
                default:
                    return gameObject.AddComponent<CAB_Base>();
            }
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}