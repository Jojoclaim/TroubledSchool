using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PinDebug : MonoBehaviour
{
    [SerializeField] string corectPin;
    [SerializeField] string[] pinSimbol;
    [SerializeField] bool[] notEmpty;
    [SerializeField] TextMeshProUGUI debugPin;
    [SerializeField] UnityEvent OnFullPin, OnNotFullPin;

    public void CeckPin()
    {
        if (debugPin.text == corectPin)
        {
            OnFullPin.Invoke();
        }
        else
        {
            OnNotFullPin.Invoke();
        }
    }

    public void UpdatePinDebug(int i)
    {
        string debug;
        debug = "";
        notEmpty[i] = true;
        for (int j = 0; j < notEmpty.Length; j++)
        {
            debug = notEmpty[j] ? debug + pinSimbol[j] : debug + "#";
        }
        debugPin.text = debug;
    }
}
