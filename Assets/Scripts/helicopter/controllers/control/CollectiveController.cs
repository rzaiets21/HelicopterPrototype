using UnityEngine;
namespace helicopter.controllers.control {
    public sealed class CollectiveController {
        public float Value { get; private set; }

        private readonly float _changeSpeed;

        public CollectiveController(float initialValue, float changeSpeed) {
            Value = Mathf.Clamp01(initialValue);
            _changeSpeed = changeSpeed;
        }

        public void Update(float axis, float deltaTime) {
            Value += axis * _changeSpeed * deltaTime;
            Value = Mathf.Clamp01(Value);
        }
    }
}
