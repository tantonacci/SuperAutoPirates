using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetTeamBtn : MonoBehaviour
{

    public List<Pirate> team = new List<Pirate>();
    public TeamManager enemyTeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyTeam() {
        enemyTeam.SetTeam(team);
    }
}
