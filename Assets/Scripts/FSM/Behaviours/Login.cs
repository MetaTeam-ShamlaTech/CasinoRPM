using System.Collections;
using UnityEngine;

public class Login : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachine;
    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Login State");
        finiteStateMachine = FiniteStateMachine;
        StartCoroutine(LoginPanel());
    }

    private IEnumerator LoginPanel()
    {
        yield return new WaitForSeconds(2f);
        finiteStateMachine.ChangeState(finiteStateMachine.PreGameState);
    }
}