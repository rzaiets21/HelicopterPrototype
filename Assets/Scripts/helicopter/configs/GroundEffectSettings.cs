using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class GroundEffectSettings {
        public bool Enabled => enabled;
        public float Height => height;
        public float Strength => strength;
        public float AltitudeProbeDistance => altitudeProbeDistance;
        
        [SerializeField] private bool enabled;
        [SerializeField] private float height;
        [SerializeField, Range(0f, 1f)] private float strength;
        [SerializeField] private float altitudeProbeDistance;
    }
}