using UnityEngine;
using System.Collections;

//JD Main Menu Scene
public class MainMenu : MonoBehaviour
{
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
    

	
	void start()
	{
		
	}
	
	void update()
	{
		
	}
	
	void OnGUI()
	{
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//JR Displays "Play"" button on the Main Menu, this button Loads level1 scene
		//JR if Button is pressed load scene
		if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 - 30, 80, 20), "Play"))
		{
			Application.LoadLevel(2);
			//JR loads level with 3 lives, 0 score and misses
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
		}
			
		//JR Creates "L 2 Play" button on the Main Menu, this button Loads Learn scene
		//JR if Button is pressed load scene
		if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 5, 80, 20), "L 2 Play"))
		{
			Application.LoadLevel(5);
		}
			
		//JR Creates "Options" button on the Main Menu, this button Loads Options scene
		//JR if Button is pressed load scene
		//JR swapped positions with "License" button on the Main Menu
		if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 75 , 80, 20), "Options"))
		{
			Application.LoadLevel(6);
		}
			
		//JR Creates "Story" button on the Main Menu, this button Loads Blank scene
		//JR if Button is pressed load scene
		//JR swapped positions with "Options" button on the Main Menu
		if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 40, 80, 20), "Story"))
		{
			Application.LoadLevel(7);
		}
			
		//JR Creates "Quit" button on the Main Menu
		//JR if Button is pressed quit game
		if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 105, 80, 20), "Quit"))
		{
			Application.Quit();
		}	
	}	
}