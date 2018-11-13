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
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_gameplay = null;
        m_gameplay_attitude = null;
        m_gameplay_acceleration = null;
        m_gameplay_velocity = null;
        m_gameplay_gyro = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
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
        public bool enabled { get { return Get().enabled; } }
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
}
