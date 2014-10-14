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
    private int InventoryBoxSideMargin = 5;

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
        GUILayout.Button("A", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("B", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("C", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("D", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("E", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("F", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("G", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("H", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("I", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("J", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("K", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("L", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("M", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("N", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("O", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("P", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("Q", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("R", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("S", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("T", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("U", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("V", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("W", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("X", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("Y", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button("Z", GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.EndHorizontal();
        #endregion Letter Type in Inventory -----------------------------------------

        #region Letters' Count in Inventory -----------------------------------------
        GUILayout.BeginHorizontal();
        GUILayout.Button(Context.PlayerInventory.A.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.B.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.C.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.D.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.E.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.F.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.G.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.H.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.I.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.J.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.K.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.L.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.M.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.N.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.O.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.P.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.Q.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.R.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.S.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.T.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.U.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.V.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.W.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.X.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.Y.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
        GUILayout.Button(Context.PlayerInventory.Z.Count.ToString(), GUILayout.Height(InventoryItemBoxHeight), GUILayout.Width(InventoryItemBoxWidth));
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
