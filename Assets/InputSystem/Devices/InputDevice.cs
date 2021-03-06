using System;
using ISX.LowLevel;
using ISX.Utilities;
using UnityEngineInternal.Input;

//device callbacks:
//  - update/poll
//  - read data
//  - write data
//  - text input
//  - configuration change
//  - make current
//  - on remove (also resets current)

namespace ISX
{
    /// <summary>
    /// The root of a control hierarchy.
    /// </summary>
    /// <remarks>
    /// Input devices act as the container for control hierarchies. Every hierarchy has to have
    /// a device at the root. Devices cannot occur inside of hierarchies.
    ///
    /// Unlike other controls, usages of InputDevices are allowed to be changed on the fly
    /// without requiring a change to the device template (<see cref="InputSystem.SetUsage"/>).
    /// </remarks>
    /// \todo The entire control hierarchy should be a linear array; transition to that with InputData.
    public class InputDevice : InputControl
    {
        public const int kInvalidDeviceId = 0;
        public const int kMaxDeviceId = 256;
        internal const int kInvalidDeviceIndex = -1;

        public InputDeviceDescription description
        {
            get { return m_Description; }
        }

        ////REVIEW: on Xbox, a device can have multiple player IDs assigned to it
        ////TODO: this needs to become part of the device's configuration
        // Systems that support multiple concurrent player inputs on the same system, the available
        // player inputs are usually numbered. For example, on a console the gamepads slots on the system
        // will be numbered and associated with gamepads. This number corresponds to the system assigned
        // player index for the device.
        public int playerId
        {
            get { return m_PlayerId; }
        }

        // Whether the device is currently connected.
        // If you want to listen for state changes, hook into InputManager.onDeviceChange.
        public bool connected
        {
            get { return (m_Flags & Flags.Connected) == Flags.Connected; }
        }

        /// <summary>
        /// Whether the device is mirrored from a remote input system and not actually present
        /// as a "real" device in the local system.
        /// </summary>
        public bool remote
        {
            get { return (m_Flags & Flags.Remote) == Flags.Remote; }
        }

        /// <summary>
        /// Whether the device comes from the native Unity runtime.
        /// </summary>
        public bool native
        {
            get { return (m_Flags & Flags.Native) == Flags.Native; }
        }

        public bool updateBeforeRender
        {
            get { return (m_Flags & Flags.UpdateBeforeRender) == Flags.UpdateBeforeRender; }
        }

        // Every registered device in the system gets a unique numeric ID.
        // For native devices, this is assigned by the underlying runtime.
        public int id
        {
            get { return m_Id; }
        }

        /// <summary>
        /// Timestamp of last state event used to update the device.
        /// </summary>
        /// <remarks>
        /// Events other than <see cref="LowLevel.StateEvent"/> and <see cref="LowLevel.DeltaStateEvent"/> will
        /// not cause lastUpdateTime to be changed.
        /// </remarks>
        public double lastUpdateTime
        {
            get { return m_LastUpdateTime; }
        }

        // This has to be public for Activator.CreateInstance() to be happy.
        public InputDevice()
        {
            m_Id = kInvalidDeviceId;
            m_DeviceIndex = kInvalidDeviceIndex;
        }

        /// <summary>
        /// Make this the current device of its type.
        /// </summary>
        /// <remarks>
        /// Use this to set static properties that give fast access to the latest device used of a given
        /// type (<see cref="Gamepad.current"/> or <see cref="XRController.leftHand"/> and <see cref="XRController.rightHand"/>).
        ///
        /// This functionality is somewhat like a 'pwd' for the semantic paths but one where there can
        /// be multiple current working directories, one for each type.
        ///
        /// A device will be made current by the system initially when it is created and subsequently whenever
        /// it receives an event.
        /// </remarks>
        public virtual void MakeCurrent()
        {
        }

        public virtual void OnTextInput(char character)
        {
        }

