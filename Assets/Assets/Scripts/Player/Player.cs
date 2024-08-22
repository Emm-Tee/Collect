using Collect.Core.Gameplay;
using UnityEngine;
using static Collect.Core.Gameplay.IHoldCollectable;

namespace Collect.Core.Player
{
    public class Player : MonoBehaviour, IHoldCollectable
    {
        #region Properties
        #endregion

        #region Fields
        [SerializeField] private Attribute _attribute;

        [Header("Collectable")]
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private float _pickUpLength = 5f;

        private RaycastHit _pickupHit;
        private Collectable _heldCollectable;

        private HoldingType _holdingType;
        #endregion

        #region Unity Methods
        private void Update()
        {
            TestPickup();
        }

        private void Awake()
        {
            Initialise();
        }
        #endregion

        #region Public Methods
        public void Initialise()
        {
            _holdingType = _attribute.HoldingType;
        }

        public void PickUpCollectable(Collectable collectable)
        {
            _heldCollectable = collectable;
        }

        public Transform GetHoldingTransform()
        {
            return _aimTarget;
        }

        public HoldingType GetHoldingType()
        {
            return _holdingType;
        }

        public Attribute GetAttribute()
        {
            return _attribute;
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
            //nothing to do here
        }

        public void ConditionIncomplete()
        {
            //Nothing to see here
        }
        #endregion

        #region Protected 
        #endregion

        #region Private Methods
        private void TestPickup()
        {
            if (_heldCollectable)
            {
                return;
            }

            Vector3 rayToCamera = Vector3.Normalize(Camera.main.transform.rotation * Vector3.back) * _pickUpLength;

            if (Physics.Raycast(_aimTarget.position, rayToCamera, out _pickupHit, _pickUpLength, LayerMasks.Collectable))
            {
                Debug.DrawRay(_aimTarget.position, rayToCamera, Color.magenta);

                if (_pickupHit.collider.gameObject.TryGetComponent<Collectable>(out Collectable collectable))
                {
                    AttemptPickup(collectable);
                }
            }
        }

        private void AttemptPickup(Collectable collectable)
        {
            //TODO: implement build in cooldown
            CollectableEvents.AttemptAtPickUp?.Invoke(this, collectable);
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}
