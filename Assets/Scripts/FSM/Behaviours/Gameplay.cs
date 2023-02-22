using System.Collections;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachine;
    [SerializeField] private Dealer dealer;
    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Gameplay State");
        finiteStateMachine = FiniteStateMachine;
        StartCoroutine(InitiateGame());
    }

    private IEnumerator InitiateGame()
    {
        yield return new WaitForSeconds(2f);
        dealer.Init(finiteStateMachine);
    }
}