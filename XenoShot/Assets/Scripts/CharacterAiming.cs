using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 1.8f;

    Camera mainCamera;
    public Transform gunPos;
    public Rig aimLayer;

    public bool lockMovement;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }*/

        aimLayer.weight = 1.0f;

        if (!lockMovement)
        {
            CharacterRotation();
        }
    }

    private void CharacterRotation()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!lockMovement)
        {
            CharacterRotation();
        }
    }

    private void LateUpdate()
    {
        
    }


}
