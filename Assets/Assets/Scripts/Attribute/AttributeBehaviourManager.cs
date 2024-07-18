using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Attribute;

public class AttributeBehaviourManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public bool CanCompleteComplete(GameManager gameManager)
    {
        return gameManager.LevelManager.CurrentLevelsCompleted;
    }

    public void CompleteLevel(GameManager gameManager)
    {
        Events.LevelComplete?.Invoke();
    }

    public void StartCompleteBehaviour(Attribute.AttributeTypes type)
    {
        switch (type)
        {
            case AttributeTypes.Base:
                break;
            case AttributeTypes.Red:
                break;
            case AttributeTypes.Blue:
                break;
            case AttributeTypes.Speed:
                break;
            case AttributeTypes.Light:
                break;
            case AttributeTypes.Key:
                break;
            case AttributeTypes.Strength:
                break;
            case AttributeTypes.Complete:
                Debug.Log($"FIN");
                break;
        }

    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
