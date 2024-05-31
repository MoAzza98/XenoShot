using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ThirdPersonConfiguration : ScriptableObject {
    public RuntimeAnimatorController controller;
    public GameObject animationRig;
    public GameObject cameraRig;
}