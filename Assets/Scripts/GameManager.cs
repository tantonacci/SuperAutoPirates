using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameState gameState;

    public GameObject UI;
    public GameObject teamLeft;
    public GameObject teamRight;
    private TeamManager teamL;
    private TeamManager teamR;

    public bool debug;

    private bool addToLeftTeam = true;

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
		
		teamR.SetPremadeTeam1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToLeftTeam(bool add) {
        addToLeftTeam = add;
    } 

    public void AddPirateToTeam(Pirate p) {
        if (addToLeftTeam) {
            teamL.AddPirateToTeam(p);
        } else {
            teamR.AddPirateToTeam(p);
        }
    }

    public void ClearLeftTeam() {
        teamL.ClearTeam();
    }

    public void ClearRightTeam() {
        teamR.ClearTeam();
    }

    public void RunBattle() {
        while (gameState == GameState.Running) {
            RunRound();

        }
    }

    public void RunRound() {
        Print("Running Round");

        StartOfRound();
        if (PreAttack() || Attack() || PostAttack()) {
            CheckWinCondition();
        }
    }
    
    void StartOfRound() {
		Print("start pf round");
        teamL.StartOfRound();
        teamR.StartOfRound();
    }

    bool PreAttack() {
        if (debug) Print("PreAttack");

        teamL.PreAttack();
        teamR.PreAttack();
        return CheckPirates();
    }

    bool Attack() {
        if (debug) Print("Attack");

        teamL.Attack();
        teamR.Attack();
        return CheckPirates();
    }

    bool PostAttack() {
        if (debug) Print("PostAttack");

        teamL.PostAttack();
        teamR.PostAttack();
        return CheckPirates();
    }

    bool CheckPirates() {
        return teamL.CheckPirates() || teamR.CheckPirates();
    }

    void CheckWinCondition() {
        UIManager ui = UI.GetComponent<UIManager>();

        if (!teamL.HasMembersRemaining() && !teamR.HasMembersRemaining()) {
            Print("Tie game");
            gameState = GameState.Tie;
            ui.Lose();
        } else if (!teamL.HasMembersRemaining()) {
            Print("Right Team Wins");
            gameState = GameState.Lose;
            ui.Lose();
        } else if (!teamR.HasMembersRemaining()) {
            Print("Left Team Wins");
            gameState = GameState.Win;
            ui.Win();
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
