using helicopter.configs;
using helicopter.models;
using UnityEngine;
namespace helicopter.controllers.control {
    public sealed class YawController {
        private readonly YawSettings _settings;

        public YawController(YawSettings settings) {
            _settings = settings;
        }

        public Vector3 CalculateTorque(in FlightState state, FlightControlCommand command) {
            var targetYawRate = command.Yaw * _settings.MaxRate * Mathf.Deg2Rad;
            var currentYawRate = Vector3.Dot(state.AngularVelocity, state.Up);
            var error = targetYawRate - currentYawRate;
            var torqueMagnitude = error * _settings.RateStrength;
            torqueMagnitude = Mathf.Clamp(torqueMagnitude, -_settings.MaxTorque, _settings.MaxTorque);

            return state.Up * torqueMagnitude;
        }
    }
}