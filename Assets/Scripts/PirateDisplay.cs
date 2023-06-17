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

        artwork.sprite = pirate.artwork;

        attackText.text = pirate.attack.ToString();
        healthText.text = pirate.health.ToString();
    }

    public void SetPirate(Pirate newPirate) {
        pirate = newPirate;
        UpdateValues();
    }
}