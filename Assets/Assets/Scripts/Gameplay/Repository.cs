using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float _detectionRadius;

        [SerializeField] private Transform _collectableHoldingPoint;

        private HoldingType _holdingType;
        private Collectable _heldCollectable;

        private Collider[] _collectableColliders;

        #endregion

        #region Unity Methods
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

        public override void Activate()
        {
            base.Activate();

            IHoldCollectable holder = this as IHoldCollectable;
            holder.SubscribeToHolderEvents();
        }

        public override void Deactivate()
        {
            base.Deactivate();

            IHoldCollectable holder = this as IHoldCollectable;
            holder.UnSubscribteToHolderEvents();
        }

        public void PickUpCollectable(Collectable collectable)
        {

            _heldCollectable = collectable;
        }

        public void ReleaseCollectable(Collectable collectable)
        {
            _heldCollectable = null;
        }

        public Transform GetHoldingTransform()
        {
            return _collectableHoldingPoint;
        }

        public HoldingType GetHoldingType()
        {
            return _holdingType;
        }

        public Collectable GetHeldCollectable()
        {
            return _heldCollectable;
        }
        #endregion

        #region Protected Methods
        protected override bool IsRelevantToCompletion(Collectable collectable)
        {
            return collectable.AmICompleteWithYou(this);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}