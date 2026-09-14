using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class YawSettings {
        public float MaxRate => maxRate;
        public float RateStrength => rateStrength;
        public float MaxTorque => maxTorque;
        
        [SerializeField] private float maxRate;
        [SerializeField] private float rateStrength;
        [SerializeField] private float maxTorque;
    }
}