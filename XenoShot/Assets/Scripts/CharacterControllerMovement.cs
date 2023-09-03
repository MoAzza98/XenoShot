using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Vector3 velocity;
    [SerializeField] private float jump = 10f;
    public float Gravity = -9.8f;

    private CharacterController cc;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        cc.Move((transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * moveSpeed * Time.deltaTime);
        if (!cc.isGrounded)
        {
            cc.Move(Vector3.up * Time.deltaTime * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < -0.51f)
        // (-0.5) change this value according to your character y position + 1
        {
            velocity.y = jump;
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
        }
        cc.Move(velocity * Time.deltaTime);

    }
    


    private bool isGrounded()
    {
        return cc.isGrounded;
    }
}
