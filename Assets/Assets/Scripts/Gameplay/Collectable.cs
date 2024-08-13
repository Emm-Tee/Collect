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
        private IHoldCollectable.HoldingType _holdingType;
        private Transform _holderHoldingPosition;

        private Vector3 _goalPosition;
        private Vector3 _velocity;

        private float _pickUpBuffer = 0f;
        private bool _inStasis = true;
        private float _timeOutOfDropDistance = 0f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _collider;

        [Space]

        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _moveTime = 1f;

        [SerializeField] private float _dropDistance = 1f;
        [SerializeField] private float _dropTime = 2f;
        [SerializeField] private float _collisionDetectionRadius;


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
                switch (_holdingType)
                {
                    case IHoldCollectable.HoldingType.Hard:
                        MoveHolderHard();
                        break;
                    case IHoldCollectable.HoldingType.Soft:
                        MoveHolderSoft();
                        break;
                    case IHoldCollectable.HoldingType.Follow:
                        MoveHolderFollow();
                        break;
                }
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
            _holdingType = _holder.GetHoldingType();

            _pickUpBuffer = PickUpCooldownDuration;

            //becomes kinematic after being picked up
            SetRigidbodySettings(true);
        }

        public void BeDropped()
        {
            _holder = null;
            _holderHoldingPosition = null;
            _goalPosition = Vector3.zero;

            SetRigidbodySettings(false);
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

        private void SetRigidbodySettings(bool held)
        {
            if(held && !_rigidbody.isKinematic)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }

            _rigidbody.useGravity = !held;
            _rigidbody.isKinematic = held && _holdingType == IHoldCollectable.HoldingType.Hard;
            _inStasis = false;
        }

        private void MoveHolderHard()
        {
            transform.position = _holderHoldingPosition.position;
        }

        private void MoveHolderSoft()
        {
            Debug.LogWarning($"Soft hold not implemented");
        }

        private void MoveHolderFollow()
        {
            _goalPosition = _holderHoldingPosition.position;

            Vector3 position = transform.position;

            Vector3 direction = _goalPosition - position;

            //Test if we've been out of drop distance for long enough to enact drop
            float sqrDistance = direction.sqrMagnitude;
            if( sqrDistance > _dropDistance)
            {
                _timeOutOfDropDistance += Time.deltaTime;
                if(_timeOutOfDropDistance > _dropTime)
                {
                    CollectableEvents.CollectableDropped?.Invoke(_holder, this);
                    _timeOutOfDropDistance = 0;
                    return;
                }
                CollectableEvents.CollectableDropped?.Invoke(_holder, this);
                return;
            }
            else
            {
                _timeOutOfDropDistance = 0;
            }

            //don't move if we have immediate blockages
            if (Physics.Raycast(position, direction, _collider.radius * 1.1f, layerMask: LayerMasks.Environment))
            {
                return;
            }

            Vector3 smooth = Vector3.SmoothDamp(position, _goalPosition, ref _velocity, _moveTime, _speed);

            _rigidbody.MovePosition(smooth);
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}