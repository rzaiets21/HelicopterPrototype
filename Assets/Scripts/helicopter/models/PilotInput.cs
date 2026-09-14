using UnityEngine;
namespace helicopter.models {
    public readonly struct PilotInput {
        public Vector2 Move { get; }
        public float CollectiveAxis { get; }
        public float Yaw { get; }

        public PilotInput( Vector2 move, float collectiveAxis, float yaw) {
            Move = move;
            CollectiveAxis = collectiveAxis;
            Yaw = yaw;
        }
    }
}