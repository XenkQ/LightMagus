using System;
using UnityEngine;

namespace LightAttacks
{
    public class LightPoint : MonoBehaviour, ILightConnectable
    {
        public bool CanConnect { get; set; } = true;
        public ILightConnectable IsConnectedTo { get; set; }
        public Vector3 CurrentPosition { get; private set; }

        private void OnEnable()
            => CurrentPosition = transform.position;
    }
}