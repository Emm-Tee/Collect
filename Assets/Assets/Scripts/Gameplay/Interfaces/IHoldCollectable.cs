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

        public Transform GetHoldingTransform();

        public HoldingType GetHoldingType();

        public bool GetIsPlayer();

        public AttributeSO GetAttribute();

        public void ConditionComplete();

        public void ConditionIncomplete();

        public void ReleaseCollectable()
        {
            NullCollectableReference();
        }
        #endregion

        #region Protected Methods
        public Collectable GetHeldCollectable();

        public void NullCollectableReference();
        #endregion

        #region Event Callbacks
        #endregion
    }
}