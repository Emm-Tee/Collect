using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("Collectable")]
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private float _pickUpLength = 5f;

    private RaycastHit _pickupHit;
    private Collectable _heldCollectable;
    #endregion

    #region Unity Methods
    private void Update()
    {
        TestPickup();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void TestPickup()
    {
        if (_heldCollectable)
        {
            return;
        }

        Vector3 rayToCamera = Vector3.Normalize(Camera.main.transform.rotation * Vector3.back) * _pickUpLength;

        if(Physics.Raycast(_aimTarget.position, rayToCamera, out _pickupHit, _pickUpLength, LayerMasks.Collectable))
        {
            Debug.DrawRay(_aimTarget.position, rayToCamera, Color.magenta);

            if (_pickupHit.collider.gameObject.TryGetComponent<Collectable>(out Collectable collectable))
            {
                PickupCollectable(collectable);
            }
        }
    }

    private void PickupCollectable(Collectable collectable)
    {
        _heldCollectable = collectable;

        _heldCollectable.transform.SetParent(_aimTarget);

        collectable.PlayerPickedUp();
    }
    #endregion

    #region Event Callbacks
    #endregion
}
