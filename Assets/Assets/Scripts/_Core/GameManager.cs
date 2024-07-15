using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties
    public CollectionManager CollecationManager => _collectionManager;
    public LevelManager LevelManager => _levelManager;
    #endregion

    #region Fields
    [SerializeField] private CollectionManager _collectionManager;
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private Player _player;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _player.Initialise(_collectionManager);

        _levelManager.Initialise(this);
    }
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