        /// <summary>
        /// Called by the system when the configuration of the device has changed.
        /// </summary>
        /// <seealso cref="LowLevel.ConfigChangeEvent"/>
        public virtual void OnConfigurationChanged()
        {
            // Mark all controls in the hierarchy as having their config out of date.
            // We don't want to update configuration right away but rather wait until
            // someone actually depends on it.
            m_ConfigUpToDate = false;
            for (var i = 0; i < m_ChildrenForEachControl.Length; ++i)
                m_ChildrenForEachControl[i].m_ConfigUpToDate = false;
        }

        ////REVIEW: Should ReadData and WriteData() sit *behind* a different interface that would
        ////        make C# data pass through natively rather than go through memory buffers?

        public virtual int ReadData(FourCC type, IntPtr buffer, int sizeInBytes)
        {
            if (native)
                return NativeInputSystem.ReadDeviceData(id, type, buffer, sizeInBytes);

            return 0;
        }

        public virtual int WriteData(FourCC type, IntPtr buffer, int sizeInBytes)
        {
            if (native)
                return NativeInputSystem.WriteDeviceData(id, type, buffer, sizeInBytes);

            return 0;
        }

        [Flags]
        internal enum Flags
        {
            Connected = 1 << 0,
            UpdateBeforeRender = 1 << 1,
            HasAutoResetControls = 1 << 2,////TODO: remove
            Remote = 1 << 3, // It's a local mirror of a device from a remote player connection.
            Native = 1 << 4, // It's a device created from data surfaced by NativeInputSystem.
        }

        internal Flags m_Flags;
        internal int m_Id;
        internal int m_PlayerId;////TODO: move to desc
        internal int m_DeviceIndex; // Index in InputManager.m_Devices.
        internal InputDeviceDescription m_Description;

        // Time of last event we received.
        internal double m_LastUpdateTime;

        // The dynamic and fixed update count corresponding to the current
        // front buffers that are active on the device. We use this to know
        // when to flip buffers.
        internal uint m_CurrentDynamicUpdateCount;
        internal uint m_CurrentFixedUpdateCount;

        // List of aliases for all controls. Each control gets a slice of this array.
        // See 'InputControl.aliases'.
        // NOTE: The device's own aliases are part of this array as well.
        internal InternedString[] m_AliasesForEachControl;

        // List of usages for all controls. Each control gets a slice of this array.
        // See 'InputControl.usages'.
        // NOTE: The device's own usages are part of this array as well. They are always
        //       at the *end* of the array.
        internal InternedString[] m_UsagesForEachControl;
        internal InputControl[] m_UsageToControl;

        // List of children for all controls. Each control gets a slice of this array.
        // See 'InputControl.children'.
        // NOTE: The device's own children are part of this array as well.
        internal InputControl[] m_ChildrenForEachControl;

        // List of state blocks in this device that require automatic resetting
        // between frames.
        internal InputStateBlock[] m_AutoResetStateBlocks;

        ////TODO: output is still in the works
        // Buffer that will receive state events for output generated from this device.
        // May be shared with other devices.
        internal InputEventBuffer m_OutputBuffer;

        // NOTE: We don't store processors in a combined array the same way we do for
        //       usages and children as that would require lots of casting from 'object'.

        internal void SetUsage(InternedString usage)
        {
            // Make last entry in m_UsagesForEachControl be our device usage string.
            var numControlUsages = m_UsageToControl != null ? m_UsageToControl.Length : 0;
            Array.Resize(ref m_UsagesForEachControl, numControlUsages + 1);
            m_UsagesForEachControl[numControlUsages] = usage;
            m_UsagesReadOnly = new ReadOnlyArray<InternedString>(m_UsagesForEachControl, numControlUsages, 1);

            // Update controls to all point to new usage array.
            UpdateUsageArraysOnControls();
        }

        internal void UpdateUsageArraysOnControls()
        {
            if (m_UsageToControl == null)
                return;

            for (var i = 0; i < m_UsageToControl.Length; ++i)
                m_UsageToControl[i].m_UsagesReadOnly.m_Array = m_UsagesForEachControl;
        }
    }
}
