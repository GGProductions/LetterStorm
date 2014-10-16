using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour
{
    #region Fields

    public Texture backgroundTexture;

    private int buttonWidth = 200;
    private int buttonHeight = 50;
    #endregion

    #region Properties

    #endregion

    #region Functions
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        if(GUI.Button(new Rect(70, Screen.height / 2 + 25, 200, 20), "Insert Coin, to continue playing"))
        {
            Application.LoadLevel(2);
            Player.Lives = 3;
        }
        
        if(GUI.Button(new Rect(123, Screen.height / 2 + 50, 90, 20), "Restart Level"))
        {
            Application.LoadLevel(2);
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
        }
        
        if(GUI.Button(new Rect(130, Screen.height / 2 + 75, 75, 20), "Main Menu"))
        {
            Application.LoadLevel(1);
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
        }
    }

    #endregion
}
