using UnityEngine;
using static Collect.Core.Gameplay.IHoldCollectable;

namespace Collect.Core.Gameplay
{
    public class Repository : Interactable, IHoldCollectable
    {
        #region Properties
        public bool IsMatched => _heldCollectable != null && _heldCollectable.Attribute.Type == _attribute.Type;
        #endregion

        #region Fields
        [SerializeField] private bool _isPlayer = false;

        [SerializeField] private float _detectionRadius;

        [SerializeField] private Transform _collectableHoldingPoint;

        private IHoldCollectable _holder;

        private HoldingType _holdingType;
        private Collectable _heldCollectable;

        private Collider[] _collectableColliders;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _holder = this;
        }

        private void Update()
        {
            if (_heldCollectable)
            {
                return;
            }

            Debug.DrawRay(_collectableHoldingPoint.position, _detectionRadius * Vector3.up);
            Debug.DrawRay(_collectableHoldingPoint.position, _detectionRadius * Vector3.left);
            Debug.DrawRay(_collectableHoldingPoint.position, _detectionRadius * Vector3.right);
            Debug.DrawRay(_collectableHoldingPoint.position, _detectionRadius * Vector3.down);
            _collectableColliders = Physics.OverlapSphere(_collectableHoldingPoint.position, _detectionRadius, LayerMasks.Collectable);

            if (_collectableColliders.Length > 0)
            {
                if (_collectableColliders[0].gameObject.TryGetComponent(out Collectable collectable))
                {
                    //TODO : implement cool down
                    CollectableEvents.AttemptAtPickUp?.Invoke(this, collectable);
                }
            }
        }
        #endregion

        #region Public Methods

        public Attribute GetAttribute()
        {
            return _attribute;
        }

        public override void Initialise(Attribute attribute)
        {
            base.Initialise(attribute);

            _holdingType = _attribute.HoldingType;
        }

        public void PickUpCollectable(Collectable collectable)
        {
            _heldCollectable = collectable;
        }

        public Transform GetHoldingTransform()
        {
            return _collectableHoldingPoint;
        }

        public HoldingType GetHoldingType()
        {
            return _holdingType;
        }

        public bool GetIsPlayer()
        {
            return _isPlayer;
        }

        public Collectable GetHeldCollectable()
        {
            return _heldCollectable;
        }

        public void NullCollectableReference()
        {
            _heldCollectable = null;
        }

        public void ConditionComplete()
        {
            UpdateAppearance(true);
        }

        public void ConditionIncomplete()
        {
            UpdateAppearance(false);
        }

        public override void TotalReset()
        {
            base.TotalReset();

            _holder.ReleaseCollectable();
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}