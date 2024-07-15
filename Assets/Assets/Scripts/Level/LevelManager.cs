using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private LevelSet[] _levels;

    private GameManager _gameManager;
    #endregion

    #region Unity Methods
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
    #endregion

    #region Event Callbacks
    #endregion
}
