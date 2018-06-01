using System.Collections;
using UnityEngine.Experimental.Input.Modifiers;
using UnityEngine;
using UnityEngine.Experimental.Input.Plugins.Switch;
using UnityEngine.UI;

////TODO: transform from using action callbacks to using events
////TODO: transform previous form that went to 'onEvent' to consuming events in bulk

public class SwitchController : MonoBehaviour
{
    public SwitchControls controls;

    float velocityScale = 2f;

    public GameObject velocityX;
    public GameObject velocityY;
    public GameObject velocityZ;

    float accScale = 2f;

    public GameObject accX;
    public GameObject accY;
    public GameObject accZ;

    public Text message;

    private Quaternion m_Attitude;
    private Vector3 m_Acceleration;
    private Vector3 m_Velocity;

    public void Awake()
    {
        controls.gameplay.attitude.performed += ctx => m_Attitude = ctx.GetValue<Quaternion>();
        controls.gameplay.velocity.performed += ctx => m_Velocity = ctx.GetValue<Vector3>();
        controls.gameplay.acceleration.performed += ctx => m_Acceleration = ctx.GetValue<Vector3>();
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    public void OnGUI()
    {
    }

    public void Update()
    {
        transform.localRotation = m_Attitude;

        velocityX.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.x * velocityScale);
        velocityY.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.y * velocityScale);
        velocityZ.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.z * velocityScale);

        accX.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.x * accScale);
        accY.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.y * accScale);
        accZ.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.z * accScale);

        var all = NPad.all;

        message.text = string.Empty;

        foreach (var gamepad in all)
        {
            NPad current = gamepad as NPad;

            if (current != null && (current.npadID != NPad.NpadId.Debug))
            {
                message.text = string.Format(
                    "ControllerID: {0}, NPadID: {1}, Orientation: {2}, Style: {3}\nColor(L): {4}/{5}, Color(R): {6}/{7}",
                    current.controllerID, current.npadID, current.orientation, current.styleMask,
                    current.leftControllerColor.Main, current.leftControllerColor.Sub, current.rightControllerColor.Main, current.rightControllerColor.Sub);
            }
        }
    }
}
