 using UnityEngine;
using System.Collections;

//JR this is the Win scene
public class Win : MonoBehaviour
{

	//JR Texture for the background in the scene
    public Texture backgroundTexture;

    void OnGUI()
    {
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

		//JR if pressed load Next Level
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 25, 80, 20), "Level 1"))
        {
			//JR Gives players fresh lives and 0s out the score and misses
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
            Application.LoadLevel("EnemyTesting");
        }
        
		//JR if pressed load Next Level
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 50, 80, 20), "Next Level"))
        {
			//JR Gives players fresh lives and 0s out the score and misses
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
            Application.LoadLevel("EnemyTesting");
        }
        
		//JR if pressed load Main Menu
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 75, 80, 20), "Main Menu"))
        {
			Application.LoadLevel("MainMenu");
        }
    }
}
