using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        #region Properties
        public CollectionManager CollectionManager => _collectionManager;
        public LevelManager LevelManager => _levelManager;
        public CollectableEvents Events => _events;
        #endregion

        #region Fields
        [SerializeField] private CollectionManager _collectionManager;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private CollectableEvents _events;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _levelManager.Initialise(this);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                ResetLevel();
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void ResetLevel()
        {
            _levelManager.ResetLevel();
            CollectableEvents.ResetLevelEvent?.Invoke();
            
        }
        #endregion

        #region Event Callbacks
        #endregion
    }
}
