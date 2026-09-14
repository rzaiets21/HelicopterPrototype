using System;
using helicopter.controllers.control;
using helicopter.controllers.input;
using helicopter.controllers.physics;
using helicopter.models;
using UnityEngine;
namespace helicopter.controllers {
    public sealed class HelicopterController : MonoBehaviour {
        private IInputSource _input;
        private HelicopterPhysics _physics;
        private CollectiveController _collective;
        private bool _initialized;

        public void Initialize(IInputSource input, HelicopterPhysics dynamics, CollectiveController collective) {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _physics = dynamics ?? throw new ArgumentNullException(nameof(dynamics));
            _collective = collective ?? throw new ArgumentNullException(nameof(collective));
            _initialized = true;
        }

        private void FixedUpdate() {
            if (!_initialized) {
                return;
            }
            
            var input = _input.Read();
            
            _collective.Update(input.CollectiveAxis, Time.fixedDeltaTime);

            var command = new FlightControlCommand(input.Move.y, input.Move.x, input.Yaw, _collective.Value);
            _physics.Simulate(command);
        }
    }
}
