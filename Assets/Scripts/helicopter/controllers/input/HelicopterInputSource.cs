using helicopter.models;
using UnityEngine;
using UnityEngine.InputSystem;
namespace helicopter.controllers.input {
    public sealed class HelicopterInputSource : MonoBehaviour, IInputSource {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference collectiveAction;
        [SerializeField] private InputActionReference yawAction;

        private void OnEnable() {
            moveAction.action.Enable();
            collectiveAction.action.Enable();
            yawAction.action.Enable();
        }

        private void OnDisable() {
            moveAction.action.Disable();
            collectiveAction.action.Disable();
            yawAction.action.Disable();
        }

        public PilotInput Read() {
            return new PilotInput(moveAction.action.ReadValue<Vector2>(), collectiveAction.action.ReadValue<float>(), yawAction.action.ReadValue<float>());
        }
    }
}
