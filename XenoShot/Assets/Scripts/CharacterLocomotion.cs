using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator animator;
    private Vector2 input;
    private CinemachineFreeLook vCamFree;
    public GameObject vCamFreeGO;
    private CinemachineVirtualCamera vCamVirt;
    public GameObject vCamVirtGO;
    public Transform lookAt;
    public Transform fpsFollow;
    public Transform follow;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vCamFree = FindAnyObjectByType<CinemachineFreeLook>();
        vCamVirt = FindAnyObjectByType<CinemachineVirtualCamera>();
        vCamVirt.m_Follow = fpsFollow;
        vCamFree.m_LookAt = lookAt;
        vCamFree.m_Follow = follow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (vCamFree.gameObject.active)
            {
                vCamFree.gameObject.SetActive(false);
                vCamVirt.gameObject.SetActive(true);
            }
            if(vCamVirt.gameObject.active)
            {
                vCamFree.gameObject.SetActive(true);
                vCamVirt.gameObject.SetActive(false);
            }
        }
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
