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
    [SerializeField] private Transform _camTarget;

    [Space]

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _lookXSensitivity = 50;
    [SerializeField] private float _lookYSensitivity = 5;
    [SerializeField] private Vector2 _camTargetHeightMinMax = new Vector2(-1, 2);

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

    #region Private Methods
    private void UpdateMovement()
    {
        if (_input.IsLooking)
        {
            //x
            Quaternion rotationDelta = Quaternion.AngleAxis(_input.Look.x * _lookXSensitivity * Time.deltaTime, Vector3.up);
            transform.rotation *= rotationDelta;

            //y
            float delta = _input.Look.y * _lookYSensitivity * Time.deltaTime;

            Vector3 localPos = _camTarget.localPosition;
            localPos.y = Mathf.Clamp(localPos.y + delta, _camTargetHeightMinMax.x, _camTargetHeightMinMax.y);
            _camTarget.localPosition = localPos;
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
        //TODO: set up mouse specific

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
