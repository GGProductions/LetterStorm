using UnityEngine;
using System.Collections;

public class FadeBlack2Clear : MonoBehaviour {
		
		
		public float fadeSpeed = 0.1f;          // Speed that the screen fades to and from black.
		
		
		
		public float amount= 0.5f;
		
		void Awake ()
		{
			// Set the texture so that it is the the size of the screen and covers it.
			guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
			
		}
		
		void Start(){
			//guiTexture.color = Color.black;
		guiTexture.color = Color.clear;
        transform.position = new Vector3(0, 0, 0);
		}
		


	public bool godude=false;
		void Update ()
		{
			// If the scene is starting...
			//	if(sceneStarting)
			// ... call the StartScene function.
			//	StartScene();
		if(godude)
		EndScene ();
			//StartScene ();
	//	EndScene ();
		}
		
	public void turnOnSwitch (){
		godude = true;
	}
		
		void FadeToClear ()
		{
			guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
		
		
		void FadeToBlack ()
		{
			guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
		}
		
		
		
		
		public void EndScene ()
		{
			// Make sure the texture is enabled.
			guiTexture.enabled = true;
			
			// Start fading towards black.
			FadeToBlack();
			
			// If the screen is almost black...
			if(guiTexture.color.a >= 0.8f)
				// ... reload the level.
				Application.LoadLevel("Win");
		}
		
		
		
		
		
		
		
		
		/// <summary>
		/// //////////////////////////////////////////////////////////////////////////////////
		/// </summary>
		
		void StartScene ()
		{
			// Fade the texture to clear.
			FadeToClear();
			
			// If the texture is almost clear...
			if(guiTexture.color.a <= 0.05f)
			{
				// ... set the colour to clear and disable the GUITexture.
				guiTexture.color = Color.clear;
				guiTexture.enabled = false;
				
				// The scene is no longer starting.
				//sceneStarting = false;
			}
		}
		
		
	}