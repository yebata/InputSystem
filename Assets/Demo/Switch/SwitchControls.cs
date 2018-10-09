// GENERATED AUTOMATICALLY FROM 'Assets/Demo/Switch/SwitchControls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class SwitchControls : InputActionAssetReference
{
    public SwitchControls()
    {
    }
    public SwitchControls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // gameplay
        m_gameplay = asset.GetActionMap("gameplay");
        m_gameplay_attitude = m_gameplay.GetAction("attitude");
        m_gameplay_acceleration = m_gameplay.GetAction("acceleration");
        m_gameplay_velocity = m_gameplay.GetAction("velocity");
        m_gameplay_gyro = m_gameplay.GetAction("gyro");
        // touchscreen
        m_touchscreen = asset.GetActionMap("touchscreen");
        m_touchscreen_position = m_touchscreen.GetAction("position");
        m_touchscreen_id = m_touchscreen.GetAction("id");
        m_touchscreen_phase = m_touchscreen.GetAction("phase");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_gameplay = null;
        m_gameplay_attitude = null;
        m_gameplay_acceleration = null;
        m_gameplay_velocity = null;
        m_gameplay_gyro = null;
        m_touchscreen = null;
        m_touchscreen_position = null;
        m_touchscreen_id = null;
        m_touchscreen_phase = null;
        m_Initialized = false;
    }
    public void SwitchAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public void DuplicateAndSwitchAsset()
    {
        SwitchAsset(ScriptableObject.Instantiate(asset));
    }
    // gameplay
    private InputActionMap m_gameplay;
    private InputAction m_gameplay_attitude;
    private InputAction m_gameplay_acceleration;
    private InputAction m_gameplay_velocity;
    private InputAction m_gameplay_gyro;
    public struct GameplayActions
    {
        private SwitchControls m_Wrapper;
        public GameplayActions(SwitchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @attitude { get { return m_Wrapper.m_gameplay_attitude; } }
        public InputAction @acceleration { get { return m_Wrapper.m_gameplay_acceleration; } }
        public InputAction @velocity { get { return m_Wrapper.m_gameplay_velocity; } }
        public InputAction @gyro { get { return m_Wrapper.m_gameplay_gyro; } }
        public InputActionMap Get() { return m_Wrapper.m_gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
    }
    public GameplayActions @gameplay
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new GameplayActions(this);
        }
    }
    // touchscreen
    private InputActionMap m_touchscreen;
    private InputAction m_touchscreen_position;
    private InputAction m_touchscreen_id;
    private InputAction m_touchscreen_phase;
    public struct TouchscreenActions
    {
        private SwitchControls m_Wrapper;
        public TouchscreenActions(SwitchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @position { get { return m_Wrapper.m_touchscreen_position; } }
        public InputAction @id { get { return m_Wrapper.m_touchscreen_id; } }
        public InputAction @phase { get { return m_Wrapper.m_touchscreen_phase; } }
        public InputActionMap Get() { return m_Wrapper.m_touchscreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(TouchscreenActions set) { return set.Get(); }
    }
    public TouchscreenActions @touchscreen
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new TouchscreenActions(this);
        }
    }
}
