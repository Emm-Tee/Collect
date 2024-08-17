using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    //One collectable and matching repository
    public class InteractablePair : MonoBehaviour
    {
        #region Properties
        public bool IsMatched => _repository.IsMatched;
        #endregion

        #region Fields
        [SerializeField] private Repository _repository;
        [SerializeField] private Collectable _collectable;
        [SerializeField] private Attribute _attribute;
        #endregion

        #region Unity Methods
        private void OnDrawGizmos()
        {
            Debug.DrawLine(_repository.transform.position, _collectable.transform.position);
        }
        #endregion

        #region Public Methods
        public void InitializePairing(GameManager gameManager)
        {
            _repository.Initialise(_attribute);

            _collectable.Initialise(_attribute, gameManager);
        }

        public void Activate()
        {
            _collectable.Activate();
            _repository.Activate();
        }

        public void Deactivate()
        {
            _collectable.Deactivate();
            _repository.Deactivate();
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}
