using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    #region Fields

    public Texture backgroundTexture;
    private string instructionText = "Instructions:\nPress [<< LEFT] and [RIGHT >>] arrows to move.\nPress [ SPACEBAR ] to fire.";
    private int buttonWidth = 200;
    private int buttonHeight = 50;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        GUI.Label(new Rect(10, 10, 600, 200), instructionText);

        if (Input.anyKeyDown)
            Application.LoadLevel(1);
    }

    #endregion
}
