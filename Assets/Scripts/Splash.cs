using UnityEngine;
using System.Collections;

//JR Splash Scene
public class Splash : MonoBehaviour {
  
  	//JR splash scene displays for 5 seconds
    public float delayTime = 5;
    
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
    
    void OnGUI()
    {
		//JR draws background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
    }
    
	//JR handles the countdown for displaying the splash scene and then cuts away to Main Menu
    IEnumerator Start()
    {
        yield return new WaitForSeconds( delayTime );
        
        Application.LoadLevel(1);
        
    }
}
