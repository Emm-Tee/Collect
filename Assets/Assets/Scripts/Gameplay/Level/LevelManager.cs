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

            foreach (LevelSet level in _levels)
            {
                level.gameObject.SetActive(true);
            }
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

            _levels[0].ActivateLevel();
        }

        private void OnLevelComplete()
        {
            _levels[_currentLevelIndex].DeactivateLevel();

            _currentLevelIndex++;

            if (_currentLevelIndex >= _levels.Length)
            {
                return;
            }

            _levels[_currentLevelIndex].ActivateLevel();
        }

        #endregion

        #region Event Callbacks
        #endregion
    }
}
