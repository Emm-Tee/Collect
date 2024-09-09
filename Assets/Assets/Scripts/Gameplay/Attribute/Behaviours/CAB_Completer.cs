namespace Collect.Core.Gameplay
{
    public class CAB_Completer : CollectableAttributeBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        protected override void OnPickupComplete(IHoldCollectable holder, Collectable collectable)
        {
            if (collectable == _collectable && IsMatchingPair(holder, collectable))
            {
                _heldByCorrectRepository = true;
            }

            if (_heldByCorrectRepository && _gameManager.LevelManager.CurrentLevelIsReadyToComplete)
            {
                CollectableEvents.CollectableCompleted?.Invoke(_collectable);
            }
        }

        protected override void AttributeConditionCompleted()
        {
            base.AttributeConditionCompleted();
            CollectableEvents.LevelComplete();
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
