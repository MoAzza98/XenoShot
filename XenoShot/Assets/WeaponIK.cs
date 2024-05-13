using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
}

public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;

    public int iterations = 10;
    public float weight = 1.0f;

    public HumanBone[] humanBones;
    Transform[] boneTransforms;


    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for(int i = 0; i < boneTransforms.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(aimTransform == null)
        {
            aimTransform = GetComponentInChildren<DebugDrawLine>().transform;
        }
        else
        {
            Vector3 targetPosition = targetTransform.position;
            for(int i = 0; i <= iterations; i++)
            {
                for (int b = 0; b < boneTransforms.Length; b++)
                {
                    Transform bone = boneTransforms[b];
                    AimAtTarget(bone, targetPosition);
                }
            }
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }
}
