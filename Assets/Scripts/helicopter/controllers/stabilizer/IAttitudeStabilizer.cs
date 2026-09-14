using helicopter.models;
using UnityEngine;
namespace helicopter.controllers.stabilizer {
    public interface IAttitudeStabilizer {
        Vector3 CalculateTorque(in FlightState state, FlightControlCommand command);
    }
}
