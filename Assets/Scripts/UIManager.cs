using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject LevelManager;

    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = LevelManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        game.RunBattle();
    }

    public void PlayRound() {
        game.RunRound();
    }

    public void  QuitGame() {
        Application.Quit();
    }

}
