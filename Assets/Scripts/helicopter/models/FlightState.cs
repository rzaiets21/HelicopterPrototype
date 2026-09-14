using UnityEngine;
namespace helicopter.models {
    public readonly struct FlightState {
        public Vector3 Up => Rotation * Vector3.up;
        public Vector3 Forward => Rotation * Vector3.forward;
        public Vector3 Right => Rotation * Vector3.right;
        public float Weight => Mass * Gravity;
        
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public Vector3 Velocity { get; }
        public Vector3 AngularVelocity { get; }

        public Vector3 CenterOfMass { get; }

        public float Mass { get; }
        public float Gravity { get; }

        public FlightState(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity, Vector3 centerOfMass, float mass, float gravity) {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            CenterOfMass = centerOfMass;
            Mass = mass;
            Gravity = gravity;
        }
    }
}