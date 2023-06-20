using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public GameObject pirateObject;
    private List<GameObject> pirates = new List<GameObject>();


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

        SetTeam(team);
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
        }
    }

    public void AddPirateToTeam(Pirate p) {
        if (team.Count < 4) {
            team.Add(p);
            SetTeam(team);
        }
    }

    public void SetTeam(List<Pirate> newTeam) {
        foreach (GameObject pirate in pirates) {
            Destroy(pirate);
        }

        team = newTeam;

        for (int i = 0; i < team.Count; i++) {
            team[i] = Instantiate(team[i]);
        }

        SetTeams();
        CreatePirates();
    }

    public void ClearTeam() {
        team.Clear();
        SetTeam(team);
    }

    public void SetTeams() {
        foreach (Pirate p in team) {
            p.SetTeams(this, enemyTeam);
        }
    }

    public void SetEnemyTeam(TeamManager enemy) {
        enemyTeam = enemy;
    }

    public void SetDebug(bool dbg) {
        debug = dbg;
        foreach (Pirate p in team) {
            p.SetDebug(dbg);
        }
    }

    public void StartOfRound() {
        foreach (Pirate p in team) {
            p.StartOfRound();
        }
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

    public bool CheckPirates() {
        for (int i = team.Count - 1; i >= 0; i--) {
            Pirate p = team[i];
            if (p.health <= 0) {
                team.Remove(p);
            }
        }

        SetTeam(team);

        return team.Count == 0;
    }

    public void Print() {
        foreach (Pirate p in team) {
            p.Print();
        }
    }

    public Pirate GetPirate(int p) {
        if (p < 0 || team.Count < p + 1) {
            return null;
        }

        return team[p];
    }

    public Pirate GetFirstPirate() {
        return GetPirate(0);
    }
    
    public Pirate GetLastPirate() {
        return GetPirate(team.Count - 1);
    }

    public Pirate GetRandomPirate() {
        if (team.Count == 0) {
            return null;
        }

        //Random random = new Random();
        return team[Random.Range(0,team.Count)];
    }

    public bool HasMembersRemaining() {
        return team.Count > 0;
    }
}

public enum Side {
    Left,
    Right
}
