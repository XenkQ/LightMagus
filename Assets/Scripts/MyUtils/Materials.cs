using UnityEngine;

namespace MyUtils
{
    public static class Materials
    {
        public static Material[] GetMaterialsFromChildrens(GameObject parent)
        {
            var renderers = parent.GetComponentsInChildren<MeshRenderer>();
            var materials = new Material[renderers.Length];
        
            for (int i = 0; i < renderers.Length; i++)
                materials[i] = renderers[i].material;

            return materials;
        }

        public static void ChangeMaterialsMetallicMapValue(Material[] materials, float newVal)
        {
            newVal = Mathf.Clamp(newVal, 0, 1f);

            foreach (var material in materials)
            {
                material.SetFloat("_Metallic", newVal);
            }
        }
    }
}