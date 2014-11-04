using UnityEngine;
using System.Collections;
using System.Linq;
using GGProductions.LetterStorm.Data;
using System.Collections.Generic;
using GGProductions.LetterStorm.Utilities;
using GGProductions.LetterStorm.Data.Collections;
using GGProductions.LetterStorm.Configuration.Collections;
using GGProductions.LetterStorm.Configuration;

public class NewGame : MonoBehaviour {

    #region Private Variables -------------------------------------------------
    private PlayerData playerData = null;
    private DifficultyLevels difficultyLevels = new DifficultyLevels();
    private List<string> difficultyLevelNames = null;
    private List<string> difficultyLevelDescriptions = null;
    private List<string> lessonNames = null;
    private int selectedDifficultyLevelBtnIdx = 0;
    private int selectedLessonBtnIdx = 0;
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
    private GUIStyle _headerStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("headerStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The style used for most buttons
    /// </summary>
    private GUIStyle _generalButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("generalButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for return to main menu button
    /// </summary>
    private GUIStyle _mainMenuButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("mainMenuButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for difficulty buttons
    /// </summary>
    private GUIStyle _difficultyButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for the easy difficulty button
    /// </summary>
    private GUIStyle _difficultyEasyButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyEasyButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for the normal difficulty button
    /// </summary>
    private GUIStyle _difficultyNormalButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyNormalButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for the hard difficulty button
    /// </summary>
    private GUIStyle _difficultyHardButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("difficultyHardButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for tooltip label
    /// </summary>
    private GUIStyle _tooltipLabelStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("tooltipLabelStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for tooltip label
    /// </summary>
    private GUIStyle _startGameButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("startGameButtonStyle", System.StringComparison.OrdinalIgnoreCase));
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
    /// The backgound image to use for the difficultyLevels area
    /// </summary>
    public Texture difficultyLevelsBackgroundImg;

    /// <summary>
    /// The backgound image to use for the lesson area
    /// </summary>
    public Texture lessonsBackgroundImg;

    /// <summary>
    /// Images to use for the easy, normal, and hard difficulty levels
    /// </summary>
    public Texture easyDifficultyImg, normalDifficultyImg, hardDifficultyImg;
    #endregion Public Variables -----------------------------------------------
    // Use this for initialization
	void Start () {
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
	void Update () {
	
	}

    void OnGUI()
    {
        CreateGUI();
    }

    private void CreateGUI()
    {
        // Calculate the location where the top left of the GUI should 
        // start if it is to be centered on screen
        int guiAreaLeft = (Screen.width / 2) - (700 / 2);
        int guiAreaTop = (Screen.height / 2) - (600 / 2);
        int guiAreaRight = (Screen.width / 2) + (700 / 2);

        // If any of the calculations fall below zero (because the screen is too small),
        // default to zero
        if (guiAreaLeft < 0)
            guiAreaLeft = 0;
        if (guiAreaTop < 0)
            guiAreaTop = 0;

        // Create the background for the screen
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImg);
        // Create the background image for the difficulty levels
        GUI.DrawTexture(new Rect(guiAreaLeft - 70, guiAreaTop + 100, 410, 330), difficultyLevelsBackgroundImg);
        
        // Create the background image for the lessons
        GUI.DrawTexture(new Rect(guiAreaLeft + 400, guiAreaTop - 10, 290, 530), lessonsBackgroundImg);
        // Create the background page (for the curriculum and lessons)
        //GUI.DrawTexture(new Rect(guiAreaLeft + 470, guiAreaTop - 10, 270, 270), wordAreaBackgroundImg);

        GUILayout.BeginArea(new Rect(guiAreaLeft, guiAreaTop, 700, 600));

        CreateDifficultyLevelArea();

        CreateAllLessonsArea();

        GUILayout.EndArea();

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
        GUILayout.BeginArea(new Rect(0, 110, 300, 300));
        
        GUILayout.BeginVertical();

        // Create the Curriculum header label
        GUILayout.Label("Enemy Difficulty:", _headerStyle);

        // Create the buttons used to select the game's difficulty
        CreateDifficultyButtons();

        // Create the label used to display a tooltip describing what each difficulty level means
        GUILayout.Space(20.0f);
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
        if (GUILayout.Button(easyGuiContent, _difficultyEasyButtonStyle))
            selectedDifficultyLevelBtnIdx = 0;
        if (GUILayout.Button(normalGuiContent, _difficultyNormalButtonStyle))
            selectedDifficultyLevelBtnIdx = 1;
        if (GUILayout.Button(hardGuiContent, _difficultyHardButtonStyle))
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
        GUILayout.Label(tooltip, _tooltipLabelStyle);
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
        GUILayout.BeginArea(new Rect(450, 0, 200, 500));

        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the Select Lesson header label
        GUILayout.Label("Select Lesson:", _headerStyle);
        
        // Create a scrollable area incase the number of lessons exceed the space available
        allLessonsScrollPosition = GUILayout.BeginScrollView(allLessonsScrollPosition);

        // Build a vertical, one-column grid of buttons corresponding to the 
        // lesson names, and note which one the player selected
        selectedLessonBtnIdx = GUILayout.SelectionGrid(selectedLessonBtnIdx, lessonNames.ToArray(), 1, _generalButtonStyle);

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
        if (GUI.Button(new Rect((Screen.width / 2) - 200, Screen.height - 100, 400, 60), "Play Game!", _startGameButtonStyle))
        {
            // Set the enemy difficulty level the game will use
            Context.EnemyDifficulty = difficultyLevels[selectedDifficultyLevelBtnIdx];
            // Set the lesson the game will use
            Context.CurrentLessonId = playerData.Curriculum.Lessons[selectedLessonBtnIdx].ID;
            // Set the initial player life count based on the difficulty choosen
            Context.PlayerLives = Context.EnemyDifficulty.InitialLifeCount;

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
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 90, 150, 40), "Main Menu", _mainMenuButtonStyle))
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
        _difficultyBtnStyleStatesOnNormal[0] = _difficultyEasyButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnNormal[1] = _difficultyNormalButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnNormal[2] = _difficultyHardButtonStyle.onNormal;
        _difficultyBtnStyleStatesOnHover[0] = _difficultyEasyButtonStyle.onHover;
        _difficultyBtnStyleStatesOnHover[1] = _difficultyNormalButtonStyle.onHover;
        _difficultyBtnStyleStatesOnHover[2] = _difficultyHardButtonStyle.onHover;

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
                _difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnHover[0];
                _difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[1];
                _difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[2];
                break;
            // Normal difficulty button was clicked
            case 1:
                _difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[0];
                _difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnHover[1];
                _difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[2];
                break;
            // Hard difficulty button was clicked
            case 2:
                _difficultyEasyButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[0];
                _difficultyNormalButtonStyle.normal = _difficultyBtnStyleStatesOnNormal[1];
                _difficultyHardButtonStyle.normal = _difficultyBtnStyleStatesOnHover[2];
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
    #endregion Helper Methods --------------------------------------------------
}
