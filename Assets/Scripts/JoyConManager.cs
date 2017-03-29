using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyConManager : MonoBehaviour {

    [SerializeField] GameObject brushesManager;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetJoystickNames().Length > 0)
        {
            float axisValue = Input.GetAxis("Axis 10");

            if (axisValue == -1.0f)
            {
                brushesManager.SendMessage("IncreaseBrushSize", 0.01f);
            }

            else if (axisValue == 0)
            {
                brushesManager.SendMessage("DecreaseBrushSize", 0.01f);
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                brushesManager.SendMessage("PickNextColor");
            }

            else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                brushesManager.SendMessage("PickPreviousColor");
            }

            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                brushesManager.SendMessage("InitializeBrush");
            }

            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                brushesManager.SendMessage("DestroyBrush");
            }

            else if (Input.GetKeyDown(KeyCode.Joystick1Button15))
            {
                brushesManager.SendMessage("ResetDrawing");
            }
        }
    }
}
