using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect.Core.Gameplay
{
    public class Interactable : MonoBehaviour
    {
        #region Properties
        public Attribute Attribute => _attribute;
        #endregion

        #region Fields
        [SerializeField] protected Attribute _attribute;

        [Space]

        [Header("Appearance")]
        [SerializeField] private MeshRenderer[] _decoratedRenderers;
        [SerializeField] private MeshRenderer[] _postCollectionDecoratedRenderers;

        [Space]

        [SerializeField] private Material _defaultMaterial;

        [Space]

        private Material _decorationMaterial;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public void SetAttribute(Attribute attribute)
        {
            _attribute = attribute;

            _decorationMaterial = new Material(_attribute.Material)
            {
                color = _attribute.Color
            };

            UpdateAppearance(false);
        }
        #endregion

        #region Protected Methods
        protected void UpdateAppearance(bool collected)
        {

            Material mat = collected ? _decorationMaterial : _defaultMaterial;

            foreach (MeshRenderer renderer in _postCollectionDecoratedRenderers)
            {
                renderer.material = mat;
            }

            if(!collected)
            {
                foreach (MeshRenderer renderer in _decoratedRenderers)
                {
                    renderer.material = _decorationMaterial;
                }
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Callbacks
        #endregion
    }
}