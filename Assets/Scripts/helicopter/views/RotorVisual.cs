using UnityEngine;
namespace helicopter.views {
    public sealed class RotorVisual : MonoBehaviour {
        private const float DEGREES_RPM = 6f;

        [SerializeField] private Vector3 localAxis;
        [SerializeField] private float rpm;

        private void Update() {
            var degreesPerSecond = rpm * DEGREES_RPM;
            transform.Rotate(localAxis, degreesPerSecond * Time.deltaTime, Space.Self);
        }
    }
}