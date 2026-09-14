using System;
using UnityEngine;
namespace helicopter.configs {
    [Serializable]
    public sealed class DragSettings {
        public Vector3 Coefficients => coefficients;

        [Tooltip("X = lateral, Y = vertical, Z = longitudinal")]
        [SerializeField] private Vector3 coefficients;
    }
}