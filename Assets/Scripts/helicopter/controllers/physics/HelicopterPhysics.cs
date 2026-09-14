using System;
using helicopter.configs;
using helicopter.controllers.control;
using helicopter.controllers.stabilizer;
using helicopter.models;
using UnityEngine;
namespace helicopter.controllers.physics {
    [RequireComponent(typeof(Rigidbody))]
    public sealed class HelicopterPhysics : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Rigidbody body;
        [SerializeField] private Transform centerOfMass;
        [SerializeField] private Transform mainRotor;

        [Header("Environment")]
        [SerializeField] private LayerMask groundMask;

        private HelicopterFlightConfig _config;
        private MainRotorModel _mainRotorModel;
        private AerodynamicDragModel _dragModel;
        private IAttitudeStabilizer _attitudeController;
        private YawController _yawController;
        private bool _initialized;

        public FlightTelemetry Telemetry { get; private set; }

        public void Initialize(HelicopterFlightConfig config, IAttitudeStabilizer attitudeController, YawController yawController) {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _attitudeController = attitudeController ?? throw new ArgumentNullException(nameof(attitudeController));
            _yawController = yawController ?? throw new ArgumentNullException(nameof(yawController));

            if (body == null) {
                body = GetComponent<Rigidbody>();
            }

            ConfigureCenterOfMass();

            var groundEffect = new GroundEffectModel(_config.GroundEffect, groundMask);
            
            _mainRotorModel = new MainRotorModel(_config.Collective, groundEffect, _config.Move);
            _dragModel = new AerodynamicDragModel(_config.Drag);

            _initialized = true;
        }

        public void Simulate(FlightControlCommand command) {
            if (!_initialized) {
                return;
            }

            var state = CaptureState();

            var liftForce = _mainRotorModel.CalculateForce(state, command, GetMainRotorPosition());
            var dragForce = _dragModel.Calculate(state);
            var attitudeTorque = _attitudeController.CalculateTorque(state, command);
            var yawTorque = _yawController.CalculateTorque(state, command);

            ApplyForces(liftForce, dragForce, attitudeTorque, yawTorque);
            UpdateTelemetry(state, command, liftForce.magnitude);
        }

        private FlightState CaptureState() => new FlightState(body.position, body.rotation, body.linearVelocity, body.angularVelocity, body.worldCenterOfMass, body.mass, Physics.gravity.magnitude);

        private void ApplyForces(Vector3 liftForce, Vector3 dragForce, Vector3 attitudeTorque, Vector3 yawTorque) {
            body.AddForceAtPosition(liftForce, GetMainRotorPosition(), ForceMode.Force);
            body.AddForce(dragForce, ForceMode.Force);
            body.AddTorque(attitudeTorque + yawTorque, ForceMode.Force);
        }

        private void ConfigureCenterOfMass() {
            if (centerOfMass == null) {
                return;
            }

            body.centerOfMass = transform.InverseTransformPoint(centerOfMass.position);
        }

        private Vector3 GetMainRotorPosition() => mainRotor != null ? mainRotor.position : body.worldCenterOfMass;

        private void UpdateTelemetry(in FlightState state, FlightControlCommand command, float currentLift) {
            var horizontalVelocity = Vector3.ProjectOnPlane(state.Velocity, Vector3.up);
            var verticalSpeed = Vector3.Dot(state.Velocity, Vector3.up);
            
            Telemetry = new FlightTelemetry(command.Collective, _config.Collective.HoverCollective, currentLift, state.Weight, CalculateAltitude(state), 
                verticalSpeed, horizontalVelocity.magnitude);
        }

        private float CalculateAltitude(in FlightState state) {
            var hasGround = Physics.Raycast(state.CenterOfMass, Vector3.down, out RaycastHit hit, _config.GroundEffect.AltitudeProbeDistance, groundMask, QueryTriggerInteraction.Ignore);
            return hasGround ? hit.distance : state.Position.y;
        }
    }
}