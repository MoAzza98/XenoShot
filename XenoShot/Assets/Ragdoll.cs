using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateRagdoll()
    {
        foreach (var rb in rb)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (var rb in rb)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidbody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
