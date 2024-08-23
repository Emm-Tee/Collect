using UnityEngine;
using static Collect.Core.Gameplay.IHoldCollectable;


namespace Collect.Core.Gameplay
{
    public partial class Collectable
    {
        #region Constants
        private const float PickUpCooldownDuration = 2f;
        #endregion

        #region Properties
        public bool InPickUpBuffer => _pickUpBuffer > 0;
        #endregion

        #region 
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _collider;

        [Space]

        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _moveTime = 1f;
        [SerializeField] private float _lerpSpeed = 1f;

        [SerializeField] private float _dropDistance = 1f;
        [SerializeField] private float _dropTime = 2f;
        [SerializeField] private float _collisionDetectionRadius;

        private bool _inStasis = true;

        private Vector3 _velocity;
        private Vector3 _goalPosition; // Follows the holder more closely - our position gets smoothly lerped to it

        private float _pickUpBuffer = 0f;
        private float _timeInDropDistance = 0f;

        private HoldingType _holdingType;
        protected Transform _holderHoldingTransform;
        #endregion

        #region Unity Methods

        private void Update()
        {
            if (_pickUpBuffer > 0f)
            {
                _pickUpBuffer -= Time.deltaTime;
            }

            HandleMovement();
        }

        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void SetRigidbodySettings(bool held)
        {
            if (held && !_rigidbody.isKinematic)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }

            _rigidbody.useGravity = !held;
            _rigidbody.isKinematic = held && _holdingType == IHoldCollectable.HoldingType.Hard;
            _inStasis = false;
        }

        private void HandleMovement()
        {
            if (_inStasis)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            if (_holderHoldingTransform != null)
            {
                switch (_holdingType)
                {
                    case IHoldCollectable.HoldingType.Hard:
                        SetGoalHardPosition();
                        break;
                    case IHoldCollectable.HoldingType.Soft:
                        SetGoalSoftPosition();
                        break;
                    case IHoldCollectable.HoldingType.Follow:
                        SetGoalFollowPosition();
                        break;
                }

                //See if dropped.
                TestForDropped();

                //Move
                MovePosition();
            }
        }

        private void SetGoalHardPosition()
        {
            //transform.position = _holderHoldingPosition.position;
            _goalPosition = _holderHoldingTransform.position;
        }

        private void SetGoalSoftPosition()
        {
            Debug.LogWarning($"Soft hold not implemented");
        }

        private void SetGoalFollowPosition()
        {
            Vector3 position = _goalPosition;
            Vector3 distance = _holderHoldingTransform.position - position;

            //don't move if we have immediate blockages
            if (Physics.Raycast(position, distance, _collider.radius * 1.1f, layerMask: LayerMasks.Environment))
            {
                return;
            }

            Vector3 delta = distance.normalized * _speed * Time.deltaTime;
            Vector3 newGoalPosition = position + delta;

            //If we're less than a single step away, go straight to home
            if (delta.sqrMagnitude > distance.sqrMagnitude)
            {
                newGoalPosition = _holderHoldingTransform.position;
            }

            _goalPosition = newGoalPosition;
        }

        private void MovePosition()
        {
            Vector3 position = transform.position;

            Vector3 move = Vector3.SmoothDamp(position, _goalPosition, ref _velocity, _moveTime, _lerpSpeed);
            _rigidbody.MovePosition(move);
        }

        private void TestForDropped()
        {
            Vector3 distance = _holderHoldingTransform.position - _goalPosition;
            float sqrMag = distance.sqrMagnitude;
            float drop = _dropDistance * _dropDistance;

            if (sqrMag > drop * 4)
            {
                CollectableEvents.AttemptReleaseCollectable?.Invoke(_holder, this);
                return;
            }
            if (sqrMag > drop)
            {
                _timeInDropDistance += Time.deltaTime;
                if (_timeInDropDistance > _dropTime)
                {
                    CollectableEvents.AttemptReleaseCollectable?.Invoke(_holder, this);
                    _timeInDropDistance = 0;
                    return;
                }
            }
            else
            {
                _timeInDropDistance = 0;
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
        #endregion
    }
}
