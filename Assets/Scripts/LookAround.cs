using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    [SerializeField]
    private float clampAngle = 45f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Up/down
        playerBody.Rotate(Vector3.up * mouseX); // Left/right
    }
}