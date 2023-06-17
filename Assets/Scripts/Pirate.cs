using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Pirate", menuName = "Pirate")]
public class Pirate : ScriptableObject {

    public string name;
    public string desc;

    public Sprite artwork;

    public int attack;
	public int maxhealth;
    public int health;

    public preAtks preattackType;
    public Atks attackType;
    public postAtks postattackType;

    private TeamManager myTeam;
    private TeamManager enemyTeam;
	
	public bool poison;

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
			case preAtks.organize:
				preOrganize();
				break;
        }
    }

    public void Attack() {
        switch(attackType) {
            case Atks.none:
                break;
			case Atks.basic:
				atkBasic();
				break;
            case Atks.ranged:
                atkRanged();
                break;
			case Atks.poison:
				atkPoison();
				break;
			case Atks.sharpShooter:
				atkSharpShooter();
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
			case postAtks.poison:
				atkPoison();
				break;
        }
    }

    public enum preAtks {
        none,
        ranged,
		organize
    }

    public enum Atks {
        none,
		basic,
        ranged,
		poison,
		sharpShooter
    }

    public enum postAtks {
        none,
        ranged,
		poison
    }


    //Standard:
    // PreAttacks start with pre
    // Attacks start with atk
    // PostAttacks start with pst

    void atkRanged() {
        //code for ranged attack
		enemyTeam.team[0].health -= attack;
    }
	
	void atkBasic() {
        //code for basic attack
		enemyTeam.team[0].health -= attack;
		health -= enemyTeam.team[0].attack;
    }
	
	void preOrganize(){
		//code for organizer pirate attack
		int n = enemyTeam.team.Count;
		System.Random rng = new System.Random();
		while(n > 1){
			n--;
			int k = rng.Next(n + 1);
			Pirate p = enemyTeam.team[k];
			enemyTeam.team[k] = enemyTeam.team[n];
			enemyTeam.team[n] = p;
		}
	}
	
	void atkPoison(){
		enemyTeam.team[0].poison = true;
	}
	
	void pstPoison(){
		foreach (Pirate p in enemyTeam.team) {
            if (p.poison == true) {
                p.health -= 2;
            }
        }
	}
	
	void atkSharpShooter(){
		enemyTeam.team[enemyTeam.team.Count].health -= attack;
	}
	
	void atkHeal(){
		myTeam.team[myTeam.team.IndexOf(this) - 1].health += myTeam.team[myTeam.team.IndexOf(this) - 1].maxhealth * .5;
	}
}
