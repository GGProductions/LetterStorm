using UnityEngine;
using System.Collections;

public class Context : MonoBehaviour {

    // Static variables to track in-game information

    // Tracks player lives
    public static int PlayerLives;

    // Player inventory
    public static Inventory Inventory;

    // Stores the alphabet for efficient coding
    public static char[] Alphabet;

    /// <summary>
    /// Method that runs only once in the beginning
    /// </summary>
    void Start()
    {
        PlayerLives = 3;
        Inventory = new Inventory();
        Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    }
}
