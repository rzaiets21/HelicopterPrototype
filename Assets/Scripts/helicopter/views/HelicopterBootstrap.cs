using helicopter.configs;
using helicopter.controllers;
using helicopter.controllers.control;
using helicopter.controllers.input;
using helicopter.controllers.physics;
using helicopter.controllers.stabilizer;
using UnityEngine;
namespace helicopter.views {
    public class HelicopterBootstrap : MonoBehaviour {
        [Header("Scene References")]
        [SerializeField] private HelicopterInputSource inputSource;
        [SerializeField] private HelicopterController controller;
        [SerializeField] private HelicopterPhysics physics;

        [Header("Configuration")]
        [SerializeField] private HelicopterFlightConfig config;

        [Header("Initial State")]
        [SerializeField, Range(0f, 1f)] private float initialCollective;

        private void Awake() {
            var attitudeController = new AttitudeStabilizer(config.Stability);
            var yawController = new YawController(config.Yaw);
            var collectiveController = new CollectiveController(initialCollective, config.Collective.ChangeSpeed);

            physics.Initialize(config, attitudeController, yawController);
            controller.Initialize(inputSource, physics, collectiveController);
        }
    }
}