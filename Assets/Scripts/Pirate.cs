using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Pirate", menuName = "Pirate")]
public class Pirate : ScriptableObject {

    public string name;
    public string desc;

    public Sprite artwork;

    public int attack;
    public int health;

    public preAtks preattackType;
    public Atks attackType;
    public postAtks postattackType;

    private TeamManager myTeam;
    private TeamManager enemyTeam;

    public void SetTeams(TeamManager team1, TeamManager team2) {
        myTeam = team1;
        enemyTeam = team2;
    }


    public void Print() {
        Debug.Log(name + ": " + desc + "(" + attack + "/" + health +")");
    }

    public void PreAttack() {
        switch(preattackType) {
            case preAtks.none:
                break;
            case preAtks.ranged:
                atkRanged();
                break;
        }
    }

    public void Attack() {
        switch(attackType) {
            case Atks.none:
                break;
            case Atks.ranged:
                atkRanged();
                break;
        }
    }

    public void PostAttack() {
        switch(postattackType) {
            case postAtks.none:
                break;
            case postAtks.ranged:
                atkRanged();
                break;
        }
    }

    public enum preAtks {
        none,
        ranged
    }

    public enum Atks {
        none,
        ranged
    }

    public enum postAtks {
        none,
        ranged
    }


    //Standard:
    // PreAttacks start with pre
    // Attacks start with atk
    // PostAttacks start with pst

    void atkRanged() {
        //code for ranged attack
    }
}
