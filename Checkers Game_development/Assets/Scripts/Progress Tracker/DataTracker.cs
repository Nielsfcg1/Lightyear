using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataTracker : MonoBehaviour
{
	//Scripts
	GameManager gameManager;

	[HeaderAttribute("The XP Screen")]
	public GameObject xPScreen;
	public GameObject progressTracker;

	[HeaderAttribute("Variables of the current Level")]
	static int timePlayed;
	static int movesMade;

	[HeaderAttribute("Variables that the LGS decides")]
	float difficulty = 1;
	int minimumMoves;
	int minimumPlayTime = 40;
	int maxScore = 890;

	static float maximumMoves;
	static float maximumTime;

	[HeaderAttribute("The UI elements that hold the data")]
	public Text levelScoreText;
	public Text movesUsedText;
	public Text timePlayedText;
	public Text totalScoreText;

	[HeaderAttribute("The UI elements that hold the data")]
	public int bonusXp;

	static float levelBonus;
	static float moveBonus;
	static float timeBonus;
	static float totalScore;

	[HeaderAttribute("Current and next level variables")]
	static int curLevelColors;
	static int nextLevelColors;

	static int curLevelmultipleOrbs;
	static int nextLevelmultipleOrbs;

	static bool curLevelLoop;
	static bool nextLevelLoop;

	static bool curLevelPlaceOrb;
	static bool nextLevelPlaceOrb;

	static bool curLevelPlaceAvatar;
	static bool nextLevelPlaceAvatar;

	[HeaderAttribute("OrbPrefabs")]
	public List<GameObject> orbs = new List<GameObject> ();

	//Local variables
	float playTime;
    int gameXp;
    LevelGenerator lg;
	ShapeDatabase sd;

	public static int levelType = 0;

    public static bool _theDirtiestMFBOOLEANuHAFE4rSeen = true;
    public static bool firstTime = true;

	bool dirty = false;

    void Awake()
	{
		//print ("awake");
		if (SceneManager.GetActiveScene ().name == "Game")
		{
			gameManager = GetComponent<GameManager> ();
			lg = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>();
			sd = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShapeDatabase>();

            //Must only create a level the first time like this
            if (_theDirtiestMFBOOLEANuHAFE4rSeen)
            {
				lg.CreateLevel(levelType);
				dirty = true;
                _theDirtiestMFBOOLEANuHAFE4rSeen = false;
            }

        }
	}

	void Update ()
	{
		if (SceneManager.GetActiveScene ().name == "Game")
		{
			playTime += Time.deltaTime;
		}
	}

	public void GetInfo ()
	{
//		difficulty = levelGenerator.difficulty;
//		minimumMoves = levelGenerator.minimumMoves;
//		minimumPlayTime = levelGenerator.minimumPlayTime;
	}

	public void CalculateXP ()
	{
		timePlayed = (int)Mathf.Round (playTime);
		movesMade = gameManager.moves;

		//Calculate the score according to all the formulas

		//levelBonus
		//Temp variable untill we have access to the level generator variables
		//gameXp = 100;
		

        /* Nodig uit de level generator

		Hoeveel kleuren zitten er in het level?
		Welke vormen? Lus geeft bonus
		Welke mechanics zitten er in het level?
		Zijn er meerdere orbs per tile?
		Moet je de avatar plaatsen?
		Hoeveel hints zijn er mogelijk?
		
		*/


        //Kleuren in level
		for(int i = 0; i < lg.shapesInLevel.Count; i++)
        {
            gameXp += 100;
        }
		curLevelColors = lg.orbTypes.Count;

        //If a shape was a loop
        for(int i = 0; i < lg.shapes.Count; i++)
        {
            if(lg.shapes[i] == 2)
            {
                gameXp += 30;
				curLevelLoop = true;
            }
        }

        //Welke mechanics (check removeRandomOrb bool)
        if(lg.removeRandomOrb)
        {
            gameXp += 400;
			curLevelPlaceOrb = true;
        }

        //Meerdere orbs per tile
        gameXp += (lg.tilesWithMultipleOrbs * 10);
		curLevelmultipleOrbs = lg.tilesWithMultipleOrbs;

        //Moet je de avatar plaatsen?
        if (!lg.placeAvatar)
        {
            gameXp += 50;
			curLevelPlaceAvatar = true;
        }

		levelBonus = gameXp;


		difficulty = 1- (levelBonus / maxScore);

        //moveBonus
        minimumMoves = gameManager.allOrbs.Count;
		maximumMoves = minimumMoves + (minimumMoves * (difficulty));
		moveBonus = (maximumMoves - movesMade) * bonusXp;

		if (moveBonus <= 0)
			moveBonus = 0;
		
		//timeBonus
		maximumTime = minimumPlayTime + (minimumPlayTime * (difficulty));
		timeBonus = (maximumTime - timePlayed) * bonusXp;
		if (timeBonus <= 0)
			timeBonus = 0;



        totalScore = (levelBonus + moveBonus + timeBonus);

        SceneManager.LoadScene ("Progress");
	}

	void SetXPVariables ()
	{
		levelScoreText.text = Mathf.RoundToInt(levelBonus).ToString ();
		movesUsedText.text = Mathf.RoundToInt(moveBonus).ToString ();
		timePlayedText.text = Mathf.RoundToInt(timeBonus).ToString ();
		totalScoreText.text = Mathf.RoundToInt(totalScore).ToString ();
	}

	public void SetXPScreen (bool state)
	{
		if (state == true)
		{
			xPScreen.SetActive (true);
		}
		if (state == false)
		{
			//Animation here
			xPScreen.SetActive (false);
		}
	}

	public void SetProgressTracker (bool state)
	{
		if (state == true) 
		{
			progressTracker.SetActive (true);
			progressTracker.GetComponent<XPBar> ().AddXP (totalScore);
			ResetVariables ();
		}
		if (state == false)
		{
			//Animation here
			//ResetVariables ();
			progressTracker.SetActive (false);
			SceneManager.LoadScene ("Game");
		}
	}

	void ResetVariables ()
	{
		levelBonus = 0;
		moveBonus = 0;
		timeBonus = 0;
		totalScore = 0;

		playTime = 0;
	}

	void OnLevelWasLoaded ()
	{
		if (SceneManager.GetActiveScene ().name == "Progress")
		{
			SetXPVariables ();

		}
		else if (SceneManager.GetActiveScene ().name == "Game")
		{
            if(firstTime)
            {
                firstTime = false;
            }
			else if (!dirty)
           	{
                if (movesMade <= maximumMoves && timePlayed <= maximumTime)
                {
                    CreateNextLevel(1);
                }
                else
                {
                    //Create a sort like level
                    CreateNextLevel(0);
                    //print("Zelfde Level");
                }
            }
        }
	}

	void CreateNextLevel (int amount)
	{
//		if (lg.orbTypes.Count < 3)
//		{
//			AddOrbToLevel ();
//		}
		if (levelType < 6)
		{
			//print ("Difficulty up");
			lg.CreateLevel (levelType + amount);
			levelType++;
		}
		else
		{
			lg.CreateLevel (levelType);
			//print ("Difficulty same");
		}
	}

    void AddOrbToLevel()
    {
        int number = Random.Range(0, orbs.Count);

        if (!lg.orbTypes.Contains(orbs[number]))
        {
            lg.orbTypes.Add(orbs[number]);
        }
        else if (lg.orbTypes.Contains(orbs[number]))
            AddOrbToLevel();
    }

	public void ResetDirty ()
	{
		_theDirtiestMFBOOLEANuHAFE4rSeen = true;
	}
}
