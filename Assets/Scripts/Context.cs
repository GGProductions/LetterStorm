using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.Data.Collections;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Utilities;
using System;
using GGProductions.LetterStorm.Configuration;
using GGProductions.LetterStorm.Configuration.Collections;

public class Context : MonoBehaviour
{

    #region Private Variables ---------------------------------------------
    // Create the private variables that the this class's 
    //  properties will use to store their data
    private static LessonBook _curriculum;
    private static int? _playerLives = null;
    private static Health _playerHealth;
    private static Inventory _playerInventory;
    private static string _selectedLetter;
    private static char[] _alphabet;
    private static Word _word;
    private static int _level = 1;
    private static EnemyDifficulty _enemyDifficulty;
    private static Guid _currentLessonId;
    #endregion Private Variables ------------------------------------------

    #region Enums ---------------------------------------------------------
    /// <summary>
    /// Represents that data that should be loaded from the save file
    /// </summary>
    enum DataToLoad
    {
        /// <summary>Only load the curriculum from the save file</summary>
        Curriculum = 0,
        /// <summary>Only load the game state from the save file</summary>
        GameState,
        /// <summary>Load all data from the save file</summary>
        Everything
    }
    #endregion Enums ------------------------------------------------------

    #region Properties ----------------------------------------------------
    /// <summary>
    /// The number of the current level, with the first level being 1.
    /// </summary>
    public static int LevelNum
    {
        get
        {
            return _level;
        }
    }

    /// <summary>
    /// The Word the user should learn how to spell
    /// </summary>
    public static Word Word
    {
        get 
        {
            // If the word has not yet been retrieved...
            if (_word == null)
            {   
                // If the user has not selected a lesson when this property is accessed, default to the first lesson available
                if (_currentLessonId == Guid.Empty)
                {
                    _word = Curriculum.Lessons[0].Words.GetRandomWord();
                    Debug.LogWarning("No lesson was selected.  Using first avialable lesson.");
                }
                // Else, return a random word from the lesson selected by the user
                else
                    _word = Curriculum.Lessons.GetLessonById(_currentLessonId).Words.GetRandomWord();
            }

            return _word; 
        }
    }

    /// <summary>
    /// Stores the alphabet for efficient coding
    /// </summary>
    public static char[] Alphabet
    {
        get
        {
            if (_alphabet == null)
            {
                _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            }
            return _alphabet;
        }
        set { _alphabet = value; }
    }

    /// <summary>
    /// Represents all the Lessons for the current user
    /// </summary>
    public static LessonBook Curriculum
    {
        get {
            if (_curriculum == null)
            {
                // Populate the curriculum from the save file
                LoadDataFromSaveFile(DataToLoad.Curriculum);
            }
            return _curriculum; 
        }
        set { _curriculum = value; }
    }

    /// <summary>Tracks player health</summary>
    public static Health PlayerHealth
    {
        get
        {
            // If the life count has not yet been set, default it to that associated with the enemy difficulty selected by the user
            if (_playerHealth == null)
            {
                _playerHealth = new Health(EnemyDifficulty);
            }
            return _playerHealth;
        }
        set { _playerHealth = value; }
    }

    /// <summary>
    /// Player inventory
    /// </summary>
    public static Inventory PlayerInventory
    {
        get {
            if (_playerInventory == null)
            {
                _playerInventory = new Inventory();
            }
            return _playerInventory; 
        }
        set { _playerInventory = value; }
    }

    /// <summary>
    /// Selected letter in inventory
    /// </summary>
    public static string SelectedLetter
    {
        get { return _selectedLetter; }
        set { _selectedLetter = value; }
    }

    /// <summary>
    /// The enemy difficulty level chosen for the game playthrough
    /// </summary>
    public static EnemyDifficulty EnemyDifficulty
    {
        get {
            // If the user has not selected a difficulty level by the time this property is accessed, default to returning the "Normal" enemy difficulty level
            if (_enemyDifficulty == null)
            {
                DifficultyLevels dl = new DifficultyLevels();
                _enemyDifficulty = dl.GetDifficultyByName("Normal");
                Debug.LogWarning("No enemy difficulty was selected.  Using 'Normal' difficulty for this playthrough.");
            }
            return _enemyDifficulty; 
        }
        set { _enemyDifficulty = value; }
    }

    /// <summary>
    /// The id of the lesson chosen for the game playthrough
    /// </summary>
    public static Guid CurrentLessonId
    {
        get { return _currentLessonId; }
        set { _currentLessonId = value; }
    }
    #endregion Properties -------------------------------------------------

    #region Helper Methods ----------------------------------------------------
    /// <summary>
    /// Populate the Context from the save file
    /// </summary>
    /// <param name="desiredData">The type of data to load from the save file</param>
    private static void LoadDataFromSaveFile(DataToLoad desiredData)
    {
        // Retrieve all data from the save file
        PlayerData tmpPlayerData = GameStateUtilities.Load();

        // If the curriculum should be loaded...
        if (desiredData == DataToLoad.Curriculum || desiredData == DataToLoad.Everything)
        {
            // Copy the lessons to the Context
            _curriculum = tmpPlayerData.Curriculum;
            // If the user has not created any lessons, create a sample lesson to work with
            if (_curriculum.Lessons.Count == 0)
            {
                _curriculum.CreateSampleLessons();
            }
        }
        
        // If the game state should be loaded (ie the user is loading a saved game)...
        if(desiredData == DataToLoad.GameState || desiredData == DataToLoad.Everything) {
            // PLACEHOLDER: This code has not yet been implimented
        }
    }

    /// <summary>
    /// Update the Context in prepration for the next level
    /// </summary>
    public static void PrepareForNextLevel()
    {
        _word = null;
        _level++;
    }
    #endregion Helper Methods -------------------------------------------------
}
