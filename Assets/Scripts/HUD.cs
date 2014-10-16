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
    public GUIStyle SelectedLetterButtonStyle;
    private string SelectedLetter = "";

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
        //SelectedLetterButtonStyle.normal.background = SelectedLetterButtonColor;
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

        DisplayInventoryWindow();
        
    }

    /// <summary>
    /// Method that updates HUD once every frame
    /// Displays game, player information, and player inventory
    /// </summary>
    void DisplayInventoryWindow()
    {

        GUILayout.BeginArea(new Rect(
            Screen.width / 2 - InventoryItemBoxWidth * 31 / 2,                          // X start position
            Screen.height - InventoryItemBoxHeight * 3 - InventoryBoxBottomMargin,      // Y start position
            InventoryItemBoxWidth * 31,                                                 // Width
            InventoryItemBoxHeight * 3));                                               // Height

        #region Letter Type in Inventory --------------------------------------------
        GUILayout.BeginHorizontal();

        GUI.color = DefaultLetterButtonColor;

        // A --------------------------------------------
        GUI.color = SelectedLetter == "A" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("A", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "A";
            GUI.color = DefaultLetterButtonColor;
        }

        // B --------------------------------------------
        GUI.color = SelectedLetter == "B" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("B", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "B";
            GUI.color = DefaultLetterButtonColor;
        }

        // C --------------------------------------------
        GUI.color = SelectedLetter == "C" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("C", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "C";
            GUI.color = DefaultLetterButtonColor;
        }

        // D --------------------------------------------
        GUI.color = SelectedLetter == "D" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("D", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "D";
            GUI.color = DefaultLetterButtonColor;
        }

        // E --------------------------------------------
        GUI.color = SelectedLetter == "E" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("E", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "E";
            GUI.color = DefaultLetterButtonColor;
        }

        // F --------------------------------------------
        GUI.color = SelectedLetter == "F" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("F", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "F";
            GUI.color = DefaultLetterButtonColor;
        }

        // G --------------------------------------------
        GUI.color = SelectedLetter == "G" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("G", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "G";
            GUI.color = DefaultLetterButtonColor;
        }

        // H --------------------------------------------
        GUI.color = SelectedLetter == "H" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("H", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "H";
            GUI.color = DefaultLetterButtonColor;
        }

        // I --------------------------------------------
        GUI.color = SelectedLetter == "I" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("I", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "I";
            GUI.color = DefaultLetterButtonColor;
        }

        // J --------------------------------------------
        GUI.color = SelectedLetter == "J" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("J", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "J";
            GUI.color = DefaultLetterButtonColor;
        }

        // K --------------------------------------------
        GUI.color = SelectedLetter == "K" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("K", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "K";
            GUI.color = DefaultLetterButtonColor;
        }

        // L --------------------------------------------
        GUI.color = SelectedLetter == "L" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("L", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "L";
            GUI.color = DefaultLetterButtonColor;
        }

        // M --------------------------------------------
        GUI.color = SelectedLetter == "M" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("M", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "M";
            GUI.color = DefaultLetterButtonColor;
        }

        // N --------------------------------------------
        GUI.color = SelectedLetter == "N" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("N", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "N";
            GUI.color = DefaultLetterButtonColor;
        }


        // O --------------------------------------------
        GUI.color = SelectedLetter == "O" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("O", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "O";
            GUI.color = DefaultLetterButtonColor;
        }

        // P --------------------------------------------
        GUI.color = SelectedLetter == "P" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("P", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "P";
            GUI.color = DefaultLetterButtonColor;
        }

        // Q --------------------------------------------
        GUI.color = SelectedLetter == "Q" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("Q", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "Q";
            GUI.color = DefaultLetterButtonColor;
        }

        // R --------------------------------------------
        GUI.color = SelectedLetter == "R" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("R", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "R";
            GUI.color = DefaultLetterButtonColor;
        }

        // S --------------------------------------------
        GUI.color = SelectedLetter == "S" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("S", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "S";
            GUI.color = DefaultLetterButtonColor;
        }

        // T --------------------------------------------
        GUI.color = SelectedLetter == "T" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("T", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "T";
            GUI.color = DefaultLetterButtonColor;
        }

        // U --------------------------------------------
        GUI.color = SelectedLetter == "U" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("U", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "U";
            GUI.color = DefaultLetterButtonColor;
        }


        // V --------------------------------------------
        GUI.color = SelectedLetter == "V" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("V", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "V";
            GUI.color = DefaultLetterButtonColor;
        }

        // W --------------------------------------------
        GUI.color = SelectedLetter == "W" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("W", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "W";
            GUI.color = DefaultLetterButtonColor;
        }

        // X --------------------------------------------
        GUI.color = SelectedLetter == "X" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("X", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "X";
            GUI.color = DefaultLetterButtonColor;
        }

        // Y --------------------------------------------
        GUI.color = SelectedLetter == "Y" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("Y", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "Y";
            GUI.color = DefaultLetterButtonColor;
        }

        // Z --------------------------------------------
        GUI.color = SelectedLetter == "Z" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        if (GUILayout.Button("Z", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth)))
        {
            SelectedLetter = "Z";
            GUI.color = DefaultLetterButtonColor;
        }

        GUILayout.EndHorizontal();
        #endregion Letter Type in Inventory -----------------------------------------

        #region Letters' Count in Inventory -----------------------------------------
        GUILayout.BeginHorizontal();

        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "A" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.A.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "B" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.B.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "C" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.C.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "D" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.D.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "E" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.E.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "F" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.F.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "G" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.G.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "H" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.H.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "I" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.I.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "J" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.J.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "K" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.K.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "L" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.L.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "M" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.M.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "N" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.N.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "O" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.O.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "P" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.P.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "Q" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.Q.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "R" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.R.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "S" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.S.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "T" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.T.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "U" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.U.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "V" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.V.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "W" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.W.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "X" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.X.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "Y" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.Y.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

        GUI.color = SelectedLetter == "Z" ? SelectedLetterButtonColor : DefaultLetterButtonColor;
        GUILayout.Button(Context.PlayerInventory.Z.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUI.color = DefaultLetterButtonColor;

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
