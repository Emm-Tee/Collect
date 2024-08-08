using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class Collectable : Interactable
    {
        #region Constants
        private float PickUpCooldownDuration = 2f;
        #endregion

        #region Properties
        public bool InPickUpBuffer => _pickUpBuffer > 0;
        public IHoldCollectable CurrentHolder => _holder;
        #endregion

        #region Fields
        private AttributeBehaviour _behaviour;

        private IHoldCollectable _holder;
        private Transform _holderHoldingPosition;

        private float _pickUpBuffer = 0f;
        private Vector3 _goalPosition;

        private bool _inStasis = true;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        [Space]

        [SerializeField] private float _maxMoveSpeed = 1f;
        [SerializeField] private AnimationCurve _speedModifierCurve;

        [SerializeField] private float _dropDistance = 1f;
        #endregion

        #region Unity Methods

        private void Update()
        {
            if (_pickUpBuffer > 0f)
            {
                _pickUpBuffer -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (_inStasis)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            if (_holderHoldingPosition != null)
            {
                _goalPosition = _holderHoldingPosition.position;

                Vector3 position = transform.position;

                Vector3 direction = _goalPosition - position;

                if (direction.sqrMagnitude > _dropDistance * _dropDistance)
                {
                    CollectableEvents.CollectableDropped?.Invoke(_holder, this);
                }

                float maxSpeedDistance = _dropDistance * .9f;
                float speedPercent = Mathf.InverseLerp(0, maxSpeedDistance * maxSpeedDistance, direction.sqrMagnitude);
                float speed = _speedModifierCurve.Evaluate(speedPercent);

                _rigidbody.MovePosition(Vector3.MoveTowards(position, _goalPosition, speed));
            }
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

        public void Activate()
        {
            _behaviour?.Activate();
            SetKinematicStasis();
        }

        public void Deactivate()
        {
            _behaviour?.Deactivate();
        }

        public void ToggleCollider(bool enable)
        {
            _collider.enabled = enable;
        }

        public bool CanCollect(IHoldCollectable repository)
        {
            return _pickUpBuffer <= 0f;
        }

        public void GetPickedUp(IHoldCollectable holder)
        {
            _holder = holder;

            _holderHoldingPosition = _holder.GetHoldingTransform();

            _pickUpBuffer = PickUpCooldownDuration;

            //becomes kinematic after being picked up
            ToggleHeldKinematic(true);
        }

        public void BeDropped()
        {
            _holder = null;
            _holderHoldingPosition = null;
            _goalPosition = Vector3.zero;

            ToggleHeldKinematic(false);
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private AttributeBehaviour CreateBehaviour()
        {
            switch (_attribute.Type)
            {
                case Attribute.AttributeTypes.Complete:
                    return gameObject.AddComponent<AB_Complete>();
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

        private void ToggleHeldKinematic(bool held)
        {
            _rigidbody.useGravity = !held;
            _rigidbody.isKinematic = false;
            _inStasis = false;
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}