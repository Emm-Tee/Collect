using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private float _detectionRadius;

    private Collectable _heldCollectable;

    private Collider[] _collectableColliders;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (_heldCollectable)
        {
            return;
        }

        _collectableColliders = Physics.OverlapSphere(transform.position, _detectionRadius, LayerMasks.Collectable);
        if(_collectableColliders.Length > 0 )
        {
            if (_collectableColliders[0].gameObject.TryGetComponent<Collectable>(out Collectable collectable))
            {

            }
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
