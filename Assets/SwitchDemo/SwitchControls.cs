// GENERATED AUTOMATICALLY FROM 'Assets/SwitchDemo/SwitchControls.inputactions'

[System.Serializable]
public class SwitchControls : UnityEngine.Experimental.Input.InputActionWrapper
{
    private bool m_Initialized;
    private void Initialize()
    {
        // gameplay
        m_gameplay = asset.GetActionSet("gameplay");
        m_gameplay_attitude = m_gameplay.GetAction("attitude");
        m_gameplay_acceleration = m_gameplay.GetAction("acceleration");
        m_gameplay_velocity = m_gameplay.GetAction("velocity");
        m_Initialized = true;
    }
    // gameplay
    private UnityEngine.Experimental.Input.InputActionSet m_gameplay;
    private UnityEngine.Experimental.Input.InputAction m_gameplay_attitude;
    private UnityEngine.Experimental.Input.InputAction m_gameplay_acceleration;
    private UnityEngine.Experimental.Input.InputAction m_gameplay_velocity;
    public struct GameplayActions
    {
        private SwitchControls m_Wrapper;
        public GameplayActions(SwitchControls wrapper) { m_Wrapper = wrapper; }
        public UnityEngine.Experimental.Input.InputAction @attitude { get { return m_Wrapper.m_gameplay_attitude; } }
        public UnityEngine.Experimental.Input.InputAction @acceleration { get { return m_Wrapper.m_gameplay_acceleration; } }
        public UnityEngine.Experimental.Input.InputAction @velocity { get { return m_Wrapper.m_gameplay_velocity; } }
        public UnityEngine.Experimental.Input.InputActionSet Get() { return m_Wrapper.m_gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public UnityEngine.Experimental.Input.InputActionSet Clone() { return Get().Clone(); }
        public static implicit operator UnityEngine.Experimental.Input.InputActionSet(GameplayActions set) { return set.Get(); }
    }
    public GameplayActions @gameplay
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new GameplayActions(this);
        }
    }
}
