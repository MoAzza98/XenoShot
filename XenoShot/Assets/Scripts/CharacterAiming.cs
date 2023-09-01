using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    Camera mainCamera;
    public Transform gunPos;
    RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.StartFiring();
        } else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weapon.StopFiring();
        }
    }
}
