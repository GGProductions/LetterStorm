 using UnityEngine;
using System.Collections;

//JR Story Scene (Blank)
public class Blank : MonoBehaviour
{
	//JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Title Texture
	public Texture storyTexture;
	
	//JR Scrollview
    private Vector2 scrollViewVector = Vector2.zero;
	
	//JR string that holds text to be inserted
	private string StoryText = " It was a dark and stormy night, and two siblings, Albert and Annie, were caught out in it.  As the wind howled and the trees creaked, they plodded through the nearly blinding rain toward a hope of refuge in the distance.  The dark, rectangular shape on the horizon would have been foreboding under any other circumstance, but to them it symbolized shelter and warmth.  With each passing minute, hopes were strengthened as the structure slowly grew larger and clearer. Then, their moment finally came. After journeying for what seemed to be hours, the two found themselves before the large, unsecured iron gates of what appeared to be an old college building of sorts. It took only one look at the rusted iron bars and crumbling brick walls to see that time was no friend to this work of man.  Yet, they were not deterred.  Perhaps they could take temporary shelter inside until the storm passed, or at least such was the thought. Slowly they worked their way through the gate and to the facility’s oversized engraved doorway.  Whatever fantasies of danger it would have stirred up under normal circumstances fell prey to the desperation of their plight, and so, without much thought, they eagerly pressed the two iron-clad doors open.  Their entrance did not go unannounced, though, as the age-old hinges let out a moan strong enough to shake one’s bones.  And that is when the trouble started. Looking into the dark interior, one could make out a large foyer walled in by deep hallways and a large, central staircase.  The faint but growing noise similar to that of frantic footsteps emanated from the core of the structure.  Then they saw him: A scholarly gentleman with his hair amuck and his clothes wrinkled and stained came running madly down the staircase right toward them.  “Help!  Help!”  he yelled, gasping for breath between his words.  “Something has gone terribly wrong.  The sound of your entry startled me into a grave accident.  My lab is now run amuck with creatures not from this world.” At the sound of such news, the two siblings began to feel fear well up within them for the first time that night.  “It’s alright to be afraid, but do not let fear stop you from doing what is right.  I have here the book that the creatures came from and the pencil used to design them.  If you use them correctly, you should be able to vanquish the creatures and I will be forever in your debt.” Hesitantly accepting their fate, Albert and Annie took up the tools and began their adventure into the LetterStorm.";
    
    void OnGUI()
    {
		//JR draws background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//JR Calls up title texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 120 , Screen.height / 2 - 200, 240, 75), storyTexture);
		
		//JR Make text font black
		//GUI.contentColor = Color.black;
		
		//JR Story field begins
        //JR ScrollView for Story
        scrollViewVector = GUI.BeginScrollView (new Rect (Screen.width / 2 - 200 , Screen.height / 2 - 120, 425, 240), scrollViewVector, new Rect (Screen.width / 2 - 205 , Screen.height / 2 - 120, 425, 600));
    
        //JR Text area that holds StoryText string
        StoryText = GUI.TextArea (new Rect (Screen.width / 2 - 200 , Screen.height / 2 - 120, 400, 600), StoryText);
    
    	//JR  End the ScrollView
        GUI.EndScrollView();
		
		//JR Button for return to Main Menu
 	 	if(GUI.Button(new Rect(Screen.width / 2 - 40 , Screen.height / 2 + 125 , 80, 20), "Main Menu"))
        {
			Application.LoadLevel("MainMenu");
        }
    }
}