using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class CollectiveSettings {
        public float HoverCollective => hoverCollective;
        public float ChangeSpeed => changeSpeed;
        public float MaxLiftToWeightRatio => maxLiftToWeightRatio;

        [SerializeField, Range(0.1f, 1f)] private float hoverCollective;
        [SerializeField] private float changeSpeed;
        [SerializeField] private float maxLiftToWeightRatio;
    }
}