using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet : MonoBehaviour
{
    #region Properties
    public bool LevelComplete
    {
        get
        {
            foreach (InteractablePair pair in _interactablePairings)
            {
                if (!pair.IsMatched)
                {
                    return false;
                }
            }
            return true;
        }
    }
    #endregion

    #region Fields
    [SerializeField] private InteractablePair[] _interactablePairings;
    [SerializeField] private Interactable[] _soloInteractables;

    [Header("Animations")]
    [SerializeField] private float _activeHeight;
    [SerializeField] private float _inactiveHeight;

    [SerializeField] private float _animateDuration = 2f;
    [SerializeField] private AnimationCurve _animateOutCurve;
    [SerializeField] private AnimationCurve _animationInCurve;

    private float _animateTime = 0f;
    private bool _animatingIn = false;
    #endregion

    #region Unity Methods
    private void Update()
    {
        ManageAnimation();
    }
    #endregion

    #region Public Methods
    public void Initialize(GameManager gameManager)
    {
        foreach (InteractablePair pair in _interactablePairings)
        {
            pair.InitializePairing(gameManager);
        }

        SetLevelPositionHeight(_inactiveHeight);
    }

    public void ToggleActiveLevel(bool animIn)
    {
        _animateTime = _animateDuration;
        _animatingIn = animIn;

        if (animIn)
        {
            SetActive(true);
        }
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void SetActive(bool Active)
    {
        foreach(InteractablePair pair in _interactablePairings)
        {
            pair.gameObject.SetActive(Active);
        }
    }

    private void ManageAnimation()
    {
        if (_animateTime > 0f)
        {
            _animateTime -= Time.deltaTime;

            float percent = Mathf.InverseLerp(_animateDuration, 0, _animateTime);
            float height;

            if (_animateTime < 0f)
            {
                height = _animatingIn ? _activeHeight : _inactiveHeight;
                _animateTime = 0;

                if(!_animatingIn)
                {
                    SetActive(false);
                }
            }
            else
            {
                AnimationCurve curve = _animatingIn? _animationInCurve : _animateOutCurve;

                height = Mathf.LerpUnclamped(_inactiveHeight, _activeHeight, curve.Evaluate(percent));
            }

            SetLevelPositionHeight(height);
        }
    }

    private void SetLevelPositionHeight(float height)
    {
        foreach (InteractablePair pair in _interactablePairings)
        {
            Vector3 position = pair.transform.position;
            position.y = height;
            pair.transform.position = position;
        }
    }
    #endregion

    #region Event Callbacks
    #endregion
}
