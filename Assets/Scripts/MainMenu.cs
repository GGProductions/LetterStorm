using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	//JR menu fields at the start of the display
	
	private bool _isFirstMenu = true;
	private bool _isHowPlayMenu = false;
	private bool _isOptionsMenu = false;
	private bool _isBlankMenu = false;
    
    public Texture backgroundTexture;
    
    private string instructionText = "Blahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\n";
    
    private string optionText = "Blahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\nBlahblahblahblahblahblahblahblahblahblahblah.\n";
	
	//JR game title call up public string gameTitle = "LetterStorm";
	
	void start()
	{
		
	}
	
	void update()
	{
		
	}
	
	void OnGUI()
	{
		//JR game title call up GUI.Label(new Rect(30, 75, 300, 25), gameTitle);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		FirstMenu();
		HowPlayMenu();
		OptionsMenu();
		BlankMenu();
		
		if(_isHowPlayMenu == true || _isOptionsMenu == true || _isBlankMenu ==true)
		{
			if(GUI.Button(new Rect(10, Screen.height - 35, 75, 20), "Main Menu"))
			{
				_isHowPlayMenu = false;
				_isOptionsMenu = false;
				_isBlankMenu = false;
				
				_isFirstMenu = true;
			}
		}
	}
	
	void FirstMenu()
	{
		if(_isFirstMenu)
		{
			if(GUI.Button(new Rect(240, Screen.height / 2 - 65, 75, 20), "Play"))
			{
				Application.LoadLevel("EnemyTesting");
			}
			
			if(GUI.Button(new Rect(240, Screen.height / 2 - 30, 75, 20), "L 2 Play"))
			{
				_isFirstMenu = false;
				_isHowPlayMenu = true;
                GUI.Label(new Rect(10, 10, 200, 200), instructionText);
			}
			
			if(GUI.Button(new Rect(240, Screen.height / 2 + 5 , 75, 20), "Options"))
			{
				_isFirstMenu = false;
				_isOptionsMenu = true;
                GUI.Label(new Rect(10, 10, 200, 200), optionText);
			}
			
			if(GUI.Button(new Rect(240, Screen.height / 2 + 40, 75, 20), "Blank"))
			{
				_isFirstMenu = false;
				_isBlankMenu = true;
			}
			
			if(GUI.Button(new Rect(240, Screen.height / 2 + 75, 75, 20), "Quit"))
			{
				Application.Quit();
			}
		}
	}
	
	void HowPlayMenu()
	{
		if(_isHowPlayMenu)
		{
			
		}
	}
	
	void OptionsMenu()
	{
		if(_isOptionsMenu)
		{
			
		}
	}
	
	void BlankMenu()
	{
		if(_isBlankMenu)
		{
			
		}
	}
}