namespace helicopter.models {
    public readonly struct FlightTelemetry {
        public float LiftToWeightRatio => Weight > 0f ? Lift / Weight : 0f;
        
        public float Collective { get; }
        public float HoverCollective { get; }

        public float Lift { get; }
        public float Weight { get; }

        public float Altitude { get; }

        public float VerticalSpeed { get; }
        public float HorizontalSpeed { get; }

        public FlightTelemetry(float collective, float hoverCollective, float lift, float weight, float altitude, float verticalSpeed, float horizontalSpeed) {
            Collective = collective;
            HoverCollective = hoverCollective;

            Lift = lift;
            Weight = weight;

            Altitude = altitude;

            VerticalSpeed = verticalSpeed;
            HorizontalSpeed = horizontalSpeed;
        }
    }
}