using helicopter.configs;
using helicopter.models;
using UnityEngine;
namespace helicopter.controllers.stabilizer {
    public sealed class AttitudeStabilizer : IAttitudeStabilizer {
        private const float MAGNITUDE_THRESHOLD = 0.001f;
        
        private readonly StabilitySettings _settings;

        public AttitudeStabilizer(StabilitySettings settings) {
            _settings = settings;
        }

        public Vector3 CalculateTorque(in FlightState state, FlightControlCommand command) {
            var desiredUp = CalculateDesiredUp(state, command);
            var attitudeError = Vector3.Cross(state.Up, desiredUp);
            var yawAngularVelocity = Vector3.Project(state.AngularVelocity, state.Up);
            var tiltAngularVelocity = state.AngularVelocity - yawAngularVelocity;
            var proportional = attitudeError * _settings.Strength;
            var derivative = -tiltAngularVelocity * _settings.Damping;

            return Vector3.ClampMagnitude(proportional + derivative, _settings.MaxTorque);
        }

        private static Vector3 CalculateDesiredUp(in FlightState state, FlightControlCommand command) {
            var flatForward = Vector3.ProjectOnPlane(state.Forward, Vector3.up);

            if (flatForward.sqrMagnitude < MAGNITUDE_THRESHOLD) {
                flatForward = Vector3.forward;
            }

            flatForward.Normalize();
            var flatRight = Vector3.Cross(Vector3.up, flatForward).normalized;
            var pitchRotation = Quaternion.AngleAxis(command.Pitch, flatRight);
            var rollRotation = Quaternion.AngleAxis(-command.Roll, flatForward);

            return rollRotation * pitchRotation * Vector3.up;
        }
    }
}