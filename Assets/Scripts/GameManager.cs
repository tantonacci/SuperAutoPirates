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

    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Running;

        teamL = teamLeft.GetComponent<TeamManager>();
        teamR = teamRight.GetComponent<TeamManager>();

        teamL.SetEnemyTeam(teamR);
        teamR.SetEnemyTeam(teamL);

        teamL.SetDebug(debug);
        teamR.SetDebug(debug);
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

        Print("Running Round");

        PreAttack();
        Attack();
        PostAttack();

        CheckWinCondition();
    }

    void PreAttack() {
        if (debug) Print("PreAttack");

        teamL.PreAttack();
        teamR.PreAttack();
        CheckPirates();
    }

    void Attack() {
        if (debug) Print("Attack");

        teamL.Attack();
        teamR.Attack();
        CheckPirates();
    }

    void PostAttack() {
        if (debug) Print("PostAttack");

        teamL.PostAttack();
        teamR.PostAttack();
        CheckPirates();
    }

    void CheckPirates() {
        teamL.CheckPirates();
        teamR.CheckPirates();
    }

    void CheckWinCondition() {
        if (!teamL.HasMembersRemaining() && !teamR.HasMembersRemaining()) {
            Print("Tie game");
            gameState = GameState.Tie;
        } else if (!teamL.HasMembersRemaining()) {
            Print("Right Team Wins");
            gameState = GameState.Lose;
        } else if (!teamR.HasMembersRemaining()) {
            Print("Left Team Wins");
            gameState = GameState.Win;
        }
    }

    void Print(string str) {
        if (debug) Debug.Log(str);
    }

    enum GameState {
        Running,
        Win,
        Lose,
        Tie
    }

}
