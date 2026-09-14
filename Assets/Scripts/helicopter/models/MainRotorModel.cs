using helicopter.configs;
using UnityEngine;
namespace helicopter.models {
    public sealed class MainRotorModel {
        private readonly CollectiveSettings _settings;
        private readonly GroundEffectModel _groundEffect;
        private readonly MoveSettings _moveSettings;

        public MainRotorModel(CollectiveSettings settings, GroundEffectModel groundEffect, MoveSettings moveSettings) {
            _settings = settings;
            _groundEffect = groundEffect;
            _moveSettings = moveSettings;
        }

        public Vector3 CalculateForce(in FlightState state, FlightControlCommand command, Vector3 rotorPosition) {
            var thrustRatio = command.Collective / Mathf.Max(_settings.HoverCollective, 0.01f);
            thrustRatio = Mathf.Clamp(thrustRatio, 0f, _settings.MaxLiftToWeightRatio);

            var thrustMagnitude = state.Weight * thrustRatio;
            thrustMagnitude *= _groundEffect.CalculateMultiplier(rotorPosition);

            var thrustDirection = CalculateThrustDirection(state, command);
            return thrustDirection * thrustMagnitude;
        }

        private Vector3 CalculateThrustDirection(in FlightState state, FlightControlCommand command) {
            var pitchAngle = command.Pitch * _moveSettings.MaxPitchAngle;
            var rollAngle = command.Roll * _moveSettings.MaxRollAngle;
            var pitchRotation = Quaternion.AngleAxis(pitchAngle, state.Right);
            var rollRotation = Quaternion.AngleAxis(-rollAngle, state.Forward);
            var direction = rollRotation * pitchRotation * state.Up;

            return direction.normalized;
        }
    }
}
