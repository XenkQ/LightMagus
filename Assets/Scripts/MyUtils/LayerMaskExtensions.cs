using UnityEngine;

namespace MyUtils
{
    public static class LayerMaskExtensions
    {
        public static bool IsContainingLayer(this LayerMask layerMask, int layer)
            => (layerMask & (1 << layer)) != 0;
    }
}
