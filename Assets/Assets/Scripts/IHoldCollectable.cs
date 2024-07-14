using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldCollectable
{
    #region Properties
    #endregion

    #region Fields
    #endregion

    #region Unity Methods
    #endregion

    #region Public Methods
    public void PickUpCollectable(Collectable collectable);

    public void ReleaseCollectable(Collectable collectable);

    public Transform GetHoldingPosition();
    public RepositoryAttribute GetRepositoryAttribute();
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
