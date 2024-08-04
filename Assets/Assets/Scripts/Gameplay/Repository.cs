using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            _collectableColliders = Physics.OverlapSphere(transform.position, _detectionRadius, LayerMasks.Collectable);

            if (_collectableColliders.Length > 0)
            {
                if (_collectableColliders[0].gameObject.TryGetComponent<Collectable>(out Collectable collectable))
                {
                    if (CollectionManager.CanCollect(collectable, this))
                    {
                        CollectableEvents.AttemptAtPickUp?.Invoke(this, collectable);
                    }
                }
            }
        }
        #endregion

        #region Public Methods

        public Attribute GetAttribute()
        {
            return _attribute;
        }

        public void PickUpCollectable(Collectable collectable)
        {
            _heldCollectable = collectable;

            if (collectable.Attribute.Type == _attribute.Type)
            {
                //Trigger success event
                CollectableEvents.PickUpComplete?.Invoke(this, collectable);

                UpdateAppearance(true);
            }
        }

        public void ReleaseCollectable(Collectable collectable)
        {
            _heldCollectable = null;
        }

        public Transform GetHoldingTransform()
        {
            return _collectableHoldingPoint;
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