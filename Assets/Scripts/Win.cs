using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
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

        if (Input.anyKeyDown)
        {
            Player.Score = 0;
            Player.Lives = 3;
            Player.Missed = 0;
            Application.LoadLevel(1);
        }
    }

    #endregion
}
