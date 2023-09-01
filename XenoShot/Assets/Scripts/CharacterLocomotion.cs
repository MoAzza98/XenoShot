using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    public CinemachineFreeLook vCamFree;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            animator.SetBool("isAiming", true);
            vCamFree.m_Lens.FieldOfView = 35;
        } else if (!Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("isAiming", false);
            vCamFree.m_Lens.FieldOfView = 50;
        }

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }
}
