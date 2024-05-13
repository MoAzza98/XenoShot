using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSocket : MonoBehaviour
{
    public MeshSockets.SocketId socketId;
    public HumanBodyBones bone;

    Transform attachPoint;

    public Vector3 offset;
    public Vector3 rotation;

    public WeaponOffsets wOffsets;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponentInParent<Animator>();
        attachPoint = new GameObject("Socket " + socketId).transform;
        attachPoint.SetParent(animator.GetBoneTransform(bone));
        try
        {
            wOffsets.offset = GameObject.Find("Socket RightHand");
        } catch (Exception e)
        {
            Debug.LogError(e);
        }
        Debug.Log(animator.GetBoneTransform(bone));
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attachPoint, false);
    }
}
