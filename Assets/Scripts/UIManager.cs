using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject LevelManager;
	public AudioSource audio1;
	public AudioSource audio2;

    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = LevelManager.GetComponent<GameManager>();
		audio1.Play();
		audio2.Pause();
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
