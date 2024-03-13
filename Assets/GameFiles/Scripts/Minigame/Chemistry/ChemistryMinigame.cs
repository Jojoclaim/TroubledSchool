using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChemistryMinigame : MonoBehaviour
{
    [Header("Static")]
    [SerializeField] int Pin;

    int inputKey;
    string inputPin;

    [Header("Output")]
    [SerializeField] UnityEvent OnEnterCorectPin, OnEnterIncorectPin;
    [SerializeField] int k;
    [SerializeField] TextMeshProUGUI debugK;

    bool active;

    public void DebugK()
    {
        debugK.text = k.ToString();
    }

    public void Update()
    {
        if (active)
        {
            Dictionary<KeyCode, int> keyToInt = new Dictionary<KeyCode, int>
        {
            { KeyCode.Alpha1, 1 },
            { KeyCode.Alpha2, 2 },
            { KeyCode.Alpha3, 3 },
            { KeyCode.Alpha4, 4 },
            { KeyCode.Keypad1, 1 },
            { KeyCode.Keypad2, 2 },
            { KeyCode.Keypad3, 3 },
            { KeyCode.Keypad4, 4 }
        };

            foreach (var key in keyToInt.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    inputKey = keyToInt[key];
                    break;
                }
            }
            Select();
        }
    }

    public void Select()
    {
        if (inputKey > 0)
        {
            k++;
            inputPin += inputKey.ToString();
        }
        if (k >= 6)
        {
            CeckInputPin();
            k = 0;
        }
        DebugK();
        inputKey = 0;
    }

    public void CeckInputPin()
    {
        if (inputPin == Pin.ToString())
        {
            OnEnterCorectPin.Invoke();
            inputPin = "";
        }
        else
        {
            OnEnterIncorectPin.Invoke();
            inputPin = "";
        }
    }

    public void Activate()
    {
        active = !active;
        if (!active)
        {
            k = 0;
            inputPin = "";
        }
    }
}
