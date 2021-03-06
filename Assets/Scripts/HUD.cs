﻿using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.Utilities;
using GGProductions.LetterStorm.Data;

public class HUD : MonoBehaviour {

    #region Private Variables ---------------------------------------------
    // Private variables representing components of the HUD; Initialized in the Start() functions

    // Inventory sizes
    private int InventoryBoxWidth;
    private int InventoryItemBoxWidth;
    private int InventoryItemBoxHeight;
    private int InventoryBoxBottomMargin;
    private float InventoryLetterFontSize;
    private int InventoryX;
    private int InventoryY;
    private int InventoryWidth;
    private int InventoryHeight;
    private int InventoryBackgroundX;
    private int InventoryBackgroundY;
    private int InventoryBackgroundWidth;
    private int InventoryBackgroundHeight;

    private Color DefaultLetterButtonColor;
    private Color SelectedLetterButtonColor;
    private static ArrayList CurrentLettersInInventory;
    private char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    // Pause Menu Textures
    public Texture2D ResumeGameButtonTexture;
    public Texture2D HowToPlayGameButtonTexture;
    public Texture2D QuitGameButtonTexture;
    public Texture2D MainMenuGameButtonTexture;
    public Texture2D SettingsGameButtonTexture;
    public Texture2D SaveGameButtonTexture;

    // How to Play Textures
    public Texture2D HowToPlayTexture1;
    private float HowToPlayTexture1Width;
    private float HowToPlayTexture1Height;

    // Pause Menu Background Texture
    public Texture2D CorkBoardTexture;
    private float CorkBoardBorderSize;
    private float CorkBoardWidth;
    private float CorkBoardHeight;
    private float CorkBoardDivisionSizeWidth;
    private float CorkBoardDivisionSizeHeight;

    // HP Bar dimensions
    private float HPBarX;
    private float HPBarY;
    private float HPBarWidth;
    private float HPBarHeight;

    // Scale factors for screen resize
    private float scaleFactorPauseMenuButtons;

    // Useful styles
    private GUIStyle emptyStyle = new GUIStyle();   // Null style, for transparent backgrounds
    private GUIStyle pauseMenuButtonsStyle = new GUIStyle();
    public GUIStyle hintStyle;
    public GUIStyle HPBarStyle = new GUIStyle();
    public GUIStyle ScoreBarStyle = new GUIStyle();
    public GUIStyle InventoryBackgroundStyle = new GUIStyle();
    
    // Player health variables
    private int CurrentHealth;
    private int MaximumHealth;
    private int MinimumHealth;
    private float HealthBarLength;

    // If game is paused or not paused
    private bool isPaused;
    private bool isPlayingBGM;
    private bool isInHowToPlayMenu;

    // Scrolling hint text variables
    private string hintText;
    private Rect hintTextRectangle;

    #endregion Private Variables ------------------------------------------

