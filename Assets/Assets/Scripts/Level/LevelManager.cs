using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Events.LevelComplete += OnLevelComplete;
    }

    private void OnDestroy()
    {
        Events.LevelComplete -= OnLevelComplete;
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
        foreach(LevelSet level in _levels)
        {
            level.Initialise(_gameManager.CollecationManager);
        }
    }

    private void StartNewLevel()
    {
        //
    }

    private void OnLevelComplete()
    {
        _currentLevelIndex++;
    }
    #endregion

    #region Event Callbacks
    #endregion
}
