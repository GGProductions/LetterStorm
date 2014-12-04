 using UnityEngine;
using System.Collections;

//JR L 2 Play Scene (Learn) also houses scrollview of License
public class Learn : MonoBehaviour
{
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Title Texture
	public Texture l2PlayTexture;
	
	//JR Title Texture
	public Texture licenseTexture;
	
	//JR Scrollview
	private Vector2 scrollViewVector = Vector2.zero;
	
	//JR L2Play instructions text string
    private string LearnText = "<b>[Up], [Down], [Left] & [Right] arrows</b> = <b>Move</b> Albert \n<b>[Spacebar]</b> = Shoot projectiles enemies and bosses\n\n<b>Phase 1</b>: Fight enemies and collect letters that you can use for a later battle with the boss \n<b>Phase 2</b>: After collecting enough letters, a <b>BOSS</b> will appear. Defeat its cannons, and then load any letter(s) you have collected (<b>A - Z</b>) using the keyboard, and then shoot with <b>[Spacebar]</b> to spell out the word!";
    
	//JR License instructions text string
	private string LicenseText = "The MIT License (MIT) Copyright (c) 2014 GG Productions Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
  
    void OnGUI()
    {
		//JR L2Play field begins
		//JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 120 , Screen.height / 2 - 200, 240, 60), l2PlayTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 120 , Screen.height / 2 + 20, 240, 50), licenseTexture);
		
		//JR Make text font black
		GUI.contentColor = Color.black;
		
		//JR creates text box and displays LearnText string
        GUI.Label(new Rect(Screen.width / 2 - 245 , Screen.height / 2 - 130 , 490, 145), LearnText);
		
		//JR Make text font black
		GUI.contentColor = Color.white; 
		
		//JR License field begins
        //JR ScrollView for License
		scrollViewVector = GUI.BeginScrollView (new Rect (Screen.width / 2 - 200 , Screen.height / 2 + 80, 400, 75), scrollViewVector, new Rect (Screen.width / 2 - 205 , Screen.height / 2 + 80, 425, 320));
    
        //JR Text area that holds LicenseText string
        LicenseText = GUI.TextArea (new Rect (Screen.width / 2 - 200 , Screen.height / 2 + 90, 400, 300), LicenseText);
    
        //JR  End the ScrollView
        GUI.EndScrollView();
		
		//JR Creates and displays Main Menu button, if pressed takes you to Main Menu
        if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 165 , 80, 20), "Main Menu"))
        {
			Application.LoadLevel("MainMenu");
        }
    }
}