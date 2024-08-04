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
        #endregion

        #region Unity Methods
        private void Update()
        {
            if (_holderHoldingPosition != null)
            {
                transform.position = _holderHoldingPosition.position;
            }

            if (_pickUpBuffer > 0f)
            {
                _pickUpBuffer -= Time.deltaTime;
            }
        }
        #endregion

        #region Public Methods
        public void InitialiseBehaviour(GameManager gameManager)
        {
            _behaviour = CreateBehaviour();
            _behaviour.Initialize(this, gameManager);
        }

        public void Activate()
        {
            _behaviour?.Activate();
        }

        public void Deactivate()
        {
            _behaviour?.Deactivate();
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

            _holder.PickUpCollectable(this);
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
        #endregion

        #region Event Callbacks
        #endregion
    }
}