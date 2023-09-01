using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    private CinemachineFreeLook vCamFree;
    public Transform lookAt;
    public Transform follow;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vCamFree = FindAnyObjectByType<CinemachineFreeLook>();
        vCamFree.m_LookAt = lookAt;
        vCamFree.m_Follow = follow;
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
