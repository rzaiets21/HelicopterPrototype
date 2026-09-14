using helicopter.configs;
using UnityEngine;
namespace helicopter.models {
    public sealed class GroundEffectModel {
        private readonly GroundEffectSettings _settings;
        private readonly LayerMask _groundMask;

        public GroundEffectModel(GroundEffectSettings settings, LayerMask groundMask) {
            _settings = settings;
            _groundMask = groundMask;
        }

        public float CalculateMultiplier(Vector3 rotorPosition) {
            if (!_settings.Enabled) {
                return 1f;
            }

            var hasGround = Physics.Raycast(rotorPosition, Vector3.down, out RaycastHit hit, _settings.Height, _groundMask, QueryTriggerInteraction.Ignore);
            if (!hasGround) {
                return 1f;
            }

            var normalizedDistance = Mathf.Clamp01(hit.distance / _settings.Height);
            var influence = 1f - normalizedDistance;

            return 1f + influence * _settings.Strength;
        }
    }
}