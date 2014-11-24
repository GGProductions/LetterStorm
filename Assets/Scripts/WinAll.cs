 using UnityEngine;
using System.Collections;
using System.Linq;

//JR this is the Win scene
public class WinAll : MonoBehaviour
{
    #region Private Variables -------------------------------------------------
    private float scaleFactor = 1.0f;

    // Variables used to store the original and scaled versions of the GUI styles
    private GUIStyle _startGameButtonStyle = null;
    private GUIStyle _startGameButtonStyleOrig = null;

    /// <summary>
    /// The styles typically used for start game button, but reappropriated for main menu button here
    /// </summary>
    private GUIStyle startGameButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_startGameButtonStyle == null)
            {
                _startGameButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("startGameButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _startGameButtonStyle = new GUIStyle(_startGameButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _startGameButtonStyle.fontSize = (int)(_startGameButtonStyleOrig.fontSize * scaleFactor);
            _startGameButtonStyle.margin = new RectOffset((int)(_startGameButtonStyleOrig.margin.left * scaleFactor),
                                                            (int)(_startGameButtonStyleOrig.margin.right * scaleFactor),
                                                            (int)(_startGameButtonStyleOrig.margin.top * scaleFactor),
                                                            (int)(_startGameButtonStyleOrig.margin.bottom * scaleFactor));
            _startGameButtonStyle.fixedHeight = (int)(_startGameButtonStyleOrig.fixedHeight * scaleFactor);

            return _startGameButtonStyle;
        }
    }
    #endregion Private Variables ----------------------------------------------

    #region Public Variables --------------------------------------------------
    //JR Texture for the background in the scene
    public Texture backgroundTexture;
	
	//JR Texture for the background in the scene
    public Texture photoTexture;

	// Title Texture
	public Texture titleImg;

    /// <summary>
    /// The GUI skin containing all the styles used for the GUI elements
    /// </summary>
    public GUISkin smartMenuSkin;
    #endregion Public Variables -----------------------------------------------

    #region Unity Events ------------------------------------------------------
    
    void OnGUI()
    {
        CreateGUI();
    }
    #endregion Unity Events ---------------------------------------------------

    /// <summary>
    /// Create all controls displayed in this scene 
    /// </summary>
    private void CreateGUI()
    {
        // Calculate the factor by which the GUI should be sized/resized based on the screen size
        CalculateGUIScaleFactor();

        //JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //JR Calls up title texture which is a .png file inside textures folder
        //GUI.DrawTexture(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 150, 240, 60), titleTexture);

        // Create the page title
        float titleImgWidth = titleImg.width * scaleFactor;
        float titleImgHeight = titleImg.height * scaleFactor;
        float titleDistanceFromTop = 30.0f * scaleFactor;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (titleImgWidth / 2), titleDistanceFromTop, titleImgWidth, titleImgHeight), titleImg);

        //JR Calls up enemy defeated texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 75, 300, 250), photoTexture);

        // Create the button used to return to the main menu
        CreateMainMenuBtn();
    }

    #region Other GUI Object Methods ------------------------------------------
    /// <summary>
    /// Create the button used to return to the main menu, and 
    /// load the main menu if it has been clicked.
    /// </summary>
    private void CreateMainMenuBtn()
    {
        // Create the button used to return to the main menu.  If it was clicked...
        if (GUI.Button(new Rect((Screen.width / 2) - (200 * scaleFactor), Screen.height - (100 * scaleFactor), (400 * scaleFactor), (60 * scaleFactor)), "Main Menu", startGameButtonStyle))
        {
            // Load the main menu
            Application.LoadLevel("MainMenu");
        }
    }
    #endregion Other GUI Object Methods ---------------------------------------

    #region Helper Methods ----------------------------------------------------
    /// <summary>
    /// Calculate by what factor the GUI should be resized
    /// </summary>
    private void CalculateGUIScaleFactor()
    {
        const float idealWidth = 1024f;
        const float idealHeight = 768f;

        // Calculate the ratio of the current screen width and height to the ideal
        float curToIdealWidthRatio = Screen.width / idealWidth;
        float curToIdealHeightRatio = Screen.height / idealHeight;

        // Use the smallest ratio as the scale factor
        scaleFactor = (curToIdealHeightRatio < curToIdealWidthRatio) ? curToIdealHeightRatio : curToIdealWidthRatio;

        // If the scale factor is greater than 1, use 1 instead
        if (scaleFactor > 1.0f)
            scaleFactor = 1.0f;
    }
    #endregion Helper Methods -------------------------------------------------
}
