using UnityEngine;

namespace MyUtils
{
    public static class Layer
    {
        public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
            => (layerMask & (1 << layer)) != 0;
    }
}
