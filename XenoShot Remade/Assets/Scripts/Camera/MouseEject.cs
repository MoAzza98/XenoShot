using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEject : MonoBehaviour
{
    public KeyCode ejectKey = KeyCode.Escape;
    FlyCamera flyCam;

    private void Awake() {
        flyCam = GetComponent<FlyCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ejectKey)) {
            Cinemachine.CinemachineBrain camera = GetComponent<Cinemachine.CinemachineBrain>();
            if (camera) {
                camera.enabled = false;
            }

            CharacterAiming aiming = FindObjectOfType<CharacterAiming>();
            if (aiming) {
                aiming.enabled = false;
            }

            CharacterLocomotion locomotion = FindObjectOfType<CharacterLocomotion>();
            if (locomotion) {
                locomotion.enabled = false;
            }

            if (flyCam) {
                flyCam.enabled = true;
            }
            else {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            Cinemachine.CinemachineBrain camera = GetComponent<Cinemachine.CinemachineBrain>();
            if (camera) {
                camera.enabled = true;
            }

            CharacterAiming aiming = FindObjectOfType<CharacterAiming>();
            if (aiming) {
                aiming.enabled = true;
            }

            CharacterLocomotion locomotion = FindObjectOfType<CharacterLocomotion>();
            if (locomotion) {
                locomotion.enabled = true;
            }

            if (flyCam) {
                flyCam.enabled = false;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
