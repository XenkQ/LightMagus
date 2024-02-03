using UnityEngine;

namespace MyUtils
{
    public static class LineRendererExtensions
    {
        public static void AddNewPoint(this LineRenderer lineRenderer, Vector3 pos)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
        }
    }
}