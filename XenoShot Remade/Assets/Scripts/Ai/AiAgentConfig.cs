using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 10.0f;
    public float maxSightDistance = 5.0f;
    public float findWeaponSpeed = 5.0f;
    public float findTargetSpeed = 5.0f;

    [Header("Attack State")]
    public float attackSpeed = 3.0f;
    public float attackStoppingDistance = 5.0f;
    public float attackCloseRange = 7.0f;
}
