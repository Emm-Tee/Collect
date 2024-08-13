using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallMove : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private Rigidbody _rib;

    [SerializeField] private bool _move;
    [SerializeField] private float _speed;

    private Vector3 _startingPosition;
    #endregion

    #region Unity Methods
    private void FixedUpdate()
    {
        if (!_move)
        {
            return;
        }

        Vector3 newPos = transform.position + Vector3.left * _speed;

        _rib.MovePosition(newPos);
    }

    private void Awake()
    {
        _startingPosition = transform.position;
        _move = false;
    }
    #endregion

    #region Public Methods
    [ContextMenu("Reset")]
    #endregion

    #region Public Methods
    public void Reset()
    {
        transform.position = _startingPosition;
        _move = false;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
