// GENERATED AUTOMATICALLY FROM 'Assets/Demo/Switch/SwitchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Utilities;

public class SwitchControls : IInputActionCollection
{
    private InputActionAsset asset;
    public SwitchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SwitchControls"",
    ""maps"": [
        {
            ""name"": ""gameplay"",
            ""id"": ""64d5aa8d-2702-47ab-a288-168e999b0830"",
            ""actions"": [
                {
                    ""name"": ""Attitude"",
                    ""id"": ""67112aca-c946-4deb-a349-4273b2e3d3be"",
                    ""expectedControlLayout"": ""Quaternion"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": ""CompensateRotation"",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Acceleration"",
                    ""id"": ""445c8f0e-9881-43b7-b55a-f4be433641ee"",
                    ""expectedControlLayout"": ""Vector3"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Velocity"",
                    ""id"": ""2c822354-99d0-42fc-9797-0c78d090ff29"",
                    ""expectedControlLayout"": ""Vector3"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Gyro"",
                    ""id"": ""16c7932a-8f11-43c6-99ce-154bdf04aab5"",
                    ""expectedControlLayout"": """",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""TouchScreen"",
                    ""id"": ""7d2843f7-6976-434d-877f-3170b5e43c22"",
                    ""expectedControlLayout"": ""Touch"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""New action"",
                    ""id"": ""544aec78-a6a1-4b4d-8567-ce0612755616"",
                    ""expectedControlLayout"": """",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ac8879e0-5daf-4ba8-beb2-fb5a8753a5aa"",
                    ""path"": ""<NPad>/attitude"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attitude"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""32b37442-3a59-4e81-8bea-11ba7ba1995f"",
                    ""path"": ""<NPad>/acceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Acceleration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""b07f56fe-6e9b-435a-b332-2bb5cb2ee95d"",
                    ""path"": ""<NPad>/angularVelocity"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Velocity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""2b14ddbd-473b-4942-898c-a0f4e7500e15"",
                    ""path"": ""<Touchscreen>/touch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""7fd738cf-608f-4e4f-bd59-c41142ff4868"",
                    ""path"": ""*/{Accept}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""basedOn"": """",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // gameplay
        m_gameplay = asset.GetActionMap("gameplay");
        m_gameplay_Attitude = m_gameplay.GetAction("Attitude");
        m_gameplay_Acceleration = m_gameplay.GetAction("Acceleration");
        m_gameplay_Velocity = m_gameplay.GetAction("Velocity");
        m_gameplay_Gyro = m_gameplay.GetAction("Gyro");
        m_gameplay_TouchScreen = m_gameplay.GetAction("TouchScreen");
        m_gameplay_Newaction = m_gameplay.GetAction("New action");
    }
    ~SwitchControls()
    {
        UnityEngine.Object.Destroy(asset);
    }
    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }
    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }
    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }
    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }
    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public void Enable()
    {
        asset.Enable();
    }
    public void Disable()
    {
        asset.Disable();
    }
    // gameplay
    private InputActionMap m_gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private InputAction m_gameplay_Attitude;
    private InputAction m_gameplay_Acceleration;
    private InputAction m_gameplay_Velocity;
    private InputAction m_gameplay_Gyro;
    private InputAction m_gameplay_TouchScreen;
    private InputAction m_gameplay_Newaction;
    public struct GameplayActions
    {
        private SwitchControls m_Wrapper;
        public GameplayActions(SwitchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attitude { get { return m_Wrapper.m_gameplay_Attitude; } }
        public InputAction @Acceleration { get { return m_Wrapper.m_gameplay_Acceleration; } }
        public InputAction @Velocity { get { return m_Wrapper.m_gameplay_Velocity; } }
        public InputAction @Gyro { get { return m_Wrapper.m_gameplay_Gyro; } }
        public InputAction @TouchScreen { get { return m_Wrapper.m_gameplay_TouchScreen; } }
        public InputAction @Newaction { get { return m_Wrapper.m_gameplay_Newaction; } }
        public InputActionMap Get() { return m_Wrapper.m_gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Attitude.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttitude;
                Attitude.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttitude;
                Attitude.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttitude;
                Acceleration.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAcceleration;
                Acceleration.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAcceleration;
                Acceleration.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAcceleration;
                Velocity.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVelocity;
                Velocity.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVelocity;
                Velocity.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVelocity;
                Gyro.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGyro;
                Gyro.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGyro;
                Gyro.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGyro;
                TouchScreen.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchScreen;
                TouchScreen.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchScreen;
                TouchScreen.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchScreen;
                Newaction.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNewaction;
                Newaction.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNewaction;
                Newaction.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Attitude.started += instance.OnAttitude;
                Attitude.performed += instance.OnAttitude;
                Attitude.cancelled += instance.OnAttitude;
                Acceleration.started += instance.OnAcceleration;
                Acceleration.performed += instance.OnAcceleration;
                Acceleration.cancelled += instance.OnAcceleration;
                Velocity.started += instance.OnVelocity;
                Velocity.performed += instance.OnVelocity;
                Velocity.cancelled += instance.OnVelocity;
                Gyro.started += instance.OnGyro;
                Gyro.performed += instance.OnGyro;
                Gyro.cancelled += instance.OnGyro;
                TouchScreen.started += instance.OnTouchScreen;
                TouchScreen.performed += instance.OnTouchScreen;
                TouchScreen.cancelled += instance.OnTouchScreen;
                Newaction.started += instance.OnNewaction;
                Newaction.performed += instance.OnNewaction;
                Newaction.cancelled += instance.OnNewaction;
            }
        }
    }
    public GameplayActions @gameplay
    {
        get
        {
            return new GameplayActions(this);
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnAttitude(InputAction.CallbackContext context);
        void OnAcceleration(InputAction.CallbackContext context);
        void OnVelocity(InputAction.CallbackContext context);
        void OnGyro(InputAction.CallbackContext context);
        void OnTouchScreen(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
    }
}
