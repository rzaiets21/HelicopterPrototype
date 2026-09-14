namespace helicopter.models {
    public readonly struct FlightControlCommand {
        public float Pitch { get; }
        public float Roll { get; }
        public float Yaw { get; }
        public float Collective { get; }

        public FlightControlCommand(float pitch, float roll, float yaw, float collective) {
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
            Collective = collective;
        }
    }
}