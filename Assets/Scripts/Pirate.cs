using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Pirate", menuName = "Pirate")]
public class Pirate : ScriptableObject {

#region "Variables"
    public bool debug;

    public string name;
    public string desc;

    public Sprite artwork;

    public int attack;
    public int health;

    private int slotNum;

    public PirateType pirateType;

    private TeamManager myTeam;
    private TeamManager enemyTeam;

	private Pirate target;

	public bool poison;
	public bool confused;
	public bool music;
	public bool muscleTurn;
	public bool disabled;
#endregion

#region "Properties"

    public void SetTeams(TeamManager team1, TeamManager team2) {
        myTeam = team1;
        enemyTeam = team2;

        SetSlotNum();
    }

    private void SetSlotNum() {
        slotNum = myTeam.team.IndexOf(this);
    }

	public void TakeDamage(int dmg, Pirate attacker) {

		if (pirateType == PirateType.rubber) {
			int returnDmg = dmg/2;

			if (returnDmg > 0) attacker.TakeDamage(returnDmg, this);
		}

		if (pirateType == PirateType.millenial) {
			attacker.attack -= 2;
		}

		if(myTeam.GetPirate(slotNum) != null){
			if(myTeam.GetPirate(slotNum).pirateType == PirateType.repair){
				dmg -= 1;
			}
		}
		if(myTeam.GetPirate(slotNum + 1) != null){
			if(myTeam.GetPirate(slotNum + 1).pirateType == PirateType.repair){
				dmg -= 1;
			}
		}
		if(myTeam.GetPirate(slotNum - 1) != null){
			if(myTeam.GetPirate(slotNum - 1).pirateType == PirateType.repair){
				dmg -= 1;
			}
		}
		if (dmg < 0) return;
		
		if(pirateType == PirateType.anarchist){
			dmg /= myTeam.team.Count;
			if (dmg == 0) dmg = 1;

			foreach(Pirate p in myTeam.team){
				p.health -= dmg;
			}
			return;
		}
		else if(pirateType == PirateType.quick){
			int rng = Random.Range(1, 100);
			if(rng < 50){
				return;
			}
		}

		health -= dmg;


		if (music) {
			health -= 1;
		}
	}

	public void Heal(int heal) {
		if (heal < 0) return;

		health += heal;
	}
		
	public void SetTarget(Pirate p) {
		target = p;
	}

#endregion

#region "Utility"

    public void Print() {
        if (debug) Debug.Log(name + "(" + attack + "/" + health +") in Slot " + slotNum);
    }

    public void Print(string str) {
        if (debug) Debug.Log(str);
    }

	public void SetDebug(bool dbg) {
        debug = dbg;
    }

	public void ShowAsCard(bool card) {
		//TODO: Impliment
	}

#endregion

#region "Basic Battle Functions"

	public void StartOfRound() {
		switch(pirateType) {
			case PirateType.sharpShooter:
				SetTarget(enemyTeam.GetLastPirate());
				break;
			default:
				SetTarget(enemyTeam.GetFirstPirate());
				break;
		}

		music = false;
		disabled = false;
	}

    public void PreAttack() {
        Print();
		if (disabled) {
			return;
		}

        switch(pirateType) {
            case PirateType.none:
                Print("--PreAttack (none)--");
                break;
			default:
            case PirateType.basic:
                Print("--PreAttack (basic)--");
                break;
            case PirateType.ranged:
                Print("--PreAttack (ranged)--");
                break;
			case PirateType.organize:
				Print("--PreAttack (organize)--");
				preOrganize();
				break;
			case PirateType.princess:
				Print("--PreAttack (princess)--");
				prePrincess();
				break;
			case PirateType.sharpShooter:
				Print("--PreAttack (sharpshooter)--");
				preSharpShooter();
				break;
			case PirateType.armory:
				Print("--PreAttack (armory)--");
				preArmory();
				break;
			case PirateType.music:
				Print("--PreAttack (music)--");
				preMusic();
				break;
			case PirateType.confuse:
				Print("--PreAttack (confuse)--");
				preConfuse();
				break;
			case PirateType.grapple:
				Print("--PreAttack (grapple)--");
				preGrapple();
				break;
			case PirateType.hack:
				Print("--PreAttack (hack)--");
				preHack();
				break;
			case PirateType.policeCaptain:
				Print("--PreAttack (police captain)--");
				prePoliceCaptain();
				break;
			case PirateType.distributor:
				Print("--PreAttack (distributor)--");
				preDistributor();
				break;
			case PirateType.lucky:
				Print("--PreAttack (lucky)--");
				preLucky();
				break;
        }
    }

