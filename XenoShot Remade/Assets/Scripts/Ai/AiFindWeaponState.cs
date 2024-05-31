using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    GameObject pickup;
    GameObject[] pickups = new GameObject[3];

    public AiStateId GetId() {
        return AiStateId.FindWeapon;
    }

    public void Enter(AiAgent agent) {
        pickup = null;
        agent.navMeshAgent.speed = agent.config.findWeaponSpeed;
        agent.navMeshAgent.ResetPath();
    }

    public void Update(AiAgent agent) {
        // Find pickup
        if (!pickup) {
            pickup = FindPickup(agent);

            if (pickup) {
                CollectPickup(agent, pickup);
                return;
            }
        }

        // Wander
        if (!agent.navMeshAgent.hasPath && !agent.navMeshAgent.pathPending) {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            agent.navMeshAgent.destination = worldBounds.RandomPosition();
        }

        if (agent.weapons.Count() == 1) {
            agent.stateMachine.ChangeState(AiStateId.FindTarget);
        }
    }

    public void Exit(AiAgent agent) {
    }

    GameObject FindPickup(AiAgent agent) {
        int count = agent.sensor.Filter(pickups, "Pickup", "Weapon");
        if (count > 0) {
            float bestAngle = float.MaxValue;
            GameObject bestPickup = pickups[0];
            for(int i = 0; i < count; ++i) {
                GameObject pickup = pickups[i];
                float pickupAngle = Vector3.Angle(agent.transform.forward, pickup.transform.position - agent.transform.position);
                if (pickupAngle < bestAngle) {
                    bestAngle = pickupAngle;
                    bestPickup = pickup;
                }
            }
            return bestPickup;
        }
        return null;
    }

    void CollectPickup(AiAgent agent, GameObject pickup) {
        agent.navMeshAgent.destination = pickup.transform.position;
    }
}
