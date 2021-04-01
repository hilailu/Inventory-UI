using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float xRotation = 0f;

    void Update()
    {
        if (!PlayerController.isInventoryOpen)
        {
            xRotation -= (Input.GetAxis("Mouse Y") * Time.deltaTime * 100f);
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * 100f);
        }
    }
}
