using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform player;

    [Header("Debug")]
    [Range(0, 1f)]
    [SerializeField] float debugSize;
    [SerializeField] Color debugColor = Color.white;
    public void TeleportToRoom()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.position += offset;
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawCube(offset + transform.position, Vector3.one * debugSize);
    }
}
