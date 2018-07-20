using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Interactions;
using UnityEngine.Experimental.Input.Plugins.Switch;
using UnityEngine.UI;

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

    private bool m_IsGyroEnabled = false;

    private Quaternion m_Attitude;
    private Vector3 m_Acceleration;
    private Vector3 m_Velocity;

    public void Awake()
    {
        controls.gameplay.attitude.performed += ctx => m_Attitude = ctx.ReadValue<Quaternion>();
        controls.gameplay.velocity.performed += ctx => m_Velocity = ctx.ReadValue<Vector3>();
        controls.gameplay.acceleration.performed += ctx => m_Acceleration = ctx.ReadValue<Vector3>();

        controls.gameplay.gyro.performed += ctx =>
        {
            NPad npad = ctx.control.device as NPad;

            if (npad != null)
            {
                if (m_IsGyroEnabled)
                    npad.StopSixAxisSensor();
                else
                    npad.StartSixAxisSensor();

                m_IsGyroEnabled = !m_IsGyroEnabled;
            }
        };
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
                    "NPadID: {0}, Orientation: {1}, Style: {2}\nColor(L): {3}/{4}, Color(R): {5}/{6}",
                    current.npadID, current.orientation, current.styleMask,
                    current.leftControllerColor.Main, current.leftControllerColor.Sub, current.rightControllerColor.Main, current.rightControllerColor.Sub);
            }
        }
    }
}
