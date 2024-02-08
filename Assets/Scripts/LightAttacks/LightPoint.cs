using System;
using UnityEngine;

namespace LightAttacks
{
    public class LightPoint : MonoBehaviour, ILightConnectable, ISelfDestroyable
    {
        public bool CanConnect { get; set; } = true;
        public ILightConnectable IsConnectedTo { get; set; }
        public Vector3 CurrentPosition { get; private set; }

        public void DestroySelf() => Destroy(this.gameObject);

        private void OnEnable()
            => CurrentPosition = transform.position;
    }
}