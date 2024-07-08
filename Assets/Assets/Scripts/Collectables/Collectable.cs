using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    private IHoldCollectable _holder;
    private bool _heldByRepository;
    private Transform _holderHoldingPosition;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (_holderHoldingPosition != null)
        {
            transform.position = _holderHoldingPosition.position;
        }
    }
    #endregion

    #region Public Methods
    public void PlayerPickedUp(Player player)
    {
        if (_heldByRepository)
        {
            return;
        }

        GetPickedUp(player, true);
    }
   
    public void RepositoryPickedUp(Repository repository)
    {
        //held by player
        if (!_heldByRepository && _holder != null)
        {
            _holder.ReleaseCollectable(this);
        }

        GetPickedUp(repository, true);
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    private void GetPickedUp(IHoldCollectable holder, bool isRepository)
    {
        _holder = holder;
        _heldByRepository = isRepository;

        _holderHoldingPosition = _holder.GetHoldingPosition();

        _holder.PickUpCollectable(this);
    }
    #endregion

    #region Event Callbacks
    #endregion
}
