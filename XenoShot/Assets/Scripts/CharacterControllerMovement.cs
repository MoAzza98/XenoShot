using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float originalMoveSpeed;
    public float dashSpeed = 50f;
    public float dashTime = 0.5f;
    private float currentDashTimer = 0.5f;
    private float velocity;
    private Vector3 direction;
    [SerializeField] private float jump = 10f;
    public float Gravity = -9.8f;
    public float gravityMultiplier;

    private CharacterController cc;

    // Use this for initialization

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fast"))
        {
            StartCoroutine(ActivatePowerUp(other.gameObject));
        }
        else if (other.CompareTag("Damage"))
        {
            RaycastWeapon.instance.damage += RaycastWeapon.instance.damage;
        } 
        else if (other.CompareTag("fasterfire"))
        {
            RaycastWeapon.instance.fireRate += RaycastWeapon.instance.fireRate;
        }
    }
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ApplyGravity();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(DashCoroutine());
        }
        else
        {
            cc.Move((transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * moveSpeed * Time.deltaTime);
        }

        if (!cc.isGrounded)
        {
            cc.Move(Vector3.up * Time.deltaTime * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        // (-0.5) change this value according to your character y position + 1
        {
            Jump();
        }

    }
    
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            cc.Move((transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * dashSpeed * Time.deltaTime);
            currentDashTimer -= Time.deltaTime;

            yield return null;
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += Gravity * gravityMultiplier * Time.deltaTime;
        }

        direction.y = velocity;
    }

    public void Jump()
    {
        if (!IsGrounded()) return;
        velocity += jump;
    }


    private bool isGrounded()
    {
        return cc.isGrounded;
    }

    private bool IsGrounded() => cc.isGrounded;


    IEnumerator ActivatePowerUp(GameObject powerUp)
    {
        // Increase move speed
        moveSpeed *= 2f;

        // Wait for some time to let the power-up effect expire
        yield return new WaitForSeconds(10f);

        // Reset move speed to its original value
        moveSpeed = originalMoveSpeed;

        // Disable the power-up object
        powerUp.SetActive(false);
    }

    
}
