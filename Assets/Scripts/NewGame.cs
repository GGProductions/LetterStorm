using UnityEngine;
using System.Collections;
using System.Linq;
using GGProductions.LetterStorm.Data;
using System.Collections.Generic;
using GGProductions.LetterStorm.Utilities;
using GGProductions.LetterStorm.Data.Collections;
using GGProductions.LetterStorm.Configuration.Collections;
using GGProductions.LetterStorm.Configuration;

public class NewGame : MonoBehaviour
{

    #region Private Variables -------------------------------------------------
    private PlayerData playerData = null;
    private DifficultyLevels difficultyLevels = new DifficultyLevels();
    private List<string> difficultyLevelNames = null;
    private List<string> difficultyLevelDescriptions = null;
    private List<string> lessonNames = null;
    private int selectedDifficultyLevelBtnIdx = 0;
    private int selectedLessonBtnIdx = 0;
    private float scaleFactor = 1.0f;

    // Variables used to store the original and scaled versions of the GUI styles
    private GUIStyle _headerStyle = null;
    private GUIStyle _headerStyleOrig = null;
    private GUIStyle _generalButtonStyle = null;
    private GUIStyle _generalButtonStyleOrig = null;
    private GUIStyle _mainMenuButtonStyle = null;
    private GUIStyle _mainMenuButtonStyleOrig = null;
    private GUIStyle _difficultyEasyButtonStyle = null;
    private GUIStyle _difficultyEasyButtonStyleOrig = null;
    private GUIStyle _difficultyNormalButtonStyle = null;
    private GUIStyle _difficultyNormalButtonStyleOrig = null;
    private GUIStyle _difficultyHardButtonStyle = null;
    private GUIStyle _difficultyHardButtonStyleOrig = null;
    private GUIStyle _tooltipLabelStyleStyle = null;
    private GUIStyle _tooltipLabelStyleStyleOrig = null;
    private GUIStyle _startGameButtonStyle = null;
    private GUIStyle _startGameButtonStyleOrig = null;

    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// within the Diifficulty Levels Area
    /// </summary>
    private Vector2 difficultyLevelsScrollPosition;

    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// within the AllLessons Area
    /// </summary>
    private Vector2 allLessonsScrollPosition;

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
            }
            // Scale the style according to the screen size
            _headerStyle.fontSize = (int)(_headerStyleOrig.fontSize * scaleFactor);
            _headerStyle.fixedHeight = (int)(_headerStyleOrig.fixedHeight * scaleFactor);

            return _headerStyle;
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
    /// The styles used for return to main menu button
    /// </summary>
    private GUIStyle mainMenuButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_mainMenuButtonStyle == null)
            {
                _mainMenuButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("mainMenuButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _mainMenuButtonStyle = new GUIStyle(_mainMenuButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _mainMenuButtonStyle.fontSize = (int)(_mainMenuButtonStyleOrig.fontSize * scaleFactor);
            _mainMenuButtonStyle.padding = new RectOffset((int)(_mainMenuButtonStyleOrig.padding.left * scaleFactor),
                                             (int)(_mainMenuButtonStyleOrig.padding.right * scaleFactor),
                                             (int)(_mainMenuButtonStyleOrig.padding.top * scaleFactor),
                                             (int)(_mainMenuButtonStyleOrig.padding.bottom * scaleFactor));

            return _mainMenuButtonStyle;
        }
    }

    /// <summary>
    /// The styles used for the easy difficulty button
    /// </summary>
    private GUIStyle difficultyEasyButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_difficultyEasyButtonStyle == null)
            {
                _difficultyEasyButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyEasyButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _difficultyEasyButtonStyle = new GUIStyle(_difficultyEasyButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _difficultyEasyButtonStyle.fontSize = (int)(_difficultyEasyButtonStyleOrig.fontSize * scaleFactor);
            _difficultyEasyButtonStyle.margin = new RectOffset((int)(_difficultyEasyButtonStyleOrig.margin.left * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.margin.right * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.margin.top * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.margin.bottom * scaleFactor));
            _difficultyEasyButtonStyle.padding = new RectOffset((int)(_difficultyEasyButtonStyleOrig.padding.left * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.padding.right * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.padding.top * scaleFactor),
                                                                (int)(_difficultyEasyButtonStyleOrig.padding.bottom * scaleFactor));
            _difficultyEasyButtonStyle.fixedHeight = (int)(_difficultyEasyButtonStyleOrig.fixedHeight * scaleFactor);
            _difficultyEasyButtonStyle.fixedWidth = (int)(_difficultyEasyButtonStyleOrig.fixedWidth * scaleFactor);

            return _difficultyEasyButtonStyle;
        }
    }

    /// <summary>
    /// The styles used for the normal difficulty button
    /// </summary>
    private GUIStyle difficultyNormalButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_difficultyNormalButtonStyle == null)
            {
                _difficultyNormalButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyNormalButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _difficultyNormalButtonStyle = new GUIStyle(_difficultyNormalButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _difficultyNormalButtonStyle.fontSize = (int)(_difficultyNormalButtonStyleOrig.fontSize * scaleFactor);
            _difficultyNormalButtonStyle.margin = new RectOffset((int)(_difficultyNormalButtonStyleOrig.margin.left * scaleFactor),
                                                                 (int)(_difficultyNormalButtonStyleOrig.margin.right * scaleFactor),
                                                                 (int)(_difficultyNormalButtonStyleOrig.margin.top * scaleFactor),
                                                                 (int)(_difficultyNormalButtonStyleOrig.margin.bottom * scaleFactor));
            _difficultyNormalButtonStyle.padding = new RectOffset((int)(_difficultyNormalButtonStyleOrig.padding.left * scaleFactor),
                                                                  (int)(_difficultyNormalButtonStyleOrig.padding.right * scaleFactor),
                                                                  (int)(_difficultyNormalButtonStyleOrig.padding.top * scaleFactor),
                                                                  (int)(_difficultyNormalButtonStyleOrig.padding.bottom * scaleFactor));
            _difficultyNormalButtonStyle.fixedHeight = (int)(_difficultyNormalButtonStyleOrig.fixedHeight * scaleFactor);
            _difficultyNormalButtonStyle.fixedWidth = (int)(_difficultyNormalButtonStyleOrig.fixedWidth * scaleFactor);

            return _difficultyNormalButtonStyle;
        }
    }

    /// <summary>
    /// The styles used for the hard difficulty button
    /// </summary>
    private GUIStyle difficultyHardButtonStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_difficultyHardButtonStyle == null)
            {
                _difficultyHardButtonStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyHardButtonStyle", System.StringComparison.OrdinalIgnoreCase));
                _difficultyHardButtonStyle = new GUIStyle(_difficultyHardButtonStyleOrig);
            }
            // Scale the style according to the screen size
            _difficultyHardButtonStyle.fontSize = (int)(_difficultyHardButtonStyleOrig.fontSize * scaleFactor);
            _difficultyHardButtonStyle.margin = new RectOffset((int)(_difficultyHardButtonStyleOrig.margin.left * scaleFactor),
                                                               (int)(_difficultyHardButtonStyleOrig.margin.right * scaleFactor),
                                                               (int)(_difficultyHardButtonStyleOrig.margin.top * scaleFactor),
                                                               (int)(_difficultyHardButtonStyleOrig.margin.bottom * scaleFactor));
            _difficultyHardButtonStyle.padding = new RectOffset((int)(_difficultyHardButtonStyleOrig.padding.left * scaleFactor),
                                                                (int)(_difficultyHardButtonStyleOrig.padding.right * scaleFactor),
                                                                (int)(_difficultyHardButtonStyleOrig.padding.top * scaleFactor),
                                                                (int)(_difficultyHardButtonStyleOrig.padding.bottom * scaleFactor));
            _difficultyHardButtonStyle.fixedHeight = (int)(_difficultyHardButtonStyleOrig.fixedHeight * scaleFactor);
            _difficultyHardButtonStyle.fixedWidth = (int)(_difficultyHardButtonStyleOrig.fixedWidth * scaleFactor);

            return _difficultyHardButtonStyle;
        }
    }

    /// <summary>
    /// The styles used for tooltip label
    /// </summary>
    private GUIStyle tooltipLabelStyle
    {
        get
        {
            // If the style hasn't been retrieved, fetch it and make a copy for scaling
            if (_tooltipLabelStyleStyle == null)
            {
                _tooltipLabelStyleStyleOrig = smartMenuSkin.customStyles.First(s => s.name.Equals("tooltipLabelStyle", System.StringComparison.OrdinalIgnoreCase));
                _tooltipLabelStyleStyle = new GUIStyle(_tooltipLabelStyleStyleOrig);
            }
            // Scale the style according to the screen size
            _tooltipLabelStyleStyle.fontSize = (int)(_tooltipLabelStyleStyleOrig.fontSize * scaleFactor);
            _tooltipLabelStyleStyle.margin = new RectOffset((int)(_tooltipLabelStyleStyleOrig.margin.left * scaleFactor),
                                                            (int)(_tooltipLabelStyleStyleOrig.margin.right * scaleFactor),
                                                            (int)(_tooltipLabelStyleStyleOrig.margin.top * scaleFactor),
                                                            (int)(_tooltipLabelStyleStyleOrig.margin.bottom * scaleFactor));
            return _tooltipLabelStyleStyle;
        }
    }

    /// <summary>
    /// The styles used for tooltip label
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

    private GUIStyleState[] _difficultyBtnStyleStatesOnNormal = new GUIStyleState[3];
    private GUIStyleState[] _difficultyBtnStyleStatesOnHover = new GUIStyleState[3];
    #endregion Private Variables ----------------------------------------------

    #region Public Variables --------------------------------------------------
    /// <summary>
    /// The GUI skin containing all the styles used for the GUI elements
    /// </summary>
    public GUISkin smartMenuSkin;

    /// <summary>
    /// The image to used for the screen's title
    /// </summary>
    public Texture titleImg;

    /// <summary>
    /// The backgound image to use for the difficultyLevels area
    /// </summary>
    public Texture difficultyLevelsBackgroundImg;

    /// <summary>
    /// The backgound image to use for the lesson area
    /// </summary>
    public Texture lessonsBackgroundImg;
    #endregion Public Variables -----------------------------------------------

    #region Unity Events ------------------------------------------------------
    /// <summary>
    /// Initialize the data needed to build this page when it is first loaded
    /// </summary>
    void Start()
    {
        // If the player's Lessons and WordSets have not been loaded from 
        // persistent storage, do so
        if (playerData == null)
        {
            playerData = GameStateUtilities.Load();

            if (playerData.Curriculum.Lessons.Count == 0)
                playerData.Curriculum.CreateSampleLessons();

            // Cache the difficulty level button's backgrounds (and such) so they can be interchanged when clicked,
            // and reset their styles to be as if no buttons was selected
            BuildDifficultyLevelStyleCache(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        // Calculate the factor by which the GUI should be sized/resized
        CalculateGUIScaleFactor();

        // Calculate the location where the top left of the GUI should 
        // start if it is to be centered on screen
        int guiAreaLeft = (Screen.width / 2) - ((int)(700 * scaleFactor) / 2);
        int guiAreaTop = (Screen.height / 2) - ((int)(600 * scaleFactor) / 2);
        int guiAreaRight = (Screen.width / 2) + ((int)(700 * scaleFactor) / 2);

        // If any of the calculations fall below zero (because the screen is too small),
        // default to zero
        if (guiAreaLeft < 0)
            guiAreaLeft = 0;
        if (guiAreaTop < 0)
            guiAreaTop = 0;

        // Create the page title
        float titleImgWidth = titleImg.width * scaleFactor;
        float titleImgHeight = titleImg.height * scaleFactor;
        float titleDistanceFromTop = 30.0f * scaleFactor;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (titleImgWidth / 2), titleDistanceFromTop, titleImgWidth, titleImgHeight), titleImg);
        // Create the background image for the difficulty levels
        GUI.DrawTexture(new Rect(guiAreaLeft - (70 * scaleFactor), guiAreaTop + (100 * scaleFactor), (410 * scaleFactor), (330 * scaleFactor)), difficultyLevelsBackgroundImg);
        // Create the background image for the lessons
        GUI.DrawTexture(new Rect(guiAreaLeft + (400 * scaleFactor), guiAreaTop + (60 * scaleFactor), (290 * scaleFactor), (480 * scaleFactor)), lessonsBackgroundImg);

        GUILayout.BeginArea(new Rect(guiAreaLeft, guiAreaTop, (700 * scaleFactor), (600 * scaleFactor)));

        // Create the areas the user will use to select the game difficulty and the lesson to learn
        CreateDifficultyLevelArea();
        CreateAllLessonsArea();

        GUILayout.EndArea();

        // Create the buttons to start the game and return to the main menu
        CreateStartGameBtn();
        CreateMainMenuBtn();
    }

    #region Difficulty Level Area Methods -------------------------------------
    /// <summary>
    /// Create all the controls used to display the enemy difficulty levels
    /// </summary>
    private void CreateDifficultyLevelArea()
    {
        // If the List of enemy difficulty level names has not yet been built, 
        // populate the difficulty level name cache
        BuildDifficultyLevelNamesCache(false);

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(0, (110 * scaleFactor), (300 * scaleFactor), (300 * scaleFactor)));

        GUILayout.BeginVertical();

        // Create the Curriculum header label
        GUILayout.Label("Enemy Difficulty:", headerStyle);

        // Create the buttons used to select the game's difficulty
        CreateDifficultyButtons();

        // Create the label used to display a tooltip describing what each difficulty level means
        GUILayout.Space((20.0f * scaleFactor));
        CreateDifficultyTooltipLabel();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Create the buttons used to selecte the game's difficulty
    /// </summary>
    private void CreateDifficultyButtons()
    {
        // Ensure the buttons are laid out horizontally
        GUILayout.BeginHorizontal();

        // Create a GUIContext for each so that each button will get both a textual label and a tooltip
        GUIContent easyGuiContent = new GUIContent(difficultyLevelNames[0], difficultyLevelDescriptions[0]);
        GUIContent normalGuiContent = new GUIContent(difficultyLevelNames[1], difficultyLevelDescriptions[1]);
        GUIContent hardGuiContent = new GUIContent(difficultyLevelNames[2], difficultyLevelDescriptions[2]);

        // Create the three difficulty buttons, changing their backgrounds based on which one is selected.
        // While a SelectionGrid would do this automatically, it does not allow for it's contents to 
        // have different backgrounds (which we want).
        if (GUILayout.Button(easyGuiContent, difficultyEasyButtonStyle))
            selectedDifficultyLevelBtnIdx = 0;
        if (GUILayout.Button(normalGuiContent, difficultyNormalButtonStyle))
            selectedDifficultyLevelBtnIdx = 1;
        if (GUILayout.Button(hardGuiContent, difficultyHardButtonStyle))
            selectedDifficultyLevelBtnIdx = 2;
        SetDifficultyLevelStyle(selectedDifficultyLevelBtnIdx);

        // End the cotroller wrappers
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Create the enemy difficulty level tooltip label
    /// </summary>
    private void CreateDifficultyTooltipLabel()
    {
        // Retrieve the tooltip 
        string tooltip = GUI.tooltip;

        // If there is no tooltip, as would be the case if the user is not hovering 
        // over a button, use the tooltip of the selected button
        if (string.IsNullOrEmpty(tooltip))
        {
            tooltip = difficultyLevelDescriptions[selectedDifficultyLevelBtnIdx];
        }

        // Create the tooltip label
        GUILayout.Label(tooltip, tooltipLabelStyle);
    }
    #endregion Difficulty Level Area Methods ----------------------------------

    #region All Lessons Area Methods ----------------------------------------
    /// <summary>
    /// Create all the controls used to display all existing lessons 
    /// </summary>
    private void CreateAllLessonsArea()
    {
        // If the List of lesson names has not yet been built, 
        // populate the lessons name cache
        BuildLessonNamesCache(false);

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect((450 * scaleFactor), (70 * scaleFactor), (200 * scaleFactor), (450 * scaleFactor)));

        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the Select Lesson header label
        GUILayout.Label("Select Lesson:", headerStyle);

        // Create a scrollable area incase the number of lessons exceed the space available
        allLessonsScrollPosition = GUILayout.BeginScrollView(allLessonsScrollPosition);

        // Build a vertical, one-column grid of buttons corresponding to the 
        // lesson names, and note which one the player selected
        selectedLessonBtnIdx = GUILayout.SelectionGrid(selectedLessonBtnIdx, lessonNames.ToArray(), 1, generalButtonStyle);

        GUILayout.EndScrollView();

        // End the cotroller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    #endregion All Lessons Area Methods ---------------------------------------

    #region Other GUI Object Methods ------------------------------------------
    /// <summary>
    /// Create the button used to start the game, and 
    /// load the first level if it has been clicked.
    /// </summary>
    private void CreateStartGameBtn()
    {
        // Create the button used to create a new lesson.  If it was clicked...
        if (GUI.Button(new Rect((Screen.width / 2) - (200 * scaleFactor), Screen.height - (100 * scaleFactor), (400 * scaleFactor), (60 * scaleFactor)), "Play Game!", startGameButtonStyle))
        {
            // Set the enemy difficulty level the game will use
            Context.EnemyDifficulty = difficultyLevels[selectedDifficultyLevelBtnIdx];
            // Set the lesson the game will use
            Context.CurrentLessonId = playerData.Curriculum.Lessons[selectedLessonBtnIdx].ID;

            // Load the first level
            Application.LoadLevel("EnemyTesting");
        }
    }

    /// <summary>
    /// Create the button used to return to the main menu, and 
    /// load the main menu if it has been clicked.
    /// </summary>
    private void CreateMainMenuBtn()
    {
        // Create the button used to create a new lesson.  If it was clicked...
        if (GUI.Button(new Rect(Screen.width - (200 * scaleFactor), Screen.height - (90 * scaleFactor), (150 * scaleFactor), (40 * scaleFactor)), "Main Menu", mainMenuButtonStyle))
        {
            // Return to the main menu
            Application.LoadLevel("MainMenu");
        }
    }
    #endregion Other GUI Object Methods ---------------------------------------

    #region Helper Methods ----------------------------------------------------
    /// <summary>
    /// Cache the names and descriptions of all the available enemy difficulty levels. 
    /// This is done to minimize execution time between frames, 
    /// as the GUI is built from cache.
    /// </summary>
    /// <param name="refresh">Refresh the difficulty level names and descriptions caches</param>
    private void BuildDifficultyLevelNamesCache(bool refresh)
    {
        // If the List of difficulty level names has not yet been built...
        if (difficultyLevelNames == null || refresh == true)
        {   // Create a query to retrieve all enemy difficulty names from all existing difficulty levels
            var queryDifficultyLevelNames = from EnemyDifficulty e in difficultyLevels
                                            select e.Name;
            // Convert the query results to a List
            difficultyLevelNames = queryDifficultyLevelNames.ToList();

            // Create a query to retrieve all enemy difficulty descriptions from all existing difficulty levels
            var queryDifficultyLevelDescriptions = from EnemyDifficulty e in difficultyLevels
                                                   select e.Description;
            // Convert the query results to a List
            difficultyLevelDescriptions = queryDifficultyLevelDescriptions.ToList();
        }
    }

    /// <summary>
    /// Cache the difficulty button's backgrouns so they can be interchanged when a 
    /// button is clicked to show which button was selected.
    /// </summary>
    /// <param name="resetStyles">Reset the difficulty button's styles to their default state</param>
    private void BuildDifficultyLevelStyleCache(bool resetStyles)
    {
        // Cache the difficulty button's backgrounds so they can be interchanged when a button is clicked
        _difficultyBtnStyleStatesOnNormal[0] = difficultyEasyButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnNormal[1] = difficultyNormalButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnNormal[2] = difficultyHardButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnHover[0] = difficultyEasyButtonStyle.onHover;
        _difficultyBtnStyleStatesOnHover[1] = difficultyNormalButtonStyle.onHover;
        _difficultyBtnStyleStatesOnHover[2] = difficultyHardButtonStyle.onHover;

        if (resetStyles)
        {
            // Reset the background textures and text colors to their default state, since 
            // they might be set to the state they were in when the scene was last exited
            SetDifficultyLevelStyle(selectedDifficultyLevelBtnIdx);
        }
    }

    /// <summary>
    /// Apply the correct styles to the difficulty buttons to show which was was selected/clicked.
    /// </summary>
    /// <param name="selectedButtonIdx">The index of the button clicked</param>
    private void SetDifficultyLevelStyle(int selectedButtonIdx)
    {
        switch (selectedButtonIdx)
        {
            // Easy difficulty button was clicked
            case 0:
                difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnHover[0];
                difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[1];
                difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[2];
                break;
            // Normal difficulty button was clicked
            case 1:
                difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[0];
                difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnHover[1];
                difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[2];
                break;
            // Hard difficulty button was clicked
            case 2:
                difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[0];
                difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[1];
                difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnHover[2];
                break;
            // If no recognized difficulty button was clicked, default to Normal
            default:
                goto case 1;
        }
    }

    /// <summary>
    /// Cache the names of all the available lessons. 
    /// This is done to minimize execution time between frames, 
    /// as the GUI is built from cache.
    /// </summary>
    /// <param name="refresh">Refresh the lesson names cache</param>
    private void BuildLessonNamesCache(bool refresh)
    {
        // If the List of lesson names has not yet been built...
        if (lessonNames == null || refresh == true)
        {   // Create a query to retrieve all lesson names from all existing lessons
            var queryLessonNames = from Lesson l in playerData.Curriculum.Lessons
                                   select l.Name;
            // Convert the query results to a List
            lessonNames = queryLessonNames.ToList();
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
    #endregion Helper Methods --------------------------------------------------
}
