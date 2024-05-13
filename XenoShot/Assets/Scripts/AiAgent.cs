using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId intialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    public UIHealthbar ui;
    public Transform playerTransform;
    [HideInInspector] public AiWeapon weapon;

    private void Start()
    {
        ragdoll.GetComponent<Ragdoll>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        ui = GetComponentInChildren<UIHealthbar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        weapon = GetComponent<AiWeapon>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiFindWeaponState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.ChangeState(intialState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
