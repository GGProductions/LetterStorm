﻿using UnityEngine;
using System.Collections;
using System.Linq;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Utilities;
using GGProductions.LetterStorm.Data.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ManageLessons : MonoBehaviour
{
    #region Private Variables -------------------------------------------------
    private PlayerData playerData = null;
    private List<string> lessonNames = null;
    private List<string> lessonWordTexts = null;
    private int selectedLessonBtnIdx = 0;
    private int selectedWordBtnIdx = 0;
    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// within the AllLessons Area
    /// </summary>
    private Vector2 allLessonsScrollPosition;
    /// <summary>
    /// Vector used to store the scrolled position of the Scrollable View 
    /// for the selected lesson's words within the CurrentLessons Area
    /// </summary>
    private Vector2 lessonWordsScrollPosition;

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
    /// The styles used for subheader labels
    /// </summary>
    private GUIStyle _subHeaderStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("subHeaderStyle", System.StringComparison.OrdinalIgnoreCase));
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
    /// The styles used for create buttons
    /// </summary>
    private GUIStyle _createButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("createButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used for delete buttons
    /// </summary>
    private GUIStyle _deleteButtonStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("deleteButtonStyle", System.StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// The styles used by the input fields
    /// </summary>
    private GUIStyle _inputFieldsStyle
    {
        get
        {
            return smartMenuSkin.customStyles.First(s => s.name.Equals("inputFieldsStyle", System.StringComparison.OrdinalIgnoreCase));
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
    /// The background image for the screen
    /// </summary>
    public Texture backgroundImg;

    /// <summary>
    /// The backgound image to use for the curriculum and lesson areas
    /// </summary>
    public Texture areaBackgroundImg;

    /// <summary>
    /// The backgound image to use for the word area
    /// </summary>
    public Texture wordAreaBackgroundImg;
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
        }
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
        // Calculate the location where the top left of the GUI should 
        // start if it is to be centered on screen
        int guiAreaLeft = (Screen.width / 2) - (700 / 2);
        int guiAreaTop = (Screen.height / 2) - (600 / 2) + 70;

        // If any of the calculations fall below zero (because the screen is too small),
        // default to zero
        if (guiAreaLeft < 0)
            guiAreaLeft = 0;
        if (guiAreaTop < 0)
            guiAreaTop = 0;

        // Create the background for the screen
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImg);
        // Create the page title
        GUI.DrawTexture(new Rect((Screen.width / 2) - (titleImg.width / 2), 30.0f, titleImg.width, titleImg.height), titleImg);
        // Create the background page (for the curriculum and lessons)
        GUI.DrawTexture(new Rect(guiAreaLeft - 85, guiAreaTop - 10, 540, 560), areaBackgroundImg);
        // Create the background page (for the curriculum and lessons)
        GUI.DrawTexture(new Rect(guiAreaLeft + 470, guiAreaTop - 10, 270, 270), wordAreaBackgroundImg);

        GUILayout.BeginArea(new Rect(guiAreaLeft, guiAreaTop, 700, 530));

        // Populate the Curriculum/Lessons area with the names of all existing lessons
        bool wasDifferentLessonSelected = CreateAllLessonsArea();

        // Populate the Words area with the word set corresponding to the selected lesson
        CreateCurrentLessonArea(ref selectedLessonBtnIdx, wasDifferentLessonSelected);

        // Create the Edit Word area with the text and hint of the selected word
        CreateWordEditorArea(selectedLessonBtnIdx, ref selectedWordBtnIdx);

        GUILayout.EndArea();

        CreateMainMenuBtn();
    }


    #region All Lessons Area Methods ------------------------------------------
    /// <summary>
    /// Create all the controls used to display all existing lessons and 
    /// create new lessons
    /// </summary>
    /// <returns>Whether or not a new lesson was selected</returns>
    private bool CreateAllLessonsArea()
    {
        // If the List of lesson names has not yet been built, 
        // populate the lessons name cache
        BuildLessonNamesCache(false);

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(0, 0, 200, 530));

        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Note the button that was previously selected
        int oldSelectedLessonIdx = selectedLessonBtnIdx;

        // Create the Curriculum header label
        GUILayout.Label("Curriculum", _headerStyle);

        // Create the button used to create a new lesson, and do so if it has been clicked
        CreateNewLessonBtn(ref selectedLessonBtnIdx, ref oldSelectedLessonIdx);

        // Create a scrollable area incase the number of lessons exceed the space available
        allLessonsScrollPosition = GUILayout.BeginScrollView(allLessonsScrollPosition);
        
        // Build a vertical, one-column grid of buttons corresponding to the 
        // lesson names, and note which one the player selected
        selectedLessonBtnIdx = GUILayout.SelectionGrid(selectedLessonBtnIdx, lessonNames.ToArray(), 1, _generalButtonStyle);

        GUILayout.EndScrollView();

        // End the cotroller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();

        // If the currently-selected button is different from that selected last frame...
        if (selectedLessonBtnIdx != oldSelectedLessonIdx)
            return true;
            // Populate the Words area from a new word set corresponding to the selected lesson
            //CreateCurrentLessonArea(ref selectedLessonBtnIdx, true);
        // Else, repopulate the Words area from the word set stored in cache
        else
            return false;
            //CreateCurrentLessonArea(ref selectedLessonBtnIdx, false);
    }

    /// <summary>
    /// Create the button used to create a new lesson, and create a new lesson
    /// if it has been clicked.
    /// </summary>
    /// <param name="selectedLessonIdx">Reference to the index of the currently selected lesson</param>
    /// <param name="oldSelectedLessonIdx">Reference to the index of the last selected lesson</param>
    private void CreateNewLessonBtn(ref int selectedLessonIdx, ref int oldSelectedLessonIdx)
    {
        // Create the button used to create a new lesson.  If it was clicked...
        if (GUILayout.Button("New Lesson", _createButtonStyle))
        {   // If a lesson with the same name does not already exist...
            if (lessonNames.Contains("Lesson") == false)
            {
                // Add a new lesson with a default title to the beginning of the curriculum (so it shows up at the top of the lessons list)
                playerData.Curriculum.Lessons.Insert(0, new Lesson("Lesson"));
                // Add a default word to the new lesson
                playerData.Curriculum.Lessons[0].Words.Add(new Word("word", "hint"));
                // Rebuild the lesson cache, so a button is created for the new lesson
                BuildLessonNamesCache(true);
                // Ensure that the new button is selected so the lesson editor area is populated with the new lesson
                selectedLessonIdx = 0;
                oldSelectedLessonIdx++;     // Increment the old selected index, since a new lesson has been added before it
            }
        }
    }
    #endregion All Lessons Area Methods ---------------------------------------

    #region Current Lesson Area Methods ---------------------------------------
    /// <summary>
    /// Create all controls used to display and modify the selected lesson
    /// </summary>
    /// <param name="lessonIdx">Reference to the index of the selected lesson</param>
    /// <param name="refreshWordList">Refresh the lesson words cache</param>
    private void CreateCurrentLessonArea(ref int lessonIdx, bool refreshWordList)
    {
        // If a List of the lesson words has not yet been built, or if a new 
        // lesson has been selected and the word list should be refreshed, 
        // populate the word cache
        BuildLessonWordsCache(lessonIdx, refreshWordList);

        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(250, 0, 200, 530));
        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the Lesson header label
        GUILayout.Label("Lesson", _headerStyle);

        // Create the Lesson name subheader label
        GUILayout.Label("Name: ", _subHeaderStyle);

        // Create the text field used to update the current lesson's name, 
        // and do so if the user changes its contents
        CreateLessonNameTextField(lessonIdx);

        // Create the Words subheader label
        GUILayout.Label("", _subHeaderStyle);
        GUILayout.Label("Words: ", _subHeaderStyle);

        // Create the button used to create a new word, and do so if it has been clicked
        CreateNewWordBtn(lessonIdx, ref selectedWordBtnIdx);

        // Create a scrollable area incase the number of words exceeds the space available
        lessonWordsScrollPosition = GUILayout.BeginScrollView(lessonWordsScrollPosition);

        // Build a vertical, one-column grid of buttons corresponding to the 
        // lesson words, and note which one the player selected
        selectedWordBtnIdx = GUILayout.SelectionGrid(selectedWordBtnIdx, lessonWordTexts.ToArray(), 1, _generalButtonStyle);

        GUILayout.EndScrollView();

        // Create the button used to delete the selected lesson, and do so if it has been clicked
        GUILayout.Space(10.0f);
        CreateDeleteLessonBtn(ref lessonIdx);

        // End the cotroller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Create the text field used to update the current lesson's name, and 
    /// update the lesson's name if the user changes the text field's contents
    /// </summary>
    /// <param name="lessonIdx">The index of the current lesson</param>
    private void CreateLessonNameTextField(int lessonIdx)
    {
        // Build the text field used to update the current lesson's name, and retrieve any text the user entered
        string lessonName = GUILayout.TextField(lessonNames[lessonIdx], 50, _inputFieldsStyle);
        // If text was entered by the user, and if it only contains characters, numbers, or spaces, accept/store it
        if (lessonNames[lessonIdx].Equals(lessonName, System.StringComparison.InvariantCultureIgnoreCase) == false &&
            Regex.IsMatch(lessonName, "([^A-Za-z0-9 ]+)") == false)
        {
            playerData.Curriculum.Lessons[lessonIdx].Name = lessonName;
            BuildLessonNamesCache(true); // Refresh the lesson names cache so the updated
                                         // lesson title will appear in the lessons list
        }
    }

    /// <summary>
    /// Create the button used to create a new word, and create a new word
    /// if it has been clicked.
    /// </summary>
    /// <param name="lessonIdx">The index of the current lesson</param>
    /// <param name="selectedWordIdx">Reference to the index of the selected word</param>
    private void CreateNewWordBtn(int lessonIdx, ref int selectedWordIdx)
    {
        // If the button used to create a new word was clicked...
        if (GUILayout.Button("New Word", _createButtonStyle))
        {   // If the lesson does not already contain a new word...
            if (lessonWordTexts.Contains("word") == false)
            {
                // Add a default word and hint to the beginning of the lesson (so it shows up at the top of the word list)
                playerData.Curriculum.Lessons[lessonIdx].Words.Insert(0, new Word("word", "hint"));
                // Rebuild the word cache, so the a button is created for the new word
                BuildLessonWordsCache(lessonIdx, true);
                // Ensure that the new button is selected so the word editor area is populated with the new word
                selectedWordIdx = 0;
            }
        }
    }

    /// <summary>
    /// Create the button used to delete the selected lesson, 
    /// and delete the lesson if the user has clicked it
    /// </summary>
    /// <param name="lessonIdx">Reference to the index of the selected lesson</param>
    private void CreateDeleteLessonBtn(ref int lessonIdx)
    {
        // Create the button used to delete the selected lesson.  If it was clicked...
        if (GUILayout.Button("Delete Lesson", _deleteButtonStyle))
        {   // If there is at least one lesson left...
            if (lessonNames.Count > 1)
            {   // Remove the lesson from the curriculum
                playerData.Curriculum.Lessons.RemoveAt(lessonIdx);
                // Update the lesson name cache so the associated button is removed
                BuildLessonNamesCache(true);

                // If the lesson index is now out of bounds,
                // default it to the location of the last existing lesson
                if (lessonNames.Count <= lessonIdx)
                    lessonIdx = lessonNames.Count - 1;

                // Update the lesson words cache to reflect the newly-selected lesson
                BuildLessonWordsCache(lessonIdx, true);
            }
        }
    }
    #endregion Current Lesson Area Methods ------------------------------------

    #region Word Editor Area Methods ------------------------------------------
    /// <summary>
    /// Create all controls used to display and modify the selected word
    /// </summary>
    /// <param name="lessonIdx">The index of the selected lesson</param>
    /// <param name="wordIdx">Reference to the index of the selected word</param>
    private void CreateWordEditorArea(int lessonIdx, ref int wordIdx)
    {
        // Wrap everything in the designated GUI Area to group controls together
        GUILayout.BeginArea(new Rect(500, 15, 200, 250));
        // Ensure the controls are laid out vertically
        GUILayout.BeginVertical();

        // Create the text fields to edit the word text and hint, retrieving anything the user entered
        Word wordToEdit = playerData.Curriculum.Lessons[lessonIdx].Words[wordIdx];
        GUILayout.Label("Word: ", _subHeaderStyle);
        CreateWordTextTextField(lessonIdx, wordToEdit);
        GUILayout.Label("Hint: ", _subHeaderStyle);
        CreateWordHintTextArea(wordToEdit);
        
        // End the controller wrappers
        GUILayout.EndVertical();
        GUILayout.EndArea();

        // Wrap everything in the designated GUI Area to group controls together
        // Why create a second area?  So the delete button can stay fixed as the Hint text area grows/shrinks to fit its contents
        GUILayout.BeginArea(new Rect(500, 265, 200, 300));
        // Create the button used to delete the selected word, and do so if it was clicked
        CreateDeleteWordBtn(lessonIdx, ref wordIdx);
        // End the controller wrappers
        GUILayout.EndArea();
    }

    /// <summary>
    /// Create the text field used to edit a word's text, 
    /// and update its text if the user changes it
    /// </summary>
    /// <param name="lessonIdx">The index of the selected lesson</param>
    /// <param name="wordToEdit">The Word being edited</param>
    private void CreateWordTextTextField(int lessonIdx, Word wordToEdit)
    {
        // Create the text field used to edit the word's text, and retrieve its current contents
        string wordText = GUILayout.TextField(wordToEdit.Text, 25, _inputFieldsStyle);

        // If text was entered by the user, and if it only contains characters, accept/store it
        if (wordToEdit.Text.Equals(wordText, System.StringComparison.InvariantCultureIgnoreCase) == false &&
            Regex.IsMatch(wordText, "([^A-Za-z]+)") == false)
        {
            wordToEdit.Text = wordText.ToLowerInvariant();
            BuildLessonWordsCache(lessonIdx, true); // Refresh the word cache so the updated
            // word will appear in the words list
        }
    }

    /// <summary>
    /// Create the text area used to edit a word's hint,
    /// and update its hint if the user changes it
    /// </summary>
    /// <param name="wordToEdit">The Word being edited</param>
    private void CreateWordHintTextArea(Word wordToEdit)
    {
        // Create the text area used to edit the word's hint, and store any changes to the hint
        wordToEdit.Hint = GUILayout.TextArea(wordToEdit.Hint, 150, _inputFieldsStyle);
    }

    /// <summary>
    /// Create the button used to delete the selected word, 
    /// and delete the word if the user has clicked it
    /// </summary>
    /// <param name="lessonIdx">The index of the selected lesson</param>
    /// <param name="wordIdx">Reference to the index of the selected word</param>
    private void CreateDeleteWordBtn(int lessonIdx, ref int wordIdx)
    {
        // If the button used to delete the selected word was clicked...
        if (GUILayout.Button("Delete Word", _deleteButtonStyle))
        {   // If there is at least one word left in the lesson...
            if (lessonWordTexts.Count > 1)
            {   // Remove the word from the lesson
                playerData.Curriculum.Lessons[lessonIdx].Words.RemoveAt(wordIdx);
                // Update the lesson's word cache so the associated button is removed
                BuildLessonWordsCache(lessonIdx, true);

                // If the word index is now out of bounds,
                // default it to the location of the last existing word
                if (lessonWordTexts.Count <= wordIdx)
                    wordIdx = lessonWordTexts.Count - 1;
            }
        }
    }
    #endregion Word Editor Area Methods ---------------------------------------

    #region Other GUI Object Methods ------------------------------------------
    /// <summary>
    /// Create the button used to return to the main menu, and save the curriculum and 
    /// load the main menu if it has been clicked.
    /// </summary>
    private void CreateMainMenuBtn()
    {
        // Create the button used to create a new lesson.  If it was clicked...
        if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 90, 150, 40), "Main Menu", _mainMenuButtonStyle))
        {
            GameStateUtilities.Save(playerData);

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
    
    /// <summary>
    /// Cache the text of the words for the currently selected lesson. 
    /// This is done to minimize execution time between frames, 
    /// as the GUI is built from cache.
    /// </summary>
    /// <param name="lessonIdx">The id of the lesson whose words should be cached</param>
    /// <param name="refresh">Refresh the word cache</param>
    private void BuildLessonWordsCache(int lessonIdx, bool refresh)
    {
        // If a List of the lesson words has not yet been built, 
        // or if the word list should be refreshed...
        if (lessonWordTexts == null || refresh == true)
        {   // Create a query to retrieve the text of all words in the selected lesson
            var queryWordTexts = from Word w in playerData.Curriculum.Lessons[lessonIdx].Words
                                 select w.Text;
            // Convert the query results to a List
            lessonWordTexts = queryWordTexts.ToList();
        }
    }

    #endregion Helper Methods -------------------------------------------------
}
