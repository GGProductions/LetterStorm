﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private GUIText LabelLives;

    /// <summary>
    /// Method that updates HUD once every frame.
    /// </summary>
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 20), "Score: ");
        GUI.Label(new Rect(10, 30, 250, 20), "Lives: " + Context.PlayerLives.ToString());
        GUI.Label(new Rect(10, 50, 250, 20), "Letters Collected: ");
        GUI.Label(new Rect(10, 70, 250, 20), "Letters Needed: ");

        GUILayout.Box("Lives: " + Context.PlayerLives.ToString());
        GUI.Box(new Rect(0, 0, 100, 100), "hi");
        GUILayout.Box("test");
    }
}
