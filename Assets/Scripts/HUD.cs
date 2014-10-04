using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private GUIText Lives;

    /// <summary>
    /// Method that updates HUD once every frame.
    /// </summary>
    void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 250, 20), "Score: ");
        GUI.Label(new Rect(10, 30, 250, 20), "Lives: " + Player.Lives.ToString());
        GUI.Label(new Rect(10, 50, 250, 20), "Letters Collected: ");
        GUI.Label(new Rect(10, 70, 250, 20), "Letters Needed: ");
    }
}
