 using UnityEngine;
using System.Collections;
using System.Linq;
using GGProductions.LetterStorm.Data;
using System.Collections.Generic;
using System;
using GGProductions.LetterStorm.Data.Collections;

//JR this is the Win scene
public class WinAll : MonoBehaviour
{
    #region Private Variables -------------------------------------------------
    private string lessonName = null;
    private List<string> lessonWordTexts = null;
    private float scaleFactor = 1.0f;

    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// for the current lesson's words within the CurrentLessons Area
    /// </summary>
    private Vector2 lessonWordsScrollPosition;

    // Variables used to store the original and scaled versions of the GUI styles
    private GUIStyle _headerStyle = null;
    private GUIStyle _headerStyleOrig = null;
    private GUIStyle _subHeaderStyle = null;
    private GUIStyle _subHeaderStyleOrig = null;
    private GUIStyle _generalButtonStyle = null;
    private GUIStyle _generalButtonStyleOrig = null;
    private GUIStyle _startGameButtonStyle = null;
    private GUIStyle _startGameButtonStyleOrig = null;

    /// <summary>
    /// The styles used for header labels
    /// </summary>
    private GUIStyle headerStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_headerStyle == null)
            {
                _headerStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("headerStyle", System.StringComparison.OrdinalIgnoreCase));
                _headerStyle = new GUIStyle(_headerStyleOrig);
                _headerStyle.alignment = TextAnchor.UpperCenter;
            }
            // Scale the style according to the screen size
            _headerStyle.fontSize = (int)(_headerStyleOrig.fontSize * scaleFactor);
            _headerStyle.fixedHeight = (int)(_headerStyleOrig.fixedHeight * scaleFactor);

            return _headerStyle;
        }
    }

    /// <summary>
    /// The styles used for subheader labels
    /// </summary>
    private GUIStyle subHeaderStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_subHeaderStyle == null)
            {
                _subHeaderStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("subHeaderStyle", System.StringComparison.OrdinalIgnoreCase));
                _subHeaderStyle = new GUIStyle(_subHeaderStyleOrig);
            }
            // Scale the style according to the screen size
            _subHeaderStyle.fontSize = (int)(_subHeaderStyleOrig.fontSize * scaleFactor);

            return _subHeaderStyle;
        }
    }

    /// <summary>
    /// The style used for most buttons
    /// </summary>
    private GUIStyle generalButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_generalButtonStyle == null)
            {
                _generalButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("generalButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _generalButtonStyle = new GUIStyle(_generalButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _generalButtonStyle.fontSize = (int)(_generalButtonStyleOrig.fontSize * scaleFactor);
            _generalButtonStyle.padding = new RectOffset((int)(_generalButtonStyleOrig.padding.left * scaleFactor),
                                                         (int)(_generalButtonStyleOrig.padding.right * scaleFactor),
                                                         (int)(_generalButtonStyleOrig.padding.top * scaleFactor),
                                                         (int)(_generalButtonStyleOrig.padding.bottom * scaleFactor));
            _generalButtonStyle.fixedHeight = (int)(_generalButtonStyleOrig.fixedHeight * scaleFactor);

            return _generalButtonStyle;
        }
    }

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

    /// <summary>
    /// The backgound image to use for the summary area
    /// </summary>
    public Texture areaBackgroundImg;

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

        // Calculate the location where the top left of the GUI should 
        // start if it is to be centered on screen
        int guiAreaLeft = (Screen.width / 2) - ((int)(700 * scaleFactor) / 2);
        int guiAreaTop = (Screen.height / 2) - (int)((600 * scaleFactor) / 2) + (int)(70 * scaleFactor);

        // If any of the calculations fall below zero (because the screen is too small),
        // default to zero
        if (guiAreaLeft < 0)
            guiAreaLeft = 0;
        if (guiAreaTop < 0)
            guiAreaTop = 0;

        //JR Calls up background texture which is a .png file inside textures folder
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        // Create the page title
        float titleImgWidth = titleImg.width * scaleFactor;
        float titleImgHeight = titleImg.height * scaleFactor;
        float titleDistanceFromTop = 30.0f * scaleFactor;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (titleImgWidth / 2), titleDistanceFromTop, titleImgWidth, titleImgHeight), titleImg);

        // Create the background page for the summary area
        //GUI.DrawTexture(new Rect(guiAreaLeft - (85 * scaleFactor), guiAreaTop - (10 * scaleFactor), (540 * scaleFactor), (560 * scaleFactor)), areaBackgroundImg);
        GUI.DrawTexture(new Rect(guiAreaLeft, guiAreaTop - (10 * scaleFactor), (700 * scaleFactor), (500 * scaleFactor)), areaBackgroundImg);

        GUILayout.BeginArea(new Rect(guiAreaLeft, guiAreaTop, (700 * scaleFactor), (530 * scaleFactor)));
        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the congradulations header and controls used to display the score and difficulty
        CreateCongratsHeader();
        CreateScoreAndDifficultyArea();

        //JR Calls up enemy defeated texture which is a .png file inside textures folder
        float bossImgWidth = photoTexture.width * scaleFactor;
        float bossImgHeight = photoTexture.height * scaleFactor;
        GUI.DrawTexture(new Rect((120 * scaleFactor), (290 * scaleFactor), bossImgWidth, bossImgHeight), photoTexture);

        // Create the list of word the user has spelled
        CreateLessonWordsArea();
        
        // End the cotroller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();

        // Create the button used to return to the main menu
        CreateMainMenuBtn();
    }

    #region Create Area Methods -----------------------------------------------
    /// <summary>
    /// Create the controls used to build the Congradulations header
    /// </summary>
    private void CreateCongratsHeader()
    {
        // If a List of the lesson words and it's title has not yet been built, 
        // populate the word cache
        BuildLessonCache();

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(0, 0, (700 * scaleFactor), (100 * scaleFactor)));

        GUILayout.Label("You have defeated" + Environment.NewLine + lessonName + "!", headerStyle);

        GUILayout.EndArea();
    }

    /// <summary>
    /// Create the controls used to display the Score and Difficulty
    /// </summary>
    private void CreateScoreAndDifficultyArea()
    {
        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect((120 * scaleFactor), (120 * scaleFactor), (200 * scaleFactor), (150 * scaleFactor)));

        GUILayout.Label("Total Score:", subHeaderStyle);
        GUILayout.Label(Context.CurrentScore.Score.ToString(), subHeaderStyle);

        GUILayout.Space(20f * scaleFactor);

        GUILayout.Label("Difficulty:", subHeaderStyle);
        GUILayout.Label(Context.EnemyDifficulty.Name, subHeaderStyle);

        GUILayout.EndArea();
    }

    /// <summary>
    /// Create all controls used to display the current lesson's words
    /// </summary>
    private void CreateLessonWordsArea()
    {
        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect((400 * scaleFactor), (120 * scaleFactor), (200 * scaleFactor), (330 * scaleFactor)));
        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the Words subheader label
        GUILayout.Label("Words: ", subHeaderStyle);


        // Create a scrollable area incase the number of words exceeds the space available
        lessonWordsScrollPosition = GUILayout.BeginScrollView(lessonWordsScrollPosition);

        // Build a vertical, one-column grid of buttons corresponding to the 
        // lesson words, and note which one the player selected
        //GUILayout.SelectionGrid(0, lessonWordTexts.ToArray(), 1, generalButtonStyle);

        for (int i = 0; i < lessonWordTexts.Count; i++)
        {
            GUILayout.Label(lessonWordTexts[i], generalButtonStyle);
        }

        GUILayout.EndScrollView();
        
        // End the cotroller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion Create Area Methods --------------------------------------------

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
    /// Cache the text of the words for the current lesson. 
    /// This is done to minimize execution time between frames, 
    /// as the GUI is built from cache.
    /// </summary>
    private void BuildLessonCache()
    {
        // If a List of the lesson words has not yet been built...
        if (lessonWordTexts == null)
        {
            Lesson curLesson = null;

            if (Context.CurrentLessonId != null && Context.CurrentLessonId != Guid.Empty)
                curLesson = Context.Curriculum.Lessons.GetLessonById(Context.CurrentLessonId);
            // Else, if no lesson was selected, default to the first available lesson.  This logic is primarily 
            // for testing this page directly (without having to play through the game first).
            else if (Context.Curriculum.Lessons.Count > 0)
                curLesson = Context.Curriculum.Lessons[0];

            // Create a query to retrieve the text of all words in the current lesson
            var queryWordTexts = from Word w in curLesson.Words
                                 select w.Text;
            // Convert the query results to a List
            lessonWordTexts = queryWordTexts.ToList();
            lessonName = curLesson.Name;
        }
    }

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
