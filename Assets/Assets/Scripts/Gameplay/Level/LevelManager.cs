using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        #region Properties
        public bool CurrentLevelsCompleted
        {
            get
            {
                for (int i = 0; i <= _currentLevelIndex; i++)
                {
                    if (!_levels[i].LevelComplete)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion

        #region Fields
        [SerializeField] private LevelSet[] _levels;

        private GameManager _gameManager;
        private int _currentLevelIndex;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            CollectableEvents.LevelComplete += OnLevelComplete;
        }

        private void OnDestroy()
        {
            CollectableEvents.LevelComplete -= OnLevelComplete;
        }
        #endregion

        #region Public Methods
        public void Initialise(GameManager gameManager)
        {
            _gameManager = gameManager;

            InitialiseLevels();
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void InitialiseLevels()
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].Initialize(_gameManager);
            }

            _levels[0].ToggleActiveLevel(true);
        }

        private void OnLevelComplete()
        {
            _levels[_currentLevelIndex].ToggleActiveLevel(false);

            _currentLevelIndex++;

            if (_currentLevelIndex >= _levels.Length)
            {
                return;
            }

            _levels[_currentLevelIndex].ToggleActiveLevel(true);
        }

        #endregion

        #region Event Callbacks
        #endregion
    }
}
