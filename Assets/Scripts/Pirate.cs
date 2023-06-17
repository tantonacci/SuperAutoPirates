using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Pirate", menuName = "Pirate")]
public class Pirate : ScriptableObject {
    public bool debug;

    public string name;
    public string desc;

    public Sprite artwork;

    public int attack;
    public int health;

    private int slotNum;

    public preAtks preattackType;
    public Atks attackType;
    public postAtks postattackType;

    private TeamManager myTeam;
    private TeamManager enemyTeam;

    public void SetDebug(bool dbg) {
        debug = dbg;
    }

    public void SetTeams(TeamManager team1, TeamManager team2) {
        myTeam = team1;
        enemyTeam = team2;

        SetSlotNum();
    }

    private void SetSlotNum() {
        slotNum = myTeam.team.IndexOf(this);
    }

    public void TakeDamage(int dmg) {
        health -= dmg;
    }

    public void Print() {
        if (debug) Debug.Log(name + "(" + attack + "/" + health +") in Slot " + slotNum);
    }

    public void Print(string str) {
        if (debug) Debug.Log(str);
    }

    public void PreAttack() {
        Print();
        switch(preattackType) {
            case preAtks.none:
                Print("--PreAttack (none)--");
                break;
            case preAtks.basic:
                Print("--PreAttack (basic)--");
                break;
            case preAtks.ranged:
                Print("--PreAttack (ranged)--");
                break;
        }
    }

    public void Attack() {
        Print();
        switch(attackType) {
            case Atks.none:
                Print("--Attack (none)--");
                break;
            case Atks.basic:
                Print("--Attack (basic)--");
                atkBasic();
                break;
            case Atks.ranged:
                Print("--Attack (ranged)--");
                atkRanged();
                break;
        }
    }

    public void PostAttack() {
        Print();
        switch(postattackType) {
            case postAtks.none:
                Print("--PostAttack (none)--");
                break;
            case postAtks.basic:
                Print("--PostAttack (basic)--");
                break;
            case postAtks.ranged:
                Print("--PostAttack (ranged)--");
                break;
        }
    }

    public enum preAtks {
        none,
        basic,
        ranged
    }

    public enum Atks {
        none,
        basic,
        ranged
    }

    public enum postAtks {
        none,
        basic,
        ranged
    }


    //Standard:
    // PreAttacks start with pre
    // Attacks start with atk
    // PostAttacks start with pst

    void atkBasic() {
        if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        Pirate enemy = enemyTeam.GetFirstPirate();

        if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }

        enemy.TakeDamage(attack);
    }

    void atkRanged() {
        //code for ranged attack
    }
}
