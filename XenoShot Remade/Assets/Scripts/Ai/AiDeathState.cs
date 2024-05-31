using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;

    public AiStateId GetId() {
        return AiStateId.Death;
    }

    public void Enter(AiAgent agent) {
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        agent.ui?.gameObject.SetActive(false);
        agent.mesh.updateWhenOffscreen = true;
        agent.weapons.DropWeapon();
        agent.weapons.SetTarget(null);
        agent.navMeshAgent.enabled = false;
    }

    public void Update(AiAgent agent) {
    }

    public void Exit(AiAgent agent) {
        agent.navMeshAgent.enabled = true;
    }
}
