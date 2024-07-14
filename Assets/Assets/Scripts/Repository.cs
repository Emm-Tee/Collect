using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : MonoBehaviour, IHoldCollectable
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private RepositoryAttribute _attribute;

    [Space]

    [SerializeField] private float _detectionRadius;

    [SerializeField] private Transform _collectableHoldingPoint;

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
                if(CollectionManager.CanCollect(collectable, this))
                {
                    CollectionManager.CollectablePickedUp(collectable, this);
                }
            }
        }
    }
    #endregion

    #region Public Methods
    public void PickUpCollectable(Collectable collectable)
    {
        _heldCollectable = collectable;
    }

    public void ReleaseCollectable(Collectable collectable)
    {
        _heldCollectable = null;
    }
    
    public Transform GetHoldingPosition()
    {
        return _collectableHoldingPoint;
    }

    public RepositoryAttribute GetRepositoryAttribute()
    {
        return _attribute;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

    #region Event Callbacks
    #endregion
}
