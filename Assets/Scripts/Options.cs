 using UnityEngine;
using System.Collections;

//JR Options Scene
public class Options : MonoBehaviour
{
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Text field to share with user
	private string OptionsText = " BLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblahBLAHblah";

    void OnGUI()
    {
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        
		//JR Make text font black
		GUI.contentColor = Color.black;
		
		//JR creates text box and displays OptionsText string
		GUI.Label(new Rect(Screen.width / 2 - 125 , Screen.height / 2 - 100 , 250, 200), OptionsText);
		
		//JR Creates and displays Main Menu button, if pressed takes you to Main Menu
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 125 , 80, 20), "Main Menu"))
        {
			Application.LoadLevel(1);
        }
    }
}