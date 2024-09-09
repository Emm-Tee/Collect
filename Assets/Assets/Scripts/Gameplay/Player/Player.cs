using UnityEngine;
using static Collect.Core.Gameplay.IHoldCollectable;

namespace Collect.Core.Gameplay
{
    public class Player : MonoBehaviour, IHoldCollectable, IContributeToTemperature
    {
        #region Properties
        #endregion

        #region Fields
        [SerializeField] private AttributeSO _attribute;
        [SerializeField] private bool _isPlayer = false;

        [Header("Collectable")]
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private float _pickUpLength = 5f;


        [Header("Temperature")]
        [SerializeField] private float _temperatureRadius;
        [SerializeField] private float _bodyTemp;
        [SerializeField] private AnimationCurve _bodyTempDispersionCurve;

        GameManager _gameManager;

        //Interfaces
        private IHoldCollectable _holder;
        private IContributeToTemperature _temp;

        private Collectable _heldCollectable;
        private HoldingType _holdingType;

        private RaycastHit _pickupHit;
        #endregion

        #region Unity Methods
        private void Update()
        {
            TestPickup();
        }

        private void Awake()
        {
            CollectableEvents.ResetLevelEvent += OnReset;

            _holder = this;
            _temp = this;
        }

        private void OnDisable()
        {
            CollectableEvents.ResetLevelEvent -= OnReset;
            _temp.DeregisterSelfFromTemperatureManager(_gameManager.TemperatureManager);
        }
        #endregion

        #region Public Methods
        public void Initialise(GameManager gameManager)
        {
            _gameManager = gameManager;
            _holdingType = _attribute.HoldingType;
            _temp.RegisterSelfToTemperatureManager(_gameManager.TemperatureManager);
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

        public bool GetIsPlayer()
        {
            return _isPlayer;
        }

        public AttributeSO GetAttribute()
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

        //Some body heat
        public float GetTempModifierAtPosition(Vector3 position)
        {
            float sqrDistance = (position - transform.position).sqrMagnitude;
            float distPercent = Mathf.InverseLerp(0, _temperatureRadius * _temperatureRadius, sqrDistance);

            return _bodyTemp * _bodyTempDispersionCurve.Evaluate(distPercent);
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
            //TODO: implement built in cooldown
            CollectableEvents.AttemptAtPickUp?.Invoke(this, collectable);
        }
        #endregion

        #region Event Callbacks
        private void OnReset()
        {
            _holder.ReleaseCollectable();
        }
        #endregion
    }
}
