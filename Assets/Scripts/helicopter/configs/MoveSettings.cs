using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class MoveSettings {
        public float MaxPitchAngle => maxPitchAngle;
        public float MaxRollAngle => maxRollAngle;
        
        [SerializeField, Min(0f)] private float maxPitchAngle;
        [SerializeField, Min(0f)] private float maxRollAngle;
    }
}