    /// <summary>
    /// Method that runs only when HUD starts up
    /// </summary>
    void Start()
    {
        isPaused = false;
        isPlayingBGM = true;
        isInHowToPlayMenu = false;
        Time.timeScale = 1;
        DefaultLetterButtonColor = GUI.backgroundColor;

        CurrentLettersInInventory = new ArrayList();
        SelectedLetterButtonColor = Color.green;
        
        // Scaling factors
        scaleFactorPauseMenuButtons = 1;

        // Obtain player health
        UpdatePlayerStats();

        // Dimensions - Inventory
        InventoryBoxWidth = (int)(Screen.width * .75);
        InventoryItemBoxWidth = (int)(Screen.width / 40);
        InventoryItemBoxHeight = (int)(Screen.width / 40);
        InventoryLetterFontSize = (float)(Screen.height * 0.0255f);
        InventoryBoxBottomMargin = 5;

        InventoryX = Screen.width / 2 - InventoryItemBoxWidth * 31 / 2;
        InventoryY = Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin;
        InventoryWidth = InventoryItemBoxWidth * 31;
        InventoryHeight = InventoryItemBoxHeight * 3;

        InventoryBackgroundX = Screen.width / 2 - InventoryItemBoxWidth * 18 / 2;
        InventoryBackgroundY = Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin - 10;
        InventoryBackgroundWidth = InventoryItemBoxWidth * 18;
        InventoryBackgroundHeight = InventoryItemBoxHeight * 3;

        // Dimensions - CorkBoard for pause menu
        CorkBoardWidth = CorkBoardTexture.width;//
        CorkBoardHeight = CorkBoardTexture.height;
        CorkBoardBorderSize = CorkBoardWidth / 15;
        CorkBoardDivisionSizeWidth = (CorkBoardWidth - CorkBoardBorderSize * 4) / 3;
        CorkBoardDivisionSizeHeight = (CorkBoardHeight - CorkBoardBorderSize * 4) / 2;

        // Dimensions - HP bar
        HPBarX = InventoryBackgroundX + 10;
        HPBarY = InventoryBackgroundY - 20;
        HPBarWidth = Screen.width / 2;
        HPBarHeight = 20;

        // Dimensions - How To Play Menu
        HowToPlayTexture1Width = HowToPlayTexture1.width;
        HowToPlayTexture1Height = HowToPlayTexture1.height;
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void OnGUI()
    {
        DisplayHints();                     // Draw hints text
        DisplayHPBar();                     // Draw HP Bar
        DisplayScoreBar();                  // Draw Player's Score
        DisplayPauseMenu();                 // Draw Pause Menu when in pause mode
        DisplayInventoryWindow();           // Draw Inventory
    }

    /// <summary>
    /// Displays hints in HUD
    /// </summary>
    public void DisplayHints()
    {
        SetHintTextScrollingBox();
        hintStyle.normal.textColor = Color.blue;
        GUI.Label(hintTextRectangle, hintText, hintStyle);
    }

    /// <summary>
    /// Displays HP bar in HUD
    /// </summary>
    public void DisplayHPBar()
    {
        GUI.TextField(new Rect(HPBarX, HPBarY, HPBarWidth, HPBarHeight), "");          // Bar's background
        HPBarStyle.alignment = TextAnchor.MiddleCenter;
        HPBarStyle.normal.textColor = Color.black;
        GUI.TextField(new Rect(HPBarX, HPBarY, HealthBarLength, HPBarHeight),
            "HP: " + CurrentHealth.ToString() + "/" + MaximumHealth.ToString(), HPBarStyle);
    }

    /// <summary>
    /// Displays Score bar in HUD
    /// </summary>
    public void DisplayScoreBar()
    {
        ScoreBarStyle.alignment = TextAnchor.MiddleCenter;
        ScoreBarStyle.normal.textColor = Color.black;
        GUI.skin.button.wordWrap = true;
        GUI.Box(new Rect(HPBarX + HPBarWidth + 5, HPBarY, InventoryBackgroundWidth - HPBarWidth, 20), "Score: " + Context.CurrentScore.Score, ScoreBarStyle);
    }

    /// <summary>
    /// Sets up the scrolling rectangular region for scrolling the hint text from right to left on top of the screen
    /// </summary>
    public void SetHintTextScrollingBox()
    {
        float scrollSpeed = 65;
        try
        {
            hintText = "Hint: " + Context.Word.Hint + " ";
        }
        // This exception will be thrown during the last frames of the last boss, 
        // in which case do nothing (since the user doesn't need the hint any more)
        catch (NoUntestedWordsException)
        {
            // Do nothing
        }

        if (hintTextRectangle.width == 0)
        {
            var dimensions = GUI.skin.label.CalcSize(new GUIContent(hintText));

            // Start message past the right side of the screen.
            hintTextRectangle.x = -dimensions.x;
            hintTextRectangle.y = dimensions.y + 20;
            hintTextRectangle.width = dimensions.x;
            hintTextRectangle.height = dimensions.y;
        }

        hintTextRectangle.x -= Time.deltaTime * scrollSpeed;

        // If message has moved past the right side, move it back to the left.
        /*if (hintTextRectangle.x > Screen.width)
        {
            hintTextRectangle.x = -hintTextRectangle.width;
        }*/

        // If message has moved past the left side, move it back to the right.
        if (hintTextRectangle.x + hintTextRectangle.width * 3 < 0)
            hintTextRectangle.x = Screen.width;
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    private void DisplayInventoryWindow()
    {

        #region Determine which letters to show in the inventory --------------------------------------------
        // Determine which letters to show in the inventory
        CurrentLettersInInventory.Clear();
        for (int ii = 0; ii < Alphabet.Length; ii++)
        {
            string letter = Alphabet[ii].ToString();
            bool letterNotInInventory = !CurrentLettersInInventory.Contains(letter);

            if (letterNotInInventory)
            {
                if (!Context.PlayerInventory.A.isEmpty && letter == "A") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.B.isEmpty && letter == "B") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.C.isEmpty && letter == "C") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.D.isEmpty && letter == "D") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.E.isEmpty && letter == "E") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.F.isEmpty && letter == "F") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.G.isEmpty && letter == "G") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.H.isEmpty && letter == "H") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.I.isEmpty && letter == "I") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.J.isEmpty && letter == "J") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.K.isEmpty && letter == "K") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.L.isEmpty && letter == "L") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.M.isEmpty && letter == "M") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.N.isEmpty && letter == "N") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.O.isEmpty && letter == "O") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.P.isEmpty && letter == "P") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.Q.isEmpty && letter == "Q") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.R.isEmpty && letter == "R") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.S.isEmpty && letter == "S") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.T.isEmpty && letter == "T") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.U.isEmpty && letter == "U") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.V.isEmpty && letter == "V") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.W.isEmpty && letter == "W") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.X.isEmpty && letter == "X") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.Y.isEmpty && letter == "Y") { CurrentLettersInInventory.Add(letter); continue; }
                else if (!Context.PlayerInventory.Z.isEmpty && letter == "Z") { CurrentLettersInInventory.Add(letter); continue; }
            }
        }
        #endregion Determine which letters to show in the inventory -----------------------------------------

        // Define inventory box area

        InventoryBackgroundStyle.alignment = TextAnchor.MiddleCenter;


        // Inventory's background/texture
        GUI.TextField(new Rect(
            InventoryBackgroundX,
            InventoryBackgroundY,
            InventoryBackgroundWidth,
            InventoryBackgroundHeight), "", InventoryBackgroundStyle);

        // Inventory dimensions         
        GUILayout.BeginArea(new Rect(
            InventoryX,                                                                 // X start position
            InventoryY,                                                                 // Y start position
            InventoryWidth,                                                             // Width
            InventoryHeight));                                                          // Height

        #region Letter in Inventory --------------------------------------------
        // Draw collected letters in Inventory

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.black;

        GUI.color = DefaultLetterButtonColor;

        for (int ii = 0; ii < CurrentLettersInInventory.Count; ii++)
        {
            string currentLetter = CurrentLettersInInventory[ii].ToString();
            GUI.color = Context.SelectedLetter == currentLetter ? SelectedLetterButtonColor : DefaultLetterButtonColor;
            if (GUILayout.Button(
                "<size=" + InventoryLetterFontSize + ">" + currentLetter + "</size>", 
                GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
            {
                Context.SelectedLetter = currentLetter;
                GUI.color = DefaultLetterButtonColor;
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        #endregion Letter Type in Inventory -----------------------------------------

        #region Letters' Count in Inventory -----------------------------------------
        // Draw the inventory [count of each letter] that has been collected

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUI.color = DefaultLetterButtonColor;

        for (int ii = 0; ii < CurrentLettersInInventory.Count; ii++)
        {
            string InventoryLetter = CurrentLettersInInventory[ii].ToString();

            // Change color of letter in inventory if it is selected
            GUI.color = Context.SelectedLetter == InventoryLetter ? SelectedLetterButtonColor : DefaultLetterButtonColor;

            // Show count of letter
            if (InventoryLetter == "A")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.A.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "B")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.B.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "C")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.C.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "D")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.D.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "E")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.E.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "F")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.F.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "G")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.G.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "H")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.H.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "I")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.I.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "J")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.J.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "K")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.K.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "L")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.L.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "M")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.M.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "N")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.N.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "O")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.O.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "P")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.P.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Q")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.Q.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "R")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.R.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "S")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.S.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "T")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.T.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "U")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.U.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "V")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.V.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "W")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.W.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "X")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.X.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Y")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.Y.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Z")
                GUILayout.Button(
                    "<size=" + InventoryLetterFontSize + ">" + Context.PlayerInventory.Z.Count.ToString() + "</size>", 
                    GUILayout.Height(InventoryItemBoxHeight), 
                    GUILayout.Width(InventoryItemBoxWidth));

            // Reset to default color for the next button
            GUI.color = DefaultLetterButtonColor;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        #endregion Letters' Count in Inventory --------------------------------------

        GUILayout.EndArea();

    }

    /// <summary>
    /// Method to draw the pause menu
    /// </summary>
    private void DisplayPauseMenu()
    {

        // Draw pause menu
        if (isPaused)
        {
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;

            // Draw pause menu background
            GUI.DrawTexture(new Rect(Screen.width / 2 - CorkBoardWidth / 2,
                Screen.height / 2 - CorkBoardHeight / 2, CorkBoardWidth, CorkBoardHeight), CorkBoardTexture, ScaleMode.ScaleToFit, true);

            if (!isInHowToPlayMenu)
            {
                // Draw pause menu buttons
                // Resume button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardBorderSize,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), ResumeGameButtonTexture, emptyStyle))
                {
                    isPaused = false;
                    isInHowToPlayMenu = false;
                }
                // How to play button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardDivisionSizeWidth + CorkBoardBorderSize * 2,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), HowToPlayGameButtonTexture, emptyStyle))
                {
                    isInHowToPlayMenu = true;
                }
                // Quit game button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardDivisionSizeWidth * 2 + CorkBoardBorderSize * 3,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), QuitGameButtonTexture, emptyStyle))
                {
                    //isPaused = false;
                    Application.Quit();
                }
                // Main menu button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardBorderSize,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2 + CorkBoardDivisionSizeHeight,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), MainMenuGameButtonTexture, emptyStyle))
                {
                    // Reset values and reload to Main Menu
                    //Context.PlayerHealth.CurHealth = Context.PlayerHealth.MaxHealth;
                    //Context.PlayerInventory = new Inventory();
                    Context.ClearStatsNextLevel();
                    isPaused = false;                                       // Unpause
                    Application.LoadLevel("MainMenu");
                }
                // Save game button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardDivisionSizeWidth + CorkBoardBorderSize * 2,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2 + CorkBoardDivisionSizeHeight,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), SaveGameButtonTexture, emptyStyle))
                {
                    SaveGamePreferences();
                }
                // Settings button
                if (GUI.Button(new Rect(Screen.width / 2 - CorkBoardWidth / 2 + CorkBoardDivisionSizeWidth * 2 + CorkBoardBorderSize * 3,
                    Screen.height / 2 - CorkBoardHeight / 2 + CorkBoardBorderSize * 2 + CorkBoardDivisionSizeHeight,
                    CorkBoardDivisionSizeWidth,
                    CorkBoardDivisionSizeHeight), SettingsGameButtonTexture, emptyStyle))
                {
                    isPlayingBGM = !isPlayingBGM;
                }
            }
            // How to Play Menu
            else if (isInHowToPlayMenu)
            {
                // How to play, Page 1 Instructions
                if (GUI.Button(new Rect(Screen.width / 2 - HowToPlayTexture1Width / 2,
                    Screen.height / 2 - HowToPlayTexture1Height / 2,
                    HowToPlayTexture1Width,
                    HowToPlayTexture1Height), HowToPlayTexture1, emptyStyle))
                {
                    isInHowToPlayMenu = false;
                }
            }

        }
    }

    /// <summary>
    /// Control for keypresses
    /// </summary>
    void Update()
    {
        // Always keep track of player health
        UpdatePlayerStats();

        // Determine scrolling hint text dimensions
        AdjustScrollingHintDimensions();

        // Determine size of inventory "boxes" depending on screen size
        AdjustInventoryDimensions();
        
        // Dimensions - HP bar
        AdjustHPBarDimensions();

        // Dimensions - CorkBoard for pause menu
        AdjustCorkboardDimensions();

        // Dimensions - How To Play Menu
        HowToPlayTexture1Width = HowToPlayTexture1.width * scaleFactorPauseMenuButtons;
        HowToPlayTexture1Height = HowToPlayTexture1.height * scaleFactorPauseMenuButtons;

        // Pause/Unpause game flow control
        PauseOrUnpauseGame();

        // Determine which letter is selected based on key presses
        SetSelectedLetterFromKeyPress();

        // If [Esc] is pressed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If paused already, unpause
            if (isPaused)
            {
                isPaused = false;
                isInHowToPlayMenu = false;
            }
            // If not paused, pause game
            else
            {
                isPaused = true;
            }
        }
    }

    /// <summary>
    /// Pause or Unpause the game
    /// </summary>
    private void PauseOrUnpauseGame()
    {
        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseOrUnpauseBGM();
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Pause or Unpause the background music
    /// </summary>
    private void PauseOrUnpauseBGM()
    {
        if (isPlayingBGM)
            AudioListener.pause = false;
        else
            AudioListener.pause = true;
    }

    /// <summary>
    /// Sets the player stats to local variables, from the Context global class, 
    /// and computes other stats or dimensions for displaying player stats
    /// </summary>
    private void UpdatePlayerStats()
    {
        // Always keep track of player health
        CurrentHealth = (int)Context.PlayerHealth.CurHealth;
        MinimumHealth = (int)Context.PlayerHealth.MinHealth;
        MaximumHealth = (int)Context.PlayerHealth.MaxHealth;
    }

    /// <summary>
    /// Determines dimensions for the scrolling hint text, such as font size, depending on screen size
    /// </summary>
    private void AdjustScrollingHintDimensions()
    {
        if (Screen.width <= 1000)
            hintStyle.fontSize = 25;
        else
            hintStyle.fontSize = 35;
    }

    /// <summary>
    /// Determines the dimensions for drawing components of the player's inventory
    /// </summary>
    private void AdjustInventoryDimensions()
    {
        if (Screen.width <= 1000)
        {
            InventoryItemBoxWidth = (int)(Screen.width / 25);
            InventoryItemBoxHeight = (int)(Screen.width / 25);
            scaleFactorPauseMenuButtons = 0.60f;
        }
        else
        {
            InventoryItemBoxWidth = (int)(Screen.width / 40);
            InventoryItemBoxHeight = (int)(Screen.width / 40);
            scaleFactorPauseMenuButtons = 1;
        }

        // Font size of letters in inventory boxes
        InventoryLetterFontSize = InventoryItemBoxHeight * 0.57f;

        // Inventory dimensions
        InventoryX = Screen.width / 2 - InventoryItemBoxWidth * 31 / 2;
        InventoryY = Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin;
        InventoryWidth = InventoryItemBoxWidth * 31;
        InventoryHeight = InventoryItemBoxHeight * 3;

        // Inventory background/texture dimensions
        InventoryBackgroundX = Screen.width / 2 - InventoryItemBoxWidth * 18 / 2;
        InventoryBackgroundY = Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin - 10;
        InventoryBackgroundWidth = InventoryItemBoxWidth * 18;
        InventoryBackgroundHeight = InventoryItemBoxHeight * 3;
    }

    /// <summary>
    /// Determines the dimensions for drawing components of the player's inventory
    /// </summary>
    private void AdjustCorkboardDimensions()
    {
        CorkBoardWidth = CorkBoardTexture.width * scaleFactorPauseMenuButtons;
        CorkBoardHeight = CorkBoardTexture.height * scaleFactorPauseMenuButtons;
        CorkBoardBorderSize = CorkBoardWidth / 15 * 0.95f;
        CorkBoardDivisionSizeWidth = (CorkBoardWidth - CorkBoardBorderSize * 4) / 3;
        CorkBoardDivisionSizeHeight = (CorkBoardHeight - CorkBoardBorderSize * 4) / 2;
    }

    /// <summary>
    /// Adjusts the HP bar dimensions based on screen sizes
    /// </summary>
    private void AdjustHPBarDimensions()
    {
        HPBarX = InventoryBackgroundX + 10;
        HPBarY = InventoryBackgroundY - 20;
        HPBarWidth = InventoryBackgroundWidth * 0.75f;
        HPBarHeight = 20;

        HealthBarLength = (float)(HPBarWidth * (float)((float)CurrentHealth / (float)MaximumHealth));
    }
    /// <summary>
    /// Set selected letter from inventory upon keypress
    /// </summary>
    private void SetSelectedLetterFromKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.A)) { Context.SelectedLetter = "A"; }
        if (Input.GetKeyDown(KeyCode.B)) { Context.SelectedLetter = "B"; }
        if (Input.GetKeyDown(KeyCode.C)) { Context.SelectedLetter = "C"; }
        if (Input.GetKeyDown(KeyCode.D)) { Context.SelectedLetter = "D"; }
        if (Input.GetKeyDown(KeyCode.E)) { Context.SelectedLetter = "E"; }
        if (Input.GetKeyDown(KeyCode.F)) { Context.SelectedLetter = "F"; }
        if (Input.GetKeyDown(KeyCode.G)) { Context.SelectedLetter = "G"; }
        if (Input.GetKeyDown(KeyCode.H)) { Context.SelectedLetter = "H"; }
        if (Input.GetKeyDown(KeyCode.I)) { Context.SelectedLetter = "I"; }
        if (Input.GetKeyDown(KeyCode.J)) { Context.SelectedLetter = "J"; }
        if (Input.GetKeyDown(KeyCode.K)) { Context.SelectedLetter = "K"; }
        if (Input.GetKeyDown(KeyCode.L)) { Context.SelectedLetter = "L"; }
        if (Input.GetKeyDown(KeyCode.M)) { Context.SelectedLetter = "M"; }
        if (Input.GetKeyDown(KeyCode.N)) { Context.SelectedLetter = "N"; }
        if (Input.GetKeyDown(KeyCode.O)) { Context.SelectedLetter = "O"; }
        if (Input.GetKeyDown(KeyCode.P)) { Context.SelectedLetter = "P"; }
        if (Input.GetKeyDown(KeyCode.Q)) { Context.SelectedLetter = "Q"; }
        if (Input.GetKeyDown(KeyCode.R)) { Context.SelectedLetter = "R"; }
        if (Input.GetKeyDown(KeyCode.S)) { Context.SelectedLetter = "S"; }
        if (Input.GetKeyDown(KeyCode.T)) { Context.SelectedLetter = "T"; }
        if (Input.GetKeyDown(KeyCode.U)) { Context.SelectedLetter = "U"; }
        if (Input.GetKeyDown(KeyCode.V)) { Context.SelectedLetter = "V"; }
        if (Input.GetKeyDown(KeyCode.W)) { Context.SelectedLetter = "W"; }
        if (Input.GetKeyDown(KeyCode.X)) { Context.SelectedLetter = "X"; }
        if (Input.GetKeyDown(KeyCode.Y)) { Context.SelectedLetter = "Y"; }
        if (Input.GetKeyDown(KeyCode.Z)) { Context.SelectedLetter = "Z"; }
    }

    /// <summary>
    /// Saves user's current game preferences, scores, and HP
    /// </summary>
    private void SaveGamePreferences()
    {
        PlayerData dataToSave = GameStateUtilities.Load();
        dataToSave.Curriculum = Context.Curriculum;
        dataToSave.CurrentLessonId = Context.CurrentLessonId;
        dataToSave.LifeCount = (int)Context.PlayerHealth.CurHealth;
        dataToSave.CurrentScore = Context.CurrentScore.Score;
        dataToSave.EnemyDifficultyId = Context.EnemyDifficulty.ID;
        GameStateUtilities.Save(dataToSave);
    }

}
