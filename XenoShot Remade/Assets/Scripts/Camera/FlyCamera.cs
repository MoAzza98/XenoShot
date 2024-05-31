using UnityEngine;

public class FlyCamera : MonoBehaviour {

    public float mouseSensitivity = 1.8f;
    public float movementSpeed = 10f;
    public float acceleratedSpeed = 50f;

    private void Start() {
        enabled = false;
    }

    private void SetCursorState() {
        // Hide cursor while locked, holding the right mouse button down
        bool locked = Input.GetMouseButton(1);
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    private void Update() {

        SetCursorState();

        if (Cursor.visible)
            return;

        // Movement
        Vector3 deltaPosition = Vector3.zero;
        float currentSpeed = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = acceleratedSpeed;

        if (Input.GetKey(KeyCode.W))
            deltaPosition += transform.forward;

        if (Input.GetKey(KeyCode.S))
            deltaPosition -= transform.forward;

        if (Input.GetKey(KeyCode.A))
            deltaPosition -= transform.right;

        if (Input.GetKey(KeyCode.D))
            deltaPosition += transform.right;

        // Translate
        transform.position += deltaPosition * currentSpeed * Time.deltaTime;

        // Rotate camera by holding right mouse
        if (Input.GetMouseButton(1)) {
            // Pitch
            transform.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * mouseSensitivity,
                Vector3.right
            );

            // Yaw
            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity,
                transform.eulerAngles.z
            );
        }
    }
}
