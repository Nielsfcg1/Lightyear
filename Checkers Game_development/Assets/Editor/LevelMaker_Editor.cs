using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(LevelMaker))]
public class LevelMaker_Editor : Editor
{
	public override void OnInspectorGUI ()
	{
		//Make a custom inspector with public variables
		DrawDefaultInspector ();

		//The script that gets the custom inspector
		LevelMaker levelMaker = (LevelMaker)target;

		//Fill the guistyle array
		levelMaker.guiStyles = new GUIStyle[] {levelMaker.normal, levelMaker.start, levelMaker.blue, levelMaker.red, levelMaker.yellow, levelMaker.blueYellow, levelMaker.redBlue, levelMaker.yellowRed, levelMaker.portals};

		//GUI styles for all tiles
		levelMaker.normal = new GUIStyle (GUI.skin.button);
		levelMaker.normal.fixedWidth = 25;
		levelMaker.normal.normal.background = levelMaker.defaultTexture;
		levelMaker.normal.active.background = levelMaker.defaultTexture;

		levelMaker.start = new GUIStyle (GUI.skin.button);
		levelMaker.start.fixedWidth = 25;
		levelMaker.start.normal.background = levelMaker.startTileTexture;
		levelMaker.start.active.background = levelMaker.startTileTexture;

		levelMaker.blue = new GUIStyle (GUI.skin.button);
		levelMaker.blue.fixedWidth = 25;
		levelMaker.blue.normal.background = levelMaker.blueTexture;
		levelMaker.blue.active.background = levelMaker.blueTexture;

		levelMaker.red = new GUIStyle (GUI.skin.button);
		levelMaker.red.fixedWidth = 25;
		levelMaker.red.normal.background = levelMaker.redTexture;
		levelMaker.red.active.background = levelMaker.redTexture;

		levelMaker.yellow = new GUIStyle (GUI.skin.button);
		levelMaker.yellow.fixedWidth = 25;
		levelMaker.yellow.normal.background = levelMaker.yellowTexture;
		levelMaker.yellow.active.background = levelMaker.yellowTexture;

		levelMaker.blueYellow = new GUIStyle (GUI.skin.button);
		levelMaker.blueYellow.fixedWidth = 25;
		levelMaker.blueYellow.normal.background = levelMaker.blueYellowTexture;
		levelMaker.blueYellow.active.background = levelMaker.blueYellowTexture;

		levelMaker.redBlue = new GUIStyle (GUI.skin.button);
		levelMaker.redBlue.fixedWidth = 25;
		levelMaker.redBlue.normal.background = levelMaker.redBlueTexture;
		levelMaker.redBlue.active.background = levelMaker.redBlueTexture;

		levelMaker.yellowRed = new GUIStyle (GUI.skin.button);
		levelMaker.yellowRed.fixedWidth = 25;
		levelMaker.yellowRed.normal.background = levelMaker.yellowRedTexture;
		levelMaker.yellowRed.active.background = levelMaker.yellowRedTexture;

		levelMaker.portals = new GUIStyle (GUI.skin.button);
		levelMaker.portals.fixedWidth = 25;
		levelMaker.portals.normal.background = levelMaker.portalTexture;
		levelMaker.portals.active.background = levelMaker.portalTexture;





		//Begin drawing the inspector
		EditorGUILayout.Space ();

		//Header
		EditorGUILayout.LabelField ("Grid", EditorStyles.boldLabel, GUILayout.Width(50));

		//A grid with selectable tiles
		for (int y = 0; y <levelMaker.gridHeight; y++)
		{
			GUILayout.BeginHorizontal ();

			for (int x = 0; x < levelMaker.gridWidth; x++)
			{
				GUIStyle layout = new GUIStyle (GUI.skin.button);
				layout = levelMaker.guiStyles[levelMaker.positions[x].positionsArray[y]];

				if (GUILayout.Button ("", layout))
				{
					if (levelMaker.positions [x].positionsArray[y] < levelMaker.guiStyles.Length - 1)
					{
						levelMaker.positions [x].positionsArray[y]++;
					}
					else
					{
						levelMaker.positions [x].positionsArray[y] = 0;
					}
				}
			}

			GUILayout.EndHorizontal ();
		}
			
		EditorGUILayout.Space ();

		//A reset button which sets all tiles textures back to the default texture
		if (GUILayout.Button ("Clear", GUILayout.Width(50)))
		{
			levelMaker.Reset ();
		}

		EditorGUILayout.Space ();





		EditorGUILayout.LabelField ("Name of the level:", EditorStyles.boldLabel, GUILayout.Width(150));

		levelMaker.levelName = EditorGUILayout.TextField (levelMaker.levelName, GUILayout.Width (150));

		EditorGUILayout.Space ();





		//Header
		EditorGUILayout.LabelField ("Test the level", EditorStyles.boldLabel, GUILayout.Width(150));

		GUILayout.BeginHorizontal ();

		//A save button that saves the level in a prefab
		if (GUILayout.Button ("Test", GUILayout.Width(50)))
		{
			levelMaker.Test ();
			//levelMaker.BuildLevel ();
		}

		if (GUILayout.Button ("Clear", GUILayout.Width(50)))
		{
			levelMaker.DestroyLevel ();
		}

		GUILayout.EndHorizontal ();

		EditorGUILayout.Space ();





		EditorGUILayout.LabelField ("Location of the prefab:", EditorStyles.boldLabel, GUILayout.Width(160));

		GUILayout.BeginHorizontal ();

		levelMaker.saveLocation = EditorGUILayout.TextField (levelMaker.saveLocation, GUILayout.Width (150));

		if (GUILayout.Button ("Save", GUILayout.Width(50)))
		{
			if (levelMaker.newLevel == null)
				levelMaker.BuildLevel ();

			if (Directory.Exists (levelMaker.saveLocation))
			{
				if (levelMaker.levelName != "")
				{
					string localPath = levelMaker.saveLocation + levelMaker.levelName + ".prefab";
					Object prefab = PrefabUtility.CreateEmptyPrefab (localPath);
					PrefabUtility.ReplacePrefab (levelMaker.newLevel, prefab, ReplacePrefabOptions.ConnectToPrefab);
					Debug.Log ("The prefab " + levelMaker.levelName + " has been saved at: " + levelMaker.saveLocation);
				}
				else
					Debug.LogError ("Please enter a name for the prefab");
			}
			else
				Debug.LogError ("The save location doesn't exist.");

			levelMaker.DestroyLevel ();
		}

		GUILayout.EndHorizontal ();

		if (GUI.changed) 
		{
			EditorUtility.SetDirty (target);
		}
	}
}
