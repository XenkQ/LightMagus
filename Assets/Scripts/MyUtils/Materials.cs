using Unity.VisualScripting;
using UnityEngine;

namespace MyUtils
{
    public static class Materials
    {
        public static Material[] GetAllMaterialsFromChildrens(GameObject parent)
        {
            var renderers = parent.GetComponentsInChildren<MeshRenderer>();
            var materials = new Material[renderers.Length];
        
            for (int i = 0; i < renderers.Length; i++)
                materials[i] = renderers[i].material;

            return materials;
        }

        public static void ChangeMaterialsMetalicMapValue(Material[] materials, float newVal)
        {
            newVal = Mathf.Clamp(newVal, 0, 1f);

            foreach (var material in materials)
            {
                Debug.Log(material.name + "| |" + material.GetFloat("Metallic Map"));
                material.SetFloat("Metallic Map", newVal);
            }
        }
    }
}