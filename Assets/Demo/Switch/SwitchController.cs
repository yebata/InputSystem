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

    public GameObject gyro;

    public GameObject velocityX;
    public GameObject velocityY;
    public GameObject velocityZ;

    float accScale = 2f;

    public GameObject accX;
    public GameObject accY;
    public GameObject accZ;

    public Text message;
    public GameObject[] colorL;
    public GameObject[] colorR;
    public Image[] cursors;


    private bool m_IsGyroEnabled = false;

    private Quaternion m_Attitude;
    private Vector3 m_Acceleration;
    private Vector3 m_Velocity;

    private int m_TouchID;

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
        gyro.transform.localRotation = m_Attitude;

        velocityX.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.x * velocityScale);
        velocityY.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.y * velocityScale);
        velocityZ.transform.localScale = Vector3.one + (Vector3.right * m_Velocity.z * velocityScale);

        accX.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.x * accScale);
        accY.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.y * accScale);
        accZ.transform.localScale = Vector3.one + (Vector3.right * m_Acceleration.z * accScale);

        // Npads
        {
            var all = NPad.all;

            message.text = string.Empty;

            foreach (var gamepad in all)
            {
                NPad current = gamepad as NPad;

                if (current != null && (current.npadId != NPad.NpadId.Debug))
                {
                    message.text += string.Format(
                        "ID: {0}, NPadID: {1}, Orientation: {2}, Style: {3}\n",
                        current.id, current.npadId, current.orientation, current.styleMask);
                }
            }

            var touchscreen = InputSystem.GetDevice<Touchscreen>();

            if (touchscreen != null)
            {
                message.text += string.Format("Touch count: {0}\n", touchscreen.activeTouches.Count);
            }

            if (all.Count > 0)
            {
                NPad current = all[0] as NPad;

                if (current != null)
                {
                    if (colorL != null)
                    {
                        if (colorL[0] != null)
                        {
                            colorL[0].SetActive(true);
                            colorL[0].GetComponentInChildren<Image>().color = current.leftControllerColor.Main;
                            colorL[0].GetComponentInChildren<Text>().text = current.leftControllerColor.Main.ToString();
                        }
                        if (colorL[1] != null)
                        {
                            colorL[1].SetActive(true);
                            colorL[1].GetComponentInChildren<Image>().color = current.leftControllerColor.Sub;
                            colorL[1].GetComponentInChildren<Text>().text = current.leftControllerColor.Sub.ToString();
                        }
                    }
                    if (colorR != null)
                    {
                        if (colorR[0] != null)
                        {
                            colorR[0].SetActive(true);
                            colorR[0].GetComponentInChildren<Image>().color = current.rightControllerColor.Main;
                            colorR[0].GetComponentInChildren<Text>().text = current.rightControllerColor.Main.ToString();
                        }
                        if (colorR[1] != null)
                        {
                            colorR[1].SetActive(true);
                            colorR[1].GetComponentInChildren<Image>().color = current.rightControllerColor.Sub;
                            colorR[1].GetComponentInChildren<Text>().text = current.rightControllerColor.Sub.ToString();
                        }
                    }
                }
                else
                {
                    foreach (var bar in colorL)
                    {
                        if (bar != null)
                            bar.SetActive(false);
                    }
                    foreach (var bar in colorR)
                    {
                        if (bar != null)
                            bar.SetActive(false);
                    }
                }
            }
        }

        // Touchscreen
        if (cursors != null && cursors.Length > 0)
        {
            var touchscreen = InputSystem.GetDevice<Touchscreen>();

            if (touchscreen != null)
            {
                var activeTouches = touchscreen.activeTouches;

                if (activeTouches.Count == 0)
                {
                    foreach (var cursor in cursors)
                        cursor.enabled = false;
                }
                else
                {
                    int c = 0;

                    foreach (var activeTouch in activeTouches)
                    {
                        var touch = activeTouch.ReadValue();
                        var cursor = cursors[c];

                        cursor.enabled = true;
                        cursor.rectTransform.position = touch.position;

                        if (++c >= cursors.Length)
                            break;
                    }
                    while (c < cursors.Length)
                    {
                        cursors[c++].enabled = false;
                    }
                }
            }
        }
    }
}
