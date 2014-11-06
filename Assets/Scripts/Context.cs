﻿using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.Data.Collections;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Utilities;
using System;
using GGProductions.LetterStorm.Configuration;

public class Context : MonoBehaviour
{

    #region Private Variables ---------------------------------------------
    // Create the private variables that the this class's 
    //  properties will use to store their data
    private static LessonBook _curriculum;
    private static int _playerLives;
    private static Inventory _playerInventory;
    private static string _selectedLetter;
    private static char[] _alphabet;
    private static Word _word;
    private static int _level = 1;
    private static EnemyDifficulty _enemyDifficulty;
    private static Guid _currentLessonId;
    #endregion Private Variables ------------------------------------------

    #region Properties ----------------------------------------------------
    /// <summary>Stores the alphabet for efficient coding</summary>

    public static int LevelNum
    {
        get
        {
            return _level;
        }
    }
    public static Word Word
    {
        get 
        {
            if (_word == null)
            {
                if (_currentLessonId == null)
                    _word = Curriculum.Lessons[0].Words.GetRandomWord();
                else
                    _word = Curriculum.Lessons.GetLessonById(_currentLessonId).Words.GetRandomWord();
            }

            return _word; 
        }
    }
    public static char[] Alphabet
    {
        get { return _alphabet; }
        set { _alphabet = value; }
    }
    /// <summary>Represents all the Lessons for a specific user</summary>
    public static LessonBook Curriculum
    {
        get { return _curriculum; }
        set { _curriculum = value; }
    }

    /// <summary>Player inventory</summary>
    public static Inventory PlayerInventory
    {
        get { return _playerInventory; }
        set { _playerInventory = value; }
    }

    /// <summary>Selected letter in inventory</summary>
    public static string SelectedLetter
    {
        get { return _selectedLetter; }
        set { _selectedLetter = value; }
    }

    /// <summary>Tracks player lives</summary>
    public static int PlayerLives
    {
        get { return _playerLives; }
        set { _playerLives = value; }
    }

    /// <summary>The enemy difficulty level chosen for the game playthrough</summary>
    public static EnemyDifficulty EnemyDifficulty
    {
        get { return _enemyDifficulty; }
        set { _enemyDifficulty = value; }
    }

    /// <summary>The id of the lesson chosen for the game playthrough</summary>
    public static Guid CurrentLessonId
    {
        get { return _currentLessonId; }
        set { _currentLessonId = value; }
    }
    #endregion Properties -------------------------------------------------

    #region Event Handlers ----------------------------------------------------
    /// <summary>
    /// Method that runs only once in the beginning
    /// </summary>
    void Awake()
    {
        PlayerLives = 3;
        PlayerInventory = new Inventory();
        Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        // Populate the Context from the save file
        LoadDataFromSaveFile(false);
    }
    #endregion Event Handlers -------------------------------------------------

    #region Helper Methods ----------------------------------------------------
    /// <summary>
    /// Populate the Context from the save file
    /// </summary>
    /// <param name="loadGameState">
    /// True=Load both the stateless data and the game state from the save file (ie the user is loading a saved game); 
    /// False=Load only the stateless data from the save file (ie the user is starting a new game)
    /// </param>
    private void LoadDataFromSaveFile(bool loadGameState)
    {
        // Retrieve all data from the save file
        PlayerData tmpPlayerData = GameStateUtilities.Load();

        // Copy the lessons to the Context
        _curriculum = tmpPlayerData.Curriculum;
        // If the user has not created any lessons, create a sample lesson to work with
        if (_curriculum.Lessons.Count == 0)
        {
            _curriculum.CreateSampleLessons();
        }

        // If the game state should be loaded (ie the user is loading a saved game)...
        if (loadGameState)
        {
            // PLACEHOLDER: This code has not yet been implimented
        }
    }

    public static void PrepareForNextLevel()
    {
        _word = null;
        _level++;
    }
    #endregion Helper Methods -------------------------------------------------
}
