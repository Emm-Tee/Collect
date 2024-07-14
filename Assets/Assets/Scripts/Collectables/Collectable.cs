using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Constants
    private float PickUpCooldownDuration = 2f;
    #endregion

    #region Properties
    public CollectableAttribute Attribute => _attribute;
    public bool InPickUpBuffer => _pickUpBuffer > 0;
    public IHoldCollectable CurrentHolder => _holder;
    #endregion

    #region Fields
    [SerializeField] private CollectableAttribute _attribute;

    private IHoldCollectable _holder;
    private Transform _holderHoldingPosition;

    private float _pickUpBuffer = 0f;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (_holderHoldingPosition != null)
        {
            transform.position = _holderHoldingPosition.position;
        }

        if(_pickUpBuffer > 0f)
        {
            _pickUpBuffer -= Time.deltaTime;
        }
    }
    #endregion

    #region Public Methods
    public bool CanCollect(IHoldCollectable repository)
    {
        return _pickUpBuffer <= 0f;
    }

    public void GetPickedUp(IHoldCollectable holder)
    {
        _holder = holder;

        _holderHoldingPosition = _holder.GetHoldingPosition();

        _holder.PickUpCollectable(this);

        _pickUpBuffer = PickUpCooldownDuration;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
