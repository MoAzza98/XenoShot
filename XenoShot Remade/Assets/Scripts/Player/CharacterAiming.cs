using UnityEngine;

public class CharacterAiming : MonoBehaviour 
{
    public float aimDuration = 0.18f;
    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis = new Cinemachine.AxisState(-180, 180, true, false, 500, 0.02f, 0.02f, "Mouse X", false);
    public Cinemachine.AxisState yAxis = new Cinemachine.AxisState(-85, 85, false, false, 300, 0.02f, 0.02f, "Mouse Y", true);
    public bool isAiming;

    Animator animator;
    ActiveWeapon activeWeapon;
    int isAimingParam = Animator.StringToHash("isAiming");
    Vector3 cameraRotation;

    int frame = 0;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();

        xAxis.Value = transform.eulerAngles.y;
        yAxis.Value = 0;
    }

    void Update() {
        isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        var weapon = activeWeapon.GetActiveWeapon();
        if (weapon) {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1.0f;
        }

        RotateCamera(Time.deltaTime);
    }

    void RotateCamera(float deltaTime) {

        // This is to prevent large mouse deltas on first frame when entering play mode
        // from the editor.
        if (frame++ > 5) {
            // Update mouse axis
            xAxis.Update(deltaTime);
            yAxis.Update(deltaTime);
        }
        

        // Camera look up / down
        cameraRotation.x = yAxis.Value;
        cameraLookAt.localEulerAngles = cameraRotation;

        // Rotate player left / right
        var euler = transform.eulerAngles;
        euler.y = xAxis.Value;
        transform.eulerAngles = euler;
    }
}
