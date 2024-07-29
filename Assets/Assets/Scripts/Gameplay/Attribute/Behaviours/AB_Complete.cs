using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class AB_Complete : AttributeBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        protected override void OnPickUpComplete(IHoldCollectable holder, Collectable collectable)
        {
            if (_gameManager.LevelManager.CurrentLevelsCompleted)
            {
                Debug.Log("FIN");
                CollectableEvents.LevelComplete?.Invoke();
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}
