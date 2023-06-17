using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameState gameState;

    public GameObject teamLeft;
    public GameObject teamRight;
    private TeamManager teamL;
    private TeamManager teamR;


    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Running;

        teamL = teamLeft.GetComponent<TeamManager>();
        teamR = teamRight.GetComponent<TeamManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunBattle() {
        while (gameState == GameState.Running) {
            RunRound();

        }
    }


    public void RunRound() {
        PreAttack();
        Attack();
        PostAttack();

        CheckWinCondition();
    }

    void PreAttack() {
        teamL.PreAttack();
        teamR.PreAttack();
    }

    void Attack() {
        teamL.Attack();
        teamR.Attack();
    }

    void PostAttack() {
        teamL.PostAttack();
        teamR.PostAttack();
    }

    void CheckWinCondition() {

    }

    enum GameState {
        Running,
        Win,
        Lose,
        Tie
    }

}
