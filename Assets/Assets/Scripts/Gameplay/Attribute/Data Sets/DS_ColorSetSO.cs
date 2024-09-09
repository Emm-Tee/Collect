using UnityEngine;

namespace Collect.Core.Gameplay
{
    /// <summary>
    /// For attributes whose behaviours need a selection of colours
    /// </summary>

    [CreateAssetMenu(fileName = "DS_", menuName = "Scriptable Objects/ Data Sets/ ColorCollection")]
    public class DS_ColorSetSO : AttributeDataSetSO
    {
        #region Properties
        public Color[] ColorSet => _colorSet;
        #endregion

        #region Fields
        [SerializeField] private Color[] _colorSet;
        #endregion
    }
}
