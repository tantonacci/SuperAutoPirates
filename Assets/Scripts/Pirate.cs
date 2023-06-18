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
	public int maxhealth;
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

	public bool poison;
	public int charmed;
	public bool confused;
	public bool music;
	public bool muscleTurn;
	
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
			case preAtks.organize:
				Print("--PreAttack (organize)--");
				preOrganize();
				break;
			case preAtks.princess:
				Print("--PreAttack (princess)--");
				prePrincess();
				break;
			case preAtks.sharpShooter:
				Print("--PreAttack (sharpshooter)--");
				preSharpShooter();
				break;
			case preAtks.armory:
				Print("--PreAttack (armory)--");
				preArmory();
				break;
			case preAtks.music:
				Print("--PreAttack (music)--");
				preMusic();
				break;
			case preAtks.confuse:
				Print("--PreAttack (confuse)--");
				preConfuse();
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
			case Atks.poison:
				Print("--Attack (poison)--");
				atkPoison();
				break;
			case Atks.cyborg:
				Print("--Attack (cyborg)--");
				atkCyborg();
				break;
			case Atks.splash:
				Print("--Attack (splash)--");
				atkSplash();
				break;
			case Atks.gamble:
				Print("--Attack (gamble)--");
				atkGamble();
				break;
			case Atks.muscle:
				Print("--Attack (muscle)--");
				atkMuscle();
				break;
			case Atks.space:
				Print("--Attack (space)--");
				atkSpace();
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
			case postAtks.heal:
				Print("--PostAttack (heal)--");
				pstHeal();
				break;
			case postAtks.dullshooter:
				Print("--PostAttack (dullshooter)--");
				pstDullshooter();
				break;
			case postAtks.teacher:
				Print("--PostAttack (teacher)--");
				pstTeacher();
				break;
        }
    }

    public enum preAtks {
        none,
        basic,
        ranged,
		organize,
		princess,
		sharpShooter,
		armory,
		music,
		confuse
    }

    public enum Atks {
        none,
		basic,
        ranged,
		poison,
		cyborg,
		splash,
		gamble,
		muscle,
		space
    }

    public enum postAtks {
        none,
        basic,
        ranged,
		heal,
		dullshooter,
		teacher
    }


    //Standard:
    // PreAttacks start with pre
    // Attacks start with atk
    // PostAttacks start with pst

	public int damage() {
		// damage calculation so easier to implement buffs and debuffs
		double dmg = attack;
		if(slotNum < myTeam.team.Count - 1){
			if(myTeam.team[slotNum + 1].name == "lucky pirate"){
				int rng = Random.Range(20,50);
				dmg *= ((100.0 + rng) / 100.0);
			}
			if(myTeam.team[slotNum + 1].name == "repair pirate"){
				dmg *= .6;
			}
		}
		if(slotNum > 0){
			if(myTeam.team[slotNum - 1].name == "repair pirate"){
				dmg *= .6;
			}
		}
		if(charmed != 0){
			int rng = Random.Range(1,100);
			if(charmed == 1){
				if(rng <= 20){
					return 0;
				}
			}
			if(charmed == 2){
				if(rng <= 15){
					return 0;
				}
			}
			if(charmed == 3){
				if(rng <= 10){
					return 0;
				}
			}
			if(charmed == 4){
				if(rng <= 5){
					return 0;
				}
			}
		}
		if(music){
			dmg *= 1.25;
		}
		
		return (int)dmg;
	}

	// pre attacks
	void preOrganize() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

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
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
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
	
	void preSharpShooter() {
		if(enemyTeam.team[0].confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count - 1){
					myTeam.team[slotNum + 1].TakeDamage((int)(.6 * damage()));
				}
			}
		}
		else if(enemyTeam.team[enemyTeam.team.Count].name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
			}
		}
		else if(enemyTeam.team[enemyTeam.team.Count].name == "shield pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
			}
		}
		else{
			enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
		}
	}
	
	void preArmory() {
		if(slotNum < myTeam.team.Count - 1){
			myTeam.team[slotNum + 1].maxhealth += 2;
			myTeam.team[slotNum + 1].health += 2;
		}
		if(slotNum > 0){
			myTeam.team[slotNum - 1].maxhealth += 2;
			myTeam.team[slotNum - 1].health += 2;
		}
	}
	
	void preMusic() {
		foreach (Pirate p in enemyTeam.team) {
            p.music = true;
        }
	}
	
	void preConfuse() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		foreach (Pirate p in enemyTeam.team) {
            p.confused = true;
        }
	}
	
	
	// attacks

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

		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					myTeam.team[slotNum + 1].TakeDamage((int)(.6 * damage()));
				}
			}
		}
		else if(enemy.name == "repair pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				TakeDamage(enemy.damage());
			}
		}
		else if(enemyTeam.team[enemyTeam.team.Count].name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
			}
		}
		else{
			enemy.TakeDamage(damage());
			TakeDamage(enemy.damage());
		}
    }
	
	void atkRanged() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        Pirate enemy = enemyTeam.GetFirstPirate();
		
		if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }
		
		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					myTeam.team[slotNum + 1].TakeDamage(damage());
				}
			}
		}
		else if(enemy.name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
			}
		}
		else if(enemyTeam.team[enemyTeam.team.Count].name == "shield pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemyTeam.team[enemyTeam.team.Count].TakeDamage(damage());
			}
		}
		else{
			enemy.TakeDamage(damage());
		}
    }
	
	void atkPoison() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        Pirate enemy = enemyTeam.GetFirstPirate();

        if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }

		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					myTeam.team[slotNum + 1].TakeDamage((int)(.6 * damage()));
					myTeam.team[slotNum + 1].poison = true;
				}
			}
		}
		else if(enemy.name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				TakeDamage(enemy.damage());
			}
		}
		else{
			enemy.TakeDamage(damage());
			TakeDamage(enemy.damage());
			enemy.poison = true;
		}
	}
	
	void atkCyborg() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		foreach(Pirate p in enemyTeam.team){
			if(p.name == "shield pirate"){
				int rng = Random.Range(1,100);
				if(rng <= 50){
					continue;
				}
			}
			p.TakeDamage(damage());
		}
	}
	
	void atkSplash() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        Pirate enemy = enemyTeam.GetFirstPirate();

        if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }

		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					myTeam.team[slotNum + 1].TakeDamage((int)(.6 * damage()));
				}
			}
		}
		else if(enemy.name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				TakeDamage(enemy.damage());
			}
		}
		else{
			enemy.TakeDamage(damage());
			TakeDamage(enemy.damage());
			int rng = Random.Range(1,100);
			if(rng <= 35){
				enemy.health = 0;
			}
		}
	}
	
	void atkGamble() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        Pirate enemy = enemyTeam.GetFirstPirate();

        if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }

		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					int rng2 = Random.Range(0,100);
					int temp = attack;
					attack = (int)(attack * ((50.0 + rng2) / 100.0));
					myTeam.team[slotNum + 1].TakeDamage((int)(.6 * damage()));
					attack = temp;
				}
			}
		}
		else if(enemy.name == "repair pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				int rng2 = Random.Range(0,100);
				int temp = attack;
				attack = (int)(attack * ((50.0 + rng2) / 100.0));
				enemy.TakeDamage(damage());
				attack = temp;
			}
		}
		else{
			int rng = Random.Range(0,100);
			int temp = attack;
			attack = (int)(attack * ((50.0 + rng) / 100.0));
			enemy.TakeDamage(damage());
			attack = temp;
			TakeDamage(enemy.damage());
		}
	}
	
	void atkMuscle() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		if(!muscleTurn){
			muscleTurn = true;
		}
		else{
			atkBasic();
		}
	}
	
	void atkSpace() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		Pirate enemy = enemyTeam.GetFirstPirate();

        if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }
		
		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				int rng2 = Random.Range(1,3);
				enemy.attack -= rng2;
				enemy.maxhealth -= rng;
				if(enemy.health > maxhealth){
					enemy.health = maxhealth;
				}
			}
		}
		else{
			int rng = Random.Range(1,3);
			enemy.attack -= rng;
			enemy.maxhealth -= rng;
			if(enemy.health > maxhealth){
				enemy.health = maxhealth;
			}
		}
	}
	
	
	// post attacks

	void pstHeal() {
		if(slotNum >= 1){
			myTeam.team[slotNum - 1].health += (int)myTeam.team[slotNum - 1].maxhealth / 2;
		}
	}
	
	void pstDullshooter() {
		Pirate enemy = enemyTeam.GetFirstPirate();
		
		if (enemy == null) {
            Print("No enemy pirate to attack");
            return;
        }
		
		if(enemy.confused){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				if(slotNum < myTeam.team.Count){
					myTeam.team[slotNum + 1].TakeDamage(damage());
				}
			}
		}
		else if(enemy.name == "quick pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemy.TakeDamage(damage());
			}
		}
		else if(enemy.name == "shield pirate"){
			int rng = Random.Range(1,100);
			if(rng <= 50){
				enemy.TakeDamage(damage());
			}
		}
		else{
			enemy.TakeDamage(damage());
		}
	}
	
	void pstTeacher() {
		if(slotNum < myTeam.team.Count - 1){
			myTeam.team[slotNum + 1].maxhealth += 1;
			myTeam.team[slotNum + 1].health += 1;
			myTeam.team[slotNum + 1].attack += 1;
		}
	}
	
}
