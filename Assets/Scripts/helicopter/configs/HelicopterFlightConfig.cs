using UnityEngine;
namespace helicopter.configs {
    [CreateAssetMenu(fileName = "HelicopterFlightConfig", menuName = "Helicopter/Flight Config")]
    public sealed class HelicopterFlightConfig : ScriptableObject {
        public CollectiveSettings Collective => collective;
        public StabilitySettings Stability => stability;
        public MoveSettings Move => move;
        public YawSettings Yaw => yaw;
        public DragSettings Drag => drag;
        public GroundEffectSettings GroundEffect => groundEffect;
        
        [SerializeField] private CollectiveSettings collective;
        [SerializeField] private StabilitySettings stability;
        [SerializeField] private MoveSettings move;
        [SerializeField] private YawSettings yaw;
        [SerializeField] private DragSettings drag;
        [SerializeField] private GroundEffectSettings groundEffect;
    }
}