    public void Attack() {
        Print();
		if (disabled) {
			return;
		}

        switch(pirateType) {
            case PirateType.none:
                Print("--Attack (none)--");
                break;
			default:
            case PirateType.basic:
                Print("--Attack (basic)--");
                atkBasic();
                break;
            case PirateType.ranged:
                Print("--Attack (ranged)--");
                atkRanged();
                break;
			case PirateType.poison:
				Print("--Attack (poison)--");
				atkPoison();
				break;
			case PirateType.cyborg:
				Print("--Attack (cyborg)--");
				atkCyborg();
				break;
			case PirateType.splash:
				Print("--Attack (splash)--");
				atkSplash();
				break;
			case PirateType.gamble:
				Print("--Attack (gamble)--");
				atkGamble();
				break;
			case PirateType.muscle:
				Print("--Attack (muscle)--");
				atkMuscle();
				break;
			case PirateType.space:
				Print("--Attack (space)--");
				atkSpace();
				break;
			case PirateType.police:
				Print("--Attack (police)--");
				atkPolice();
				break;
        }
    }

    public void PostAttack() {
        Print();
		if (poison) {
			health -= 2;
		}

		if (disabled) {
			return;
		}

        switch(pirateType) {
            case PirateType.none:
                Print("--PostAttack (none)--");
                break;
			default:
            case PirateType.basic:
                Print("--PostAttack (basic)--");
                break;
            case PirateType.ranged:
                Print("--PostAttack (ranged)--");
                break;
			case PirateType.heal:
				Print("--PostAttack (heal)--");
				pstHeal();
				break;
			case PirateType.flameShooter:
				Print("--PostAttack (flameshooter)--");
				pstFlameShooter();
				break;
			case PirateType.teacher:
				Print("--PostAttack (teacher)--");
				pstTeacher();
				break;
			case PirateType.sapper:
				Print("--PostAttack (sapper)--");
				pstSapper();
				break;
        }
    }

	public enum PirateType {
		none,
        basic,
        ranged,
		organize,
		princess,
		sharpShooter,
		armory,
		music,
		confuse,
		grapple,
		hack,
		policeCaptain,
		distributor,
		poison,
		cyborg,
		sapper,
		teacher,
		flameShooter,
		heal,
		police,
		space,
		muscle,
		gamble,
		splash,
		lucky,
		repair,
		anarchist,
		quick,
		rubber,
		millenial
	}

#endregion

    //Standard:
    // PreAttacks start with pre
    // Attacks start with atk
    // PostAttacks start with pst

#region "PreAttack Functions"
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

	void preLucky() {
		if (slotNum == 0) {
			return;
		}

		Pirate p = myTeam.GetPirate(slotNum - 1);
		p.attack += 2;
	}
	
