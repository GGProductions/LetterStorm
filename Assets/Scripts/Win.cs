 using UnityEngine;
using System.Collections;

//JR this is the Win scene
public class Win : MonoBehaviour
{

	//JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Texture for the background in the scene
    public Texture photoTexture;

	//JR Title Texture
	public Texture titleTexture;
	
    void OnGUI()
    {
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 120 , Screen.height / 2 - 150, 240, 60), titleTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 300 , Screen.height / 2 - 75, 300, 250), photoTexture);

		//JR if pressed load Next Level
        if(GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height / 2 + 25, 80, 20), "Level 1"))
        {
			//JR Gives players fresh lives and 0s out the score and misses
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
            Context.ClearStatsNextLevel();
            Application.LoadLevel("EnemyTesting");
        }
        
		//JR if pressed load Next Level
        if(GUI.Button(new Rect(Screen.width / 2 + 10 , Screen.height / 2 + 50, 80, 20), "Next Level"))
        {
			//JR Gives players fresh lives and 0s out the score and misses
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
            Context.ClearStatsNextLevel();
            Application.LoadLevel("EnemyTesting");
        }
        
		//JR if pressed load Main Menu
        if(GUI.Button(new Rect(Screen.width / 2 + 10 , Screen.height / 2 + 75, 80, 20), "Main Menu"))
        {
            Context.ClearStatsNextLevel();
			Application.LoadLevel("MainMenu");
        }
    }
}
