using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModellingAnimations : MonoBehaviour
{
    [SerializeField] private bool aimAnim;
    [SerializeField] private bool crouchAnim;
    [SerializeField] private bool rlAimAnim;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if(aimAnim)
        {
            animator.SetBool("Aiming", true);
        } else if (crouchAnim)
        {
            animator.SetBool("Crouching", true);
        } else if (rlAimAnim)
        {
            animator.SetBool("RL Aim", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
