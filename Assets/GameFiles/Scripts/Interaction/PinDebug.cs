using TMPro;
using UnityEngine;

public class PinDebug : MonoBehaviour
{
    [SerializeField] string[] pinSimbol;
    [SerializeField] bool[] notEmpty;
    [SerializeField] TextMeshProUGUI debugPin;

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
