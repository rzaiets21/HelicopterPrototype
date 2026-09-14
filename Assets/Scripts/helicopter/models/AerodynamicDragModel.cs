using helicopter.configs;
using UnityEngine;
namespace helicopter.models {
    public sealed class AerodynamicDragModel {
        private readonly DragSettings _settings;

        public AerodynamicDragModel(DragSettings settings) {
            _settings = settings;
        }

        public Vector3 Calculate(in FlightState state) {
            var inverse = Quaternion.Inverse(state.Rotation);
            var localVelocity = inverse * state.Velocity;
            var coefficients = _settings.Coefficients;
            var drag = new Vector3(CalculateAxis(localVelocity.x, coefficients.x),
                    CalculateAxis(localVelocity.y, coefficients.y),
                    CalculateAxis(localVelocity.z, coefficients.z));

            return state.Rotation * drag;
        }

        private static float CalculateAxis(float velocity, float coefficient) {
            return -velocity * Mathf.Abs(velocity) * coefficient;
        }
    }
}