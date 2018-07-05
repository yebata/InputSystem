using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Controls;
using UnityEngine.Experimental.Input.Plugins.OnScreen;

public class OnScreenTests : InputTestFixture
{
    [Test]
    [Category("Devices")]
    public void Devices_CanCreateOnScreenStick()
    {
        var gameObject = new GameObject();
        var eventSystem = gameObject.AddComponent<EventSystem>();

        var stick = gameObject.AddComponent<OnScreenStick>();
        stick.controlPath = "/<Gamepad>/leftStick";

        Assert.That(stick.control.device, Is.TypeOf<Gamepad>());
        Assert.That(stick.control, Is.SameAs(stick.control.device["leftStick"]));
        Assert.That(stick.control, Is.TypeOf<StickControl>());
        var stickControl = (StickControl)stick.control;

        ////FIXME: OnScreenStick mixes up transform.position and 2D position space

        stick.OnDrag(new PointerEventData(eventSystem)
        {
            position = new Vector2(stick.movementRange, stick.movementRange)
        });

        InputSystem.Update();

        Assert.That(stick.control.ReadValueAsObject(),
            Is.EqualTo(stickControl.Process(new Vector2(stick.movementRange / 2f, stick.movementRange / 2)))
            .Using(vector2Comparer));

        ////REVIEW: check transform?
    }

    [Test]
    [Category("Devices")]
    public void Devices_CanCreateOnScreenButton()
    {
        var gameObject = new GameObject();
        var button = gameObject.AddComponent<OnScreenButton>();
        button.controlPath = "/<Keyboard>/a";

        Assert.That(InputSystem.devices, Has.Exactly(1).TypeOf<Keyboard>());
        var keyboard = (Keyboard)InputSystem.devices.FirstOrDefault(x => x is Keyboard);

        Assert.That(keyboard.aKey.isPressed, Is.False);

        button.OnPointerDown(null);
        InputSystem.Update();
        Assert.That(keyboard.aKey.isPressed, Is.True);

        button.OnPointerUp(null);
        InputSystem.Update();
        Assert.That(keyboard.aKey.isPressed, Is.False);
    }

    ////TODO: we should allow this as an optional feature
    [Test]
    [Category("Devices")]
    public void Devices_OnScreenControlsDoNotUseExistingDevices()
    {
        var existingKeyboard = InputSystem.AddDevice<Keyboard>();
        var gameObject = new GameObject();
        var button = gameObject.AddComponent<OnScreenButton>();
        button.controlPath = "/<Keyboard>/a";

        Assert.That(existingKeyboard.aKey.isPressed, Is.False);
        button.OnPointerDown(null);
        InputSystem.Update();
        Assert.That(existingKeyboard.aKey.isPressed, Is.False);
    }

    [Test]
    [Category("Devices")]
    public void Devices_OnScreenControlsShareDevicesOfTheSameType()
    {
        var gameObject = new GameObject();
        var aKey = gameObject.AddComponent<OnScreenButton>();
        var bKey = gameObject.AddComponent<OnScreenButton>();
        var leftTrigger = gameObject.AddComponent<OnScreenButton>();

        aKey.controlPath = "/<Keyboard>/a";
        bKey.controlPath = "/<Keyboard>/b";
        leftTrigger.controlPath = "/<Gamepad>/leftTrigger";

        Assert.That(aKey.control.device, Is.SameAs(bKey.control.device));
        Assert.That(aKey.control.device, Is.Not.SameAs(leftTrigger.control.device));
        Assert.That(bKey.control.device, Is.Not.SameAs(leftTrigger.control.device));
    }
}