	void prePrincess() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		int pos = 0;
		foreach (Pirate p in enemyTeam.team) {
			int rng = Random.Range(1,100);
			switch(pos) {
				case 0:
					if(rng <= 20){
						p.disabled = true;
					}
					break;
				case 1:
					if(rng <= 15){
						p.disabled = true;
					}
					break;
				case 2:
					if(rng <= 10){
						p.disabled = true;
					}
					break;
				case 3:
					if(rng <= 5){
						p.disabled = true;
					}
					break;
			}
			pos++;
        }
	}
	
	void preSharpShooter() {
		Pirate p = enemyTeam.GetLastPirate();
		p.TakeDamage(attack, this);
	}
	
	void preArmory() {
		Pirate p1 = myTeam.GetPirate(slotNum - 1);
		Pirate p2 = myTeam.GetPirate(slotNum + 1);

		if (p1 != null) p1.Heal(2);
		if (p2 != null) p2.Heal(2);
	}
		
	void preConfuse() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		if(Random.Range(1,100) > 50) {
			Pirate p = enemyTeam.GetFirstPirate();
			Pirate p2 = enemyTeam.GetPirate(1); //Getting Second pirate
			if (p2 != null) {
				p.SetTarget(p2);
			}
		}
	}

	void preMusic() {
		foreach (Pirate p in enemyTeam.team) {
            p.music = true;
        }
	}
	
	void preGrapple() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		Pirate p = enemyTeam.GetFirstPirate();
		int rng = Random.Range(1, 100);
		if(rng <= 50){
			p.disabled = true;
		}
	}
	
	void preHack() {
		Pirate p = enemyTeam.GetRandomPirate();
		int rng = Random.Range(1, 100);
		if(rng <= 50){
			p.disabled = true;
		}
	}
	
	void prePoliceCaptain() {
		foreach (Pirate p in myTeam.team) {
			if(p.pirateType == PirateType.police || 
						p.pirateType == PirateType.policeCaptain){
				attack += 1;
				Heal(1);
			}
		}
	}
	
	void preDistributor() {
		Pirate p = myTeam.GetRandomPirate();
		if(Random.Range(0, 1) == 0) {
			p.attack += 3;
		} else {
			p.Heal(3);
		}
	}
	
	void preMechanize() {
		foreach (Pirate p in enemyTeam.team) {
			if(p.pirateType == PirateType.repair){
				attack += 5;
				Heal(5);
				break;
			}
        }
	}
	
#endregion

#region "Attack Functions"
	// attacks

	void atkBasic() {
        if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

		target.TakeDamage(attack, this);
    }
	
	void atkRanged() {
		target.TakeDamage(attack, this);
    }
	
	void atkPoison() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

        target.poison = true;
		target.TakeDamage(attack, this);
	}
	
	void atkCyborg() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		
		foreach(Pirate p in enemyTeam.team){
			p.TakeDamage(attack, this);
		}
	}
	
	void atkSplash() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

		if (Random.Range(0, 100) > 20) {
			target.TakeDamage(100, this);
		} else {
        	target.TakeDamage(attack, this);
		}
	}
	
	void atkGamble() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

		int rng = Random.Range(0,100);
		int tempAttack = (int)(attack * ((50.0 + rng) / 100.0));
		target.TakeDamage(tempAttack, this);
	}
	
	void atkMuscle() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }
		if(!muscleTurn){
			muscleTurn = true;
			return;
		}
		target.TakeDamage(attack, this);
	}
	
	void atkSpace() {
		if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

		int rng = Random.Range(1,3);
		target.attack -= rng;
		if(target.attack <= 0){
			target.attack = 1;
		}
	}
	
	void atkPolice() {
        if (slotNum != 0) {
            Print("Not slot 0, no attack");
            return;
        }

		target.TakeDamage(attack, this);

		Pirate p = enemyTeam.GetRandomPirate();
		p.TakeDamage((int)Mathf.Floor(attack / 2), this);
    }

#endregion	
	
#region "PostAttack Functions"
	// post attacks

	void pstHeal() {
		Pirate p = myTeam.GetPirate(slotNum - 1);

		if (p != null) {
			p.Heal(3);
		}
	}
	
	void pstFlameShooter() {
		Pirate p = enemyTeam.GetFirstPirate();
		p.TakeDamage(attack, this);
	}
	
	void pstTeacher() {
		Pirate p = myTeam.GetPirate(slotNum + 1);

		if (p == null) {
			return;
		}

		p.attack += 1;
		p.Heal(1);
	}
	
	void pstSapper() {
		Pirate enemy = enemyTeam.GetRandomPirate();
		Pirate friend = myTeam.GetRandomPirate();

		enemy.TakeDamage(2, this);
		friend.Heal(2);
	}

#endregion
	
}
