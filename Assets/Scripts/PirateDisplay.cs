using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PirateDisplay : MonoBehaviour {

    public Pirate pirate;
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;

    public UnityEngine.UI.Image artwork;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;

    public GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValues();
    }

    void UpdateValues() {
        if (pirate == null) {
            return;
        }
        nameText.text = pirate.name;
        descText.text = pirate.desc;

        if (pirate.IsLeftTeam()) {
               
            Vector3 theScale = artwork.transform.localScale;
            theScale.x *= -1;
			if(artwork.transform.localScale.x > 0){
				artwork.transform.localScale = theScale;
			}
        }
        artwork.sprite = pirate.artwork;

        attackText.text = pirate.attack.ToString();
        healthText.text = pirate.health.ToString();
    }

    public void SetPirate(Pirate newPirate) {
        pirate = newPirate;
        UpdateValues();
    }

    public void AddPirateToTeam() {
        game.AddPirateToTeam(pirate);
    }
}
