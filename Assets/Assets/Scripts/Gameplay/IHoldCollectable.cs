using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public interface IHoldCollectable
    {
        public enum HoldingType
        {
            Hard = 0, //Collectable position stuck to holding point, no physics
            Soft = 1, //Collectable position stuck to holding point but with physics
            Follow = 2 //Collectable position moves towards holding point if holding
        }
        #region Unity Methods
        #endregion

        #region Public Methods
        public void PickUpCollectable(Collectable collectable);

        public void ReleaseCollectable(Collectable collectable);

        public Transform GetHoldingTransform();

        public HoldingType GetHoldingType();

        public Attribute GetAttribute();
        #endregion

        #region Protected Methods
        public Collectable GetHeldCollectable();

        public void SubscribeToHolderEvents()
        {
            CollectableEvents.PickUpComplete += OnPickupCompleted;
        }

        public void UnSubscribteToHolderEvents()
        {

            CollectableEvents.PickUpComplete -= OnPickupCompleted;
        }
        #endregion

        #region Event Callbacks
        protected void OnPickupCompleted(IHoldCollectable holder, Collectable collectable)
        {
            //Test to see if we've been robbed
            if(collectable == GetHeldCollectable())
            {
                ReleaseCollectable(collectable);
            }

            if(holder != this)
            {
                return;
            }

            PickUpCollectable(collectable);
        }
        #endregion
    }
}