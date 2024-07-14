using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldCollectable
{
    #region Unity Methods
    #endregion

    #region Public Methods
    public void PickUpCollectable(Collectable collectable);

    public void ReleaseCollectable(Collectable collectable);

    public Transform GetHoldingPosition();
    public Attribute GetAttribute();
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
