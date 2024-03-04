using Minigame.Snake;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{

    [SerializeField] SnakeGameLogic snakeGameLogic;
    [SerializeField] private float switchSpeed;
    [SerializeField] protected bool active;

    [SerializeField] private Transform targetLookPoint;

    [SerializeField] private Transform folowCamera; //player camera
    [SerializeField] private Transform secondCamera; //not player camera

    [SerializeField] private GameObject Player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && active)
        {
            SwitchCamera();
        }
        if (!active)
        {
            secondCamera.SetPositionAndRotation(folowCamera.position, folowCamera.rotation);
        }
        else
        {
            secondCamera.SetPositionAndRotation(
                Vector3.Lerp(secondCamera.position, targetLookPoint.position, switchSpeed * Time.deltaTime),
                Quaternion.Lerp(secondCamera.rotation, targetLookPoint.rotation, switchSpeed * Time.deltaTime));
        }
    }

    public void SwitchCamera()
    {
        active = !active;
        folowCamera.GetComponent<Camera>().enabled = !active;
        secondCamera.GetComponent<Camera>().enabled = active;
        Player.SetActive(!active);
        if (active)
        {
            snakeGameLogic.StartGame();
        }
        else
        {
            snakeGameLogic.ReturnTo3D();
        }
    }
}
