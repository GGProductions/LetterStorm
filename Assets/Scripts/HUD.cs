using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    #region Private Variables ---------------------------------------------
    // Private variables representing components of the HUD; Initialized in the Start() functions
    private GUIStyle InventoryStyle;
    private int InventoryBoxWidth;
    private int InventoryItemBoxWidth;
    private int InventoryItemBoxHeight;
    private int InventoryBoxBottomMargin;
    private float InventoryLetterFontSize;

    private Color DefaultLetterButtonColor;
    private Color SelectedLetterButtonColor;
    private static ArrayList CurrentLettersInInventory;
    private char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    // If game is paused or not paused
    private bool isPaused;
    #endregion Private Variables ------------------------------------------

    /// <summary>
    /// Method that runs only when HUD starts up
    /// </summary>
    void Start()
    {
        isPaused = false;
        InventoryStyle = new GUIStyle();
        DefaultLetterButtonColor = GUI.backgroundColor;

        CurrentLettersInInventory = new ArrayList();
        SelectedLetterButtonColor = Color.green;

        InventoryBoxWidth = (int)(Screen.width * .75);
        InventoryItemBoxWidth = (int)(Screen.width / 40);
        InventoryItemBoxHeight = (int)(Screen.width / 40);
        InventoryLetterFontSize = (float)(Screen.height * 0.0255f);
        InventoryBoxBottomMargin = 5;
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void OnGUI()
    {
        GUILayout.Box("Lives: " + Context.PlayerLives.ToString());
        GUILayout.Box("Letters Collected: " + Context.PlayerInventory.TotalCollectedLetters);
        GUILayout.Box("Test Screen Width: " + Screen.width.ToString());
        GUILayout.Box("Test Screen Height: " + Screen.height.ToString());
        GUILayout.Box("Hint: " + Context.Word.Hint);

        DisplayInventoryWindow();
        
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void DisplayInventoryWindow()
    {
        // Determine size of inventory "boxes"
        if (Screen.width <= 1000)
        {
            InventoryItemBoxWidth = (int)(Screen.width / 25);
            InventoryItemBoxHeight = (int)(Screen.width / 25);
        }
        else
        {
            InventoryItemBoxWidth = (int)(Screen.width / 40);
            InventoryItemBoxHeight = (int)(Screen.width / 40);
        }

        // Font size of letters in inventory boxes
        InventoryLetterFontSize = InventoryItemBoxHeight * 0.57f;

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

        // Define inventory box area
        GUILayout.BeginArea(new Rect(
            Screen.width / 2 - InventoryItemBoxWidth * 31 / 2,                          // X start position
            Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin,      // Y start position
            InventoryItemBoxWidth * 31,                                                 // Width
            InventoryItemBoxHeight * 3));                                               // Height

        // Draw collected letters in Inventory
        #region Letter Type in Inventory --------------------------------------------
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

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

        // Draw the inventory [count of each letter] that has been collected
        #region Letters' Count in Inventory -----------------------------------------
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
    /// Method that runs once every frame
    /// Control for keypresses and inventory
    /// </summary>
    void Update()
    {
        // If [Esc] is pressed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // Unpause
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                // Pause
                Time.timeScale = 0;
                isPaused = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Context.PlayerInventory.AddCollectedLetter("A");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Context.PlayerInventory.SubtractCollectedLetter("A");
        }
    }
}
