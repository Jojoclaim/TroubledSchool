using System.Collections;
using TMPro;
using UnityEngine;

public class BasicSharpPortal : MonoBehaviour
{
    [Header("Hint")]
    [SerializeField] TextMeshProUGUI hintDebug;
    [SerializeField] string hintText;
    [SerializeField] int triggerHintAfter;

    [Header("Teleportation")]
    [SerializeField] Transform Player; 
    [SerializeField] Transform Portal;
    [SerializeField] TeleportAxe _teleportAxe;
    [SerializeField] float offset;
    [SerializeField] Vector3 additionalVectorOffset;

    [Header("Trigger")]
    [SerializeField] TriggerOrientation _triggerOrientation;
    [SerializeField] TeleportAxe _triggerAxe;
    [Range(0f, 5f)]
    [SerializeField] float triggerRadius;

    [Header("Debug")]
    [SerializeField] Color outlineGismosColor;
    [SerializeField] Color dotGismosColor;
    [Range(0f, 1f)]
    [SerializeField] float teleportPointScale;

    enum TeleportAxe { x, y, z };
    enum TriggerOrientation { positive, negative };

    int teleportsCount = 0;
    Vector3 teleportDirection(float worldOffset)
    {
        Vector3 debugTpDirection = Vector3.zero;
        switch (_teleportAxe)
        {
            case TeleportAxe.x:
                debugTpDirection = Vector3.right;
                break; 
            case TeleportAxe.y:
                debugTpDirection = Vector3.up;
                break; 
            case TeleportAxe.z:
                debugTpDirection = Vector3.forward;
                break;
        }
        return debugTpDirection * worldOffset;
    }

    public void Update()
    {
        switch (_triggerOrientation)
        {
            case TriggerOrientation.positive:
                CeckTrigger(1);
                break; 
            case TriggerOrientation.negative:
                CeckTrigger(-1);
                break;
        }
    }

    void CeckTrigger(int _value)
    {
        if (Vector3.Distance(Player.position, Portal.position) <= triggerRadius)
        {
            switch (_triggerAxe)
            {
                case TeleportAxe.x:
                    if (Player.position.x * _value < Portal.position.x)
                    {
                        Teleport();
                    }
                    break;
                case TeleportAxe.y:
                    if (Player.position.y * _value < Portal.position.y)
                    {
                        Teleport();
                    }
                    break;
                case TeleportAxe.z:
                    if (Player.position.z * _value < Portal.position.z)
                    {
                        Teleport();
                    }
                    break;
            }
        }
    }

    public void Teleport()
    {
        Player.gameObject.GetComponent<CharacterController>().enabled = false;
        Player.position = Player.position + teleportDirection(offset) + additionalVectorOffset;
        Debug.Log(Player.position);
        Player.gameObject.GetComponent<CharacterController>().enabled = true;
        teleportsCount++;
        if (teleportsCount >= triggerHintAfter && triggerHintAfter > 0)
        {
            teleportsCount = 0;
            hintDebug.text = hintText;
            StartCoroutine(RemoveHint());
        }
    }

    IEnumerator RemoveHint()
    {
        yield return new WaitForSeconds(2);
        hintDebug.text = "";
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = outlineGismosColor;
        Gizmos.DrawWireSphere(Portal.position, triggerRadius/2);
        Gizmos.color = dotGismosColor;
        Gizmos.DrawSphere(Portal.position, teleportPointScale);
        Gizmos.DrawCube(Portal.position + teleportDirection(offset) + additionalVectorOffset, Vector3.one * teleportPointScale);
    }
}
