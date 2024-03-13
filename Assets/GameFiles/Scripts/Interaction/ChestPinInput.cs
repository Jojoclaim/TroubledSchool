using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChestPinInput : MonoBehaviour
{
    [SerializeField] UnityEvent OnCorectPin;
    [SerializeField] int Pin;

    [Header("Input Pin")]
    [SerializeField] int[] Nr;
    [SerializeField] TextMeshPro[] debugNr;

    public bool active;
    int x;

    public void Activate()
    {
        active = !active;
    }

    public void CeckPin()
    {
        string _pin;
        _pin = "";
        for (int i = 0; i < Nr.Length; i++)
        {
            _pin += Nr[i];
        }
        if (Pin.ToString() == _pin)
        {
            OnCorectPin.Invoke();
        }
    }

    public void Update()
    {
        if (active)
        {
            ChangeNumber();
        }
    }

    public void ChangeNumber()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Nr[x]++;
            if (Nr[x] > 9)
            {
                Nr[x] = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Nr[x]--;
            if (Nr[x] < 0)
            {
                Nr[x] = 9;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x--;
            if (x < 0)
            {
                x = Nr.Length-1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            x++;
            if (x > Nr.Length)
            {
                x = 0;
            }
        }
        for (int i = 0; i < Nr.Length; i++)
        {
            debugNr[i].color = Color.gray;
            debugNr[x].color = Color.white;
            debugNr[i].text = Nr[i].ToString();
        }
        CeckPin();
    }
}
