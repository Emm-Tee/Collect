using UnityEngine;

namespace Collect.Core.Gameplay
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Properties
        public AttributeSO Attribute => _attribute;
        #endregion

        #region Fields
        [SerializeField] protected AttributeSO _attribute;

        [Space]
        [Header("Appearance")]
        [SerializeField] private MeshRenderer[] _decoratedRenderers;
        [SerializeField] private MeshRenderer[] _postCollectionDecoratedRenderers;

        [Space]
        [SerializeField] protected Material _plainMaterialPrefab;

        [Space]
        protected Material _attributeMaterial;
        protected Material plainMaterial;
        #endregion

        #region Unity Methods
        #endregion

        #region Public Methods
        public virtual void Initialise(AttributeSO attribute)
        {
            _attribute = attribute;

            _attributeMaterial = new Material(_attribute.Material)
            {
                color = _attribute.Color
            };

            plainMaterial = new Material(_plainMaterialPrefab);

            UpdateAppearance(false);
        }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }

        public virtual void TotalReset()
        {
            UpdateAppearance(false);
        }
        #endregion

        #region Protected Methods
        public void UpdateAppearance(bool collected)
        {
            Material mat = collected ? _attributeMaterial : plainMaterial;

            foreach (MeshRenderer renderer in _postCollectionDecoratedRenderers)
            {
                renderer.material = mat;
            }

            //Some parts are decorated with 
            if(!collected)
            {
                foreach (MeshRenderer renderer in _decoratedRenderers)
                {
                    renderer.material = _attributeMaterial;
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