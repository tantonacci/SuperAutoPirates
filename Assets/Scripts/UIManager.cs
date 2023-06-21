using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject LevelManager;
    public GameObject BattleUI;
    public GameObject WinScreen;
    public GameObject LoseScreen;
	public AudioSource audio1;
	public AudioSource audio2;

    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = LevelManager.GetComponent<GameManager>();
		audio1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win() {
        LevelManager.SetActive(false);
        BattleUI.SetActive(false);
        WinScreen.SetActive(true);
    }

    public void Lose() {
        LevelManager.SetActive(false);
        BattleUI.SetActive(false);
        LoseScreen.SetActive(true);
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

	public void PlayAudio1() {
		audio1.Play();
	}
	public void PauseAudio1() {
		audio1.Pause();
	}
	public void PlayAudio2() {
		audio2.Play();
	}
	public void PauseAudio2() {
		audio2.Pause();
	}
}
