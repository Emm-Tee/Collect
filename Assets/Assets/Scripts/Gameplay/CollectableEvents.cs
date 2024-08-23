using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class CollectableEvents : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        public delegate void PairDelegate(IHoldCollectable holder, Collectable collectable);
        public delegate void CollectableDelegate(Collectable collectable);
        public delegate void BlankDelegate();

        public static PairDelegate AttemptAtPickUp;
        public static PairDelegate PickUpComplete;

        public static PairDelegate AttemptReleaseCollectable;
        public static PairDelegate CollectableReleased;

        public static CollectableDelegate CollectableCompleted;
        public static CollectableDelegate CollectableIncompleted;

        public static BlankDelegate LevelComplete;
        public static BlankDelegate ResetLevelEvent;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}