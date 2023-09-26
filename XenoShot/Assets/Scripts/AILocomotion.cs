using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    private Transform playerTransform;
    public float characterHeight;
    NavMeshAgent agent;
    Animator animator;

    public float maxTime = 1.0f;
    public float maxdistance = 1.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("MegaBoy").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
        if(sqDistance > maxdistance*maxdistance)
        {
            agent.destination = playerTransform.position;
        }
        timer = maxTime;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
