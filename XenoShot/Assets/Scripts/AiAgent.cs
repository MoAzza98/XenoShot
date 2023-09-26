using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;

    private void Start()
    {
        stateMachine = new AiStateMachine(this);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
