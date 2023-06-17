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
	public int charmed;
	public bool confused;
	
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
			case preAtks.princess:
				prePrincess();
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
			case Atks.heal:
				atkHeal();
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
		organize,
		princess
    }

    public enum Atks {
        none,
		basic,
        ranged,
		poison,
		sharpShooter,
		heal
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

	public int damage() {
		// damage calculation so easier to implement buffs and debuffs
		double dmg = attack;
		if(myTeam.team[myTeam.team.IndexOf(this) - 1].name == "lucky pirate"){
			dmg *= 1.25;
		}
		if(charmed != 0){
			int rng = Random.Range(1,100);
			if(charmed == 1){
				if(rng <= 20){
					dmg = 0;
				}
			}
			if(charmed == 2){
				if(rng <= 15){
					dmg = 0;
				}
			}
			if(charmed == 3){
				if(rng <= 10){
					dmg = 0;
				}
			}
			if(charmed == 4){
				if(rng <= 5){
					dmg = 0;
				}
			}
		}
		
		return (int)dmg;
	}

	// pre attacks
	void preOrganize() {
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
	
	void prePrincess() {
		int pos = 0;
		foreach (Pirate p in enemyTeam.team) {
            if(pos == 0){
				p.charmed = 1;
			}
			if(pos == 1){
				p.charmed = 2;
			}
			if(pos == 2){
				p.charmed = 3;
			}
			if(pos == 3){
				p.charmed = 4;
			}
			pos++;
        }
	}
	
	// attacks
	
    void atkRanged() {
        //code for ranged attack
		if(enemyTeam.team[0].name == "confused"){
			if(myTeam.team.IndexOf(this) < myTeam.team.Count){
				myTeam.team[myTeam.team.IndexOf(this) + 1].health -= damage();
			}
		}
		else{
			enemyTeam.team[0].health -= damage();
		}
    }
	
	void atkBasic() {
        //code for basic attack
		if(enemyTeam.team[0].name == "confused"){
			if(myTeam.team.IndexOf(this) < myTeam.team.Count){
				myTeam.team[myTeam.team.IndexOf(this) + 1].health -= damage();
				health -= myTeam.team[myTeam.team.IndexOf(this) + 1].damage();
			}
		}
		else{
			enemyTeam.team[0].health -= damage();
			health -= enemyTeam.team[0].damage();
		}
    }
	
	void atkPoison() {
		if(enemyTeam.team[0].name == "confused"){
			if(myTeam.team.IndexOf(this) < myTeam.team.Count){
				myTeam.team[myTeam.team.IndexOf(this) + 1].poison = true;
			}
		}
		else{
			enemyTeam.team[0].poison = true;
		}
	}
	
	void atkSharpShooter() {
		if(enemyTeam.team[0].name == "confused"){
			if(myTeam.team.IndexOf(this) < myTeam.team.Count){
				myTeam.team[myTeam.team.IndexOf(this) + 1].health -= damage();
			}
		}
		else{
			enemyTeam.team[enemyTeam.team.Count].health -= damage();
		}
	}
	
	void atkHeal() {
		if(myTeam.team.IndexOf(this) >= 1){
			myTeam.team[myTeam.team.IndexOf(this) - 1].health += (int)myTeam.team[myTeam.team.IndexOf(this) - 1].maxhealth / 2;
		}
	}
	
	// post attacks
	void pstPoison() {
		foreach (Pirate p in enemyTeam.team) {
            if (p.poison == true) {
                p.health -= 2;
            }
        }
	}
}
