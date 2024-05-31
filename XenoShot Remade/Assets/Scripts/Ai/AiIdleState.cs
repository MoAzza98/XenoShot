using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId() {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent) {
        agent.weapons.DeactivateWeapon();
        agent.navMeshAgent.ResetPath();
    }

    public void Update(AiAgent agent) {
        if (agent.playerTransform.GetComponent<Health>().IsDead()) {
            return;
        }

        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance) {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f) {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent) {
    }
}
