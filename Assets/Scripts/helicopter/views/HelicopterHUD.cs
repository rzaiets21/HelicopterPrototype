using helicopter.controllers.physics;
using helicopter.models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace helicopter.views {
    public sealed class HelicopterHUD : MonoBehaviour {
        private const float HOVER_MAX_SPEED = 0.15f;
        private const float HOVER_MAX_LIFT = 0.05f;
        private const float ASCENDING_SPEED = 0.2f;
        private const float DESCENDING_SPEED = -0.2f;
        
        [Header("Data")]
        [SerializeField] private HelicopterPhysics dynamics;

        [Header("Normal HUD")]
        [SerializeField] private Slider collectiveSlider;
        [SerializeField] private TextMeshProUGUI collectiveText;
        [SerializeField] private TextMeshProUGUI altitudeText;
        [SerializeField] private TextMeshProUGUI verticalSpeedText;
        [SerializeField] private TextMeshProUGUI horizontalSpeedText;
        [SerializeField] private TextMeshProUGUI flightStateText;

        [Header("Collective")]
        [SerializeField] private RectTransform hoverMarker;

        private void Update() {
            var data = dynamics.Telemetry;

            UpdateNormalHUD(data);
            UpdateHoverMarker(data);
        }

        private void UpdateNormalHUD(FlightTelemetry data) {
            if (collectiveSlider != null) {
                collectiveSlider.value = data.Collective;
            }

            if (collectiveText != null) {
                collectiveText.text = $"COLLECTIVE  {data.Collective * 100f:F0}%";
            }

            if (altitudeText != null) {
                altitudeText.text = $"ALTITUDE  {data.Altitude:F1} m";
            }

            if (verticalSpeedText != null) {
                verticalSpeedText.text = $"VERT SPEED  {FormatSigned(data.VerticalSpeed)} m/s";
            }

            if (horizontalSpeedText != null) {
                horizontalSpeedText.text = $"SPEED  {data.HorizontalSpeed:F1} m/s";
            }

            if (flightStateText != null) {
                flightStateText.text = GetFlightState(data);
            }
        }

        private void UpdateHoverMarker(FlightTelemetry data) {
            if (hoverMarker == null) {
                return;
            }

            var hover = data.HoverCollective;

            hoverMarker.anchorMin = new Vector2(hover, 0f);
            hoverMarker.anchorMax = new Vector2(hover, 1f);
            hoverMarker.anchoredPosition = Vector2.zero;
        }

        private static string GetFlightState(FlightTelemetry data) {
            if (Mathf.Abs(data.VerticalSpeed) < HOVER_MAX_SPEED && Mathf.Abs(data.LiftToWeightRatio - 1f) < HOVER_MAX_LIFT) {
                return "HOVER";
            }

            if (data.VerticalSpeed > ASCENDING_SPEED) {
                return "ASCENDING";
            }

            if (data.VerticalSpeed < DESCENDING_SPEED) {
                return "DESCENDING";
            }

            return "STABLE";
        }

        private static string FormatSigned(float value) {
            return value.ToString("+0.00;-0.00;0.00");
        }
    }
}
