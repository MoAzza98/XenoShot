using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId {
    ChasePlayer,
    Death,
    Idle,
    FindWeapon,
    AttackTarget,
    FindTarget,
    FindHealth,
    FindAmmo
}

public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
