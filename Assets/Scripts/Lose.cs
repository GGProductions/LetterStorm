using UnityEngine;
using System.Collections;

//JR this is the Lose scene
public class Lose : MonoBehaviour
{
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Title Texture
	public Texture titleTexture;

    void OnGUI()
    {
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 120 , Screen.height / 2 - 100, 240, 60), titleTexture);
		
		//JR if pressed continue playing last level with fresh lives
        if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 20), "Insert Coin, to continue playing"))
        {
            //Nabil: added this line to start the next level with 15 lives
            //JT: changed 15 to half of maximum health
            Context.PlayerHealth.CurHealth = Context.PlayerHealth.MaxHealth / 2;


		//JR Loads level and gives players fresh lives, however score and misses should stay the same
            Context.ClearStatsNextLevel();
			Application.LoadLevel("EnemyTesting");
            //Player.Lives = 3; //JR Old Code from first Iteration
        }
        
		//JR if pressed continue playing last level with fresh lives, score and misses
        if(GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 50, 90, 20), "Restart Level"))
        {

			//JR Loads level and gives players fresh lives and 0s out the score and misses
            Context.ClearStatsNextLevel();
            Application.LoadLevel("EnemyTesting");
            //Player.Score = 0; //JR Old Code from first Iteration
            //Player.Lives = 3; //JR Old Code from first Iteration
            //Player.Missed = 0; //JR Old Code from first Iteration
        }
        
		//JR if pressed load Main Menu
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 75, 80, 20), "Main Menu"))
        {
            Context.ClearStatsNextLevel();
            Application.LoadLevel("MainMenu");

        }
    }
}
