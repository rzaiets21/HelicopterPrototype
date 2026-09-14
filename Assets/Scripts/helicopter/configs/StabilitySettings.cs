using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class StabilitySettings {
        public float Strength => strength;
        public float Damping => damping;
        public float MaxTorque => maxTorque;
        
        [SerializeField] private float strength;
        [SerializeField] private float damping;
        [SerializeField] private float maxTorque;
    }
}