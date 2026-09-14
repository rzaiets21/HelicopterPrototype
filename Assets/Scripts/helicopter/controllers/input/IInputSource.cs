using helicopter.models;
namespace helicopter.controllers.input {
    public interface IInputSource {
        PilotInput Read();
    }
}
