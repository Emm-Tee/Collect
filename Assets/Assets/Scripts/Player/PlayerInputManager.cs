using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public struct InputStruct
    {
        public bool IsMoving => Movement.sqrMagnitude > Mathf.Epsilon; 
        public bool IsLooking => Look.sqrMagnitude > Mathf.Epsilon;

        public Vector3 Movement;
        public Vector2 Look;
    }

    #region Properties
    #endregion

    #region Fields
    [SerializeField] private CharacterController _characterController;

    [Space]

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    private InputStruct _input;

    #endregion

    #region Unity Methods
    private void Update()
    {
        UpdateMovement();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    public float ang;
    public Vector3 axis;

    public Vector3 angs;

    #region Private Methods
    private void UpdateMovement()
    {
        if (_input.IsLooking)
        {
            Quaternion rotationDelta = Quaternion.AngleAxis(_input.Look.x * _turnSpeed * Time.deltaTime, Vector3.up);
            transform.rotation *= rotationDelta;
        }

        if(_input.IsMoving)
        {
            Vector3 movement = _input.Movement;
            movement *= Time.deltaTime * _moveSpeed;

            //camera relative
            movement = transform.rotation * movement;

            _characterController.Move(movement);
        }
    }

    private void UpdateMoveInput(Vector2 input)
    {
        _input.Movement = new Vector3(input.x, 0, input.y);
    }

    public void UpdateLookInput(Vector2 input)
    {
        _input.Look = input;
    }
    #endregion

    #region Event Callbacks
    public void OnMove(InputValue inputValue)
    {
        UpdateMoveInput(inputValue.Get<Vector2>());
    }

    public void OnLook(InputValue inputValue)
    {
        UpdateLookInput(inputValue.Get<Vector2>());
    }
    #endregion
}
