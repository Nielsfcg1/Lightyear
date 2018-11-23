using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public int orbType;
	public Sprite orbSprite;

	AudioSource pickUpSound;
    
    GameManager gm;

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		pickUpSound = GameObject.Find ("Pick Up").GetComponent<AudioSource> ();
    }

    //When the orb is being picked up
    public void OrbBehaviour()
    {
        //Decrease orbCount by one    
        gm.OrbsLeft[orbType]--;
		pickUpSound.Play ();
		gameObject.SetActive (false);

        //If there aren't any orbs of that color left, reset current color
        if(gm.OrbsLeft[orbType] == 0)
        {
            gm.currentColorINT = -1;
            gm.UpdateColorCounter(1, gm.colorAmount);
        }
    }
}
