using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    #region Private Variables ---------------------------------------------
    // Private variables representing components of the HUD
    private GUIText LabelLives;
    GUIStyle InventoryStyle;
    private int InventoryBoxWidth = (int)(Screen.width * .75);
    private int InventoryItemBoxWidth = 25;
    private int InventoryItemBoxHeight = 20;
    private int InventoryBoxBottomMargin = 5;
    //private int InventoryBoxSideMargin = 5;

    private Color DefaultLetterButtonColor;
    private Color SelectedLetterButtonColor = Color.green;
    private string SelectedLetter = "";
    private static ArrayList CurrentLettersInInventory = new ArrayList();
    char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

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
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void OnGUI()
    {
        GUILayout.Box("Lives: " + Context.PlayerLives.ToString());
        GUILayout.Box("Letters Collected: " + Context.PlayerInventory.TotalCollectedLetters);
        GUILayout.Box("Letters Needed: ");
        GUILayout.Box("Hint: " + Context.BossWordHint.ToString());

        DisplayInventoryWindow();
        
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void DisplayInventoryWindow()
    {

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

        GUILayout.BeginArea(new Rect(
            Screen.width / 2 - InventoryItemBoxWidth * 31 / 2,                          // X start position
            Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin,      // Y start position
            InventoryItemBoxWidth * 31,                                                 // Width
            InventoryItemBoxHeight * 3));                                               // Height

        #region Letter Type in Inventory --------------------------------------------
        GUILayout.BeginHorizontal();

        GUI.color = DefaultLetterButtonColor;

        // Create an entry in the inventory for each letter the player has collected
        for (int ii = 0; ii < CurrentLettersInInventory.Count; ii++)
        {
            GUI.color = SelectedLetter == CurrentLettersInInventory[ii].ToString() ? SelectedLetterButtonColor : DefaultLetterButtonColor;
            if (GUILayout.Button(CurrentLettersInInventory[ii].ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
            {
                SelectedLetter = CurrentLettersInInventory[ii].ToString();
                GUI.color = DefaultLetterButtonColor;
            }
        }
        GUILayout.EndHorizontal();
        #endregion Letter Type in Inventory -----------------------------------------

        #region Letters' Count in Inventory -----------------------------------------
        GUILayout.BeginHorizontal();

        GUI.color = DefaultLetterButtonColor;

        // Draw the inventory [count of each letter] that has been collected
        for (int ii = 0; ii < CurrentLettersInInventory.Count; ii++)
        {
            string InventoryLetter = CurrentLettersInInventory[ii].ToString();

            // Change color of letter in inventory if it is selected
            GUI.color = SelectedLetter == CurrentLettersInInventory[ii].ToString() ? SelectedLetterButtonColor : DefaultLetterButtonColor;

            // Show count of letter
            if (InventoryLetter == "A")
                GUILayout.Button(Context.PlayerInventory.A.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "B")
                GUILayout.Button(Context.PlayerInventory.B.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "C")
                GUILayout.Button(Context.PlayerInventory.C.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "D")
                GUILayout.Button(Context.PlayerInventory.D.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "E")
                GUILayout.Button(Context.PlayerInventory.E.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "F")
                GUILayout.Button(Context.PlayerInventory.F.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "G")
                GUILayout.Button(Context.PlayerInventory.G.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "H")
                GUILayout.Button(Context.PlayerInventory.H.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "I")
                GUILayout.Button(Context.PlayerInventory.I.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "J")
                GUILayout.Button(Context.PlayerInventory.J.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "K")
                GUILayout.Button(Context.PlayerInventory.K.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "L")
                GUILayout.Button(Context.PlayerInventory.L.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "M")
                GUILayout.Button(Context.PlayerInventory.M.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "N")
                GUILayout.Button(Context.PlayerInventory.N.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "O")
                GUILayout.Button(Context.PlayerInventory.O.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "P")
                GUILayout.Button(Context.PlayerInventory.P.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Q")
                GUILayout.Button(Context.PlayerInventory.Q.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "R")
                GUILayout.Button(Context.PlayerInventory.R.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "S")
                GUILayout.Button(Context.PlayerInventory.S.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "T")
                GUILayout.Button(Context.PlayerInventory.T.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "U")
                GUILayout.Button(Context.PlayerInventory.U.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "V")
                GUILayout.Button(Context.PlayerInventory.V.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "W")
                GUILayout.Button(Context.PlayerInventory.W.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "X")
                GUILayout.Button(Context.PlayerInventory.X.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Y")
                GUILayout.Button(Context.PlayerInventory.Y.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
            else if (InventoryLetter == "Z")
                GUILayout.Button(Context.PlayerInventory.Z.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));


            // Reset to default color for the next button
            GUI.color = DefaultLetterButtonColor;
        }
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
