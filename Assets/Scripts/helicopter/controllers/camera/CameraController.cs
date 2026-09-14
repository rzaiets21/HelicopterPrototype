using UnityEngine;
namespace helicopter.controllers.camera {
    public sealed class CameraController : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private float distance;
        [SerializeField] private float height;
        [SerializeField] private float lookHeight;
        [SerializeField] private float positionSmoothTime;
        [SerializeField] private float rotationSpeed;

        private Vector3 _velocity;

        private void LateUpdate() {
            if (target == null) {
                return;
            }

            var forward = Vector3.ProjectOnPlane(target.forward, Vector3.up);

            if (forward.sqrMagnitude < 0.001f) {
                forward = Vector3.forward;
            }

            forward.Normalize();

            var targetPosition = target.position - forward * distance + Vector3.up * height;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, positionSmoothTime);

            var lookPoint = target.position + Vector3.up * lookHeight;
            var targetRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}