using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public GameObject pirateObject;
    private List<GameObject> pirates;


    public Side side;
    public List<Pirate> team = new List<Pirate>();
    public Transform[] pirateSlots;

    public bool[] availableSlots;

    public bool debug;

    public TeamManager enemyTeam;

    // Start is called before the first frame update
    void Start()
    {
        FlipAsNeeded();

        SetTeam();
    }
    
    void FlipAsNeeded() {
        if (side == Side.Left) {
            for (int i = 0; i < 4; i++) {
                Vector3 pos = pirateSlots[i].position;
                pos.x -= (192 * (i+1)) * 2;
                pirateSlots[i].position = pos;
            }
        }
    }

    void CreatePirates() {
        for(int i = 0; i < team.Count; i++) {
            Pirate pirate = team[i];
            if (pirate == null) {
                continue;
            }

            GameObject newPirate = Instantiate(pirateObject, pirateSlots[i], true);
            newPirate.GetComponent<PirateDisplay>().SetPirate(pirate);
            newPirate.transform.position = pirateSlots[i].position;

            pirates.Add(newPirate);

            //pirate.Print();
        }
    }

    public void SetTeam(List<Pirate> newTeam) {
        foreach (GameObject pirate in pirates) {
            Destroy(pirate);
        }

        team = newTeam;
        SetTeams();
        CreatePirates();
    }

    public void SetTeams() {
        foreach (Pirate p in team) {
            p.SetTeams(this, enemyTeam);
        }
    }

    public void SetEnemyTeam(TeamManager enemy) {
        enemyTeam = enemy;
        SetTeams();
    }

    public void PreAttack() {
        foreach (Pirate p in team) {
            p.PreAttack();
        }
    }

    public void Attack() {
        foreach (Pirate p in team) {
            p.Attack();
        }
    }

    public void PostAttack() {
        foreach (Pirate p in team) {
            p.PostAttack();
        }
    }

    public void CheckPirates() {
        foreach (Pirate p in team) {
            if (p.health <= 0) {
                team.Remove(p);
            }
        }
        SetTeam(team);
    }

    public void Print() {
        foreach (Pirate p in team) {
            p.Print();
        }
    }
}

public enum Side {
    Left,
    Right
}
