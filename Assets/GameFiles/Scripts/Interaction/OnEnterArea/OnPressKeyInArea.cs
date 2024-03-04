using UnityEngine;
using UnityEngine.Events;

public class OnPressKeyInArea : MonoBehaviour
{
    [SerializeField] KeyCode keyCode;
    [SerializeField] LayerMask interactiveLayers;
    [SerializeField] UnityEvent onKeyDown, onEnterArea, onExitArea;
    bool isInArea;

    public void OnTriggerEnter(Collider other)
    {
        if ((interactiveLayers & (1 << other.gameObject.layer)) != 0)
        {
            onEnterArea.Invoke();
            isInArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if ((interactiveLayers & (1 << other.gameObject.layer)) != 0)
        {
            onExitArea.Invoke();
            isInArea = false;
        }
    }

    void Update()
    {
        if (isInArea && Input.GetKeyDown(keyCode))
        {
            onKeyDown.Invoke();
        }
    }
}
