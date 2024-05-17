using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : Health
{
    AiAgent agent;
    protected override void OnStart()
    {
        agent = GetComponent<AiAgent>();
        base.OnStart();
    }

    protected override void OnDamage(Vector3 direction)
    {
        base.OnDamage(direction);
    }

    protected override void OnDeath(Vector3 direction)
    {
        Debug.Log("Should die");
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;

        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);

        // Increase the score when an enemy dies
        hitmarker.deathmarkerTimer = +1f;

        // Perform other death-related actions (e.g., play death animation, destroy GameObject)
        Destroy(gameObject, 1f);
    }
}
