using UnityEngine;
using System.Collections;
using System.Linq;
using GGProductions.LetterStorm.Data;
using System.Collections.Generic;
using GGProductions.LetterStorm.Utilities;
using GGProductions.LetterStorm.Data.Collections;

public class NewGame : MonoBehaviour {

    #region Private Variables -------------------------------------------------
    private static PlayerData playerData = null;
    private static List<string> lessonNames = null;
    private static int selectedLessonBtnIdx = 0;
    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// within the AllLessons Area
    /// </summary>
    private static Vector2 allLessonsScrollPosition;
    #endregion Private Variables ----------------------------------------------

    #region Public Variables --------------------------------------------------
    /// <summary>
    /// The styles used for header labels
    /// </summary>
    public GUIStyle headerStyle;

    /// <summary>
    /// The style used for most buttons
    /// </summary>
    public GUIStyle generalButtonStyle;
    
    /// <summary>
    /// The styles used for return to main menu button
    /// </summary>
    public GUIStyle mainMenuButtonStyle;
    #endregion Public Variables -----------------------------------------------
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // If the player's Lessons and WordSets have not been loaded from 
        // persistent storage, do so
        if (playerData == null)
        {
            playerData = GameStateUtilities.Load();

            if (playerData.Curriculum.Lessons.Count == 0)
                playerData.Curriculum.CreateSampleLessons();
        }


        CreateGUI();
    }

    private void CreateGUI()
    {
        // Calculate the location where the top left of the GUI should 
        // start if it is to be centered on screen
        int guiAreaLeft = (Screen.width / 2) - (700 / 2);
        int guiAreaTop = (Screen.height / 2) - (600 / 2);

        // If any of the calculations fall below zero (because the screen is too small),
        // default to zero
        if (guiAreaLeft < 0)
            guiAreaLeft = 0;
        if (guiAreaTop < 0)
            guiAreaTop = 0;

        // Create the background for the screen
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImg);
        // Create the background page (for the curriculum and lessons)
        //GUI.DrawTexture(new Rect(guiAreaLeft - 85, guiAreaTop - 10, 540, 630), areaBackgroundImg);
        // Create the background page (for the curriculum and lessons)
        //GUI.DrawTexture(new Rect(guiAreaLeft + 470, guiAreaTop - 10, 270, 270), wordAreaBackgroundImg);

        GUILayout.BeginArea(new Rect(guiAreaLeft, guiAreaTop, 700, 600));

        CreateAllLessonsArea();

        GUILayout.EndArea();

        CreateMainMenuBtn();
    }

    #region All Lessons Area Methods ----------------------------------------
    /// <summary>
    /// Create all the controls used to display all existing lessons and 
    /// create new lessons
    /// </summary>
    private void CreateAllLessonsArea()
    {
        // If the List of lesson names has not yet been built, 
        // populate the lessons name cache
        BuildLessonNamesCache(false);

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(0, 0, 200, 600));

        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the Curriculum header label
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

        // Populate the Words area from a new word set corresponding to the selected lesson
        //CreateCurrentLessonArea(ref selectedLessonBtnIdx, true);
    }
    #endregion All Lessons Area Methods ---------------------------------------

    #region Other GUI Object Methods ------------------------------------------
    /// <summary>
    /// Create the button used to return to the main menu, and 
    /// load the main menu if it has been clicked.
    /// </summary>
    private void CreateMainMenuBtn()
    {
        // Create the button used to create a new lesson.  If it was clicked...
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 90, 150, 40), "Main Menu", mainMenuButtonStyle))
        {
            // Return to the main menu
            Application.LoadLevel("MainMenu");
        }
    }
    #endregion Other GUI Object Methods ---------------------------------------

    #region Helper Methods ----------------------------------------------------
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
