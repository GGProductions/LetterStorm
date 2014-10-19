using UnityEngine;
using System.Collections;

public class CollectedLetter : MonoBehaviour
{

    #region Private Variables ---------------------------------------------
    // Private variables representing name of the letter, and to store its count
    private string _name;
    private int _count;
    private bool _isEmpty;
    #endregion Private Variables ------------------------------------------

    #region Properties ----------------------------------------------------
    /// <summary>
    /// The name of the letter
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    /// <summary>
    /// The counter for the letter
    /// </summary>
    public int Count
    {
        get
        {
            return _count;
        }
        set
        {
            _count = value;
        }
    }

    /// <summary>
    /// Returns true if there is none of a letter
    /// </summary>
    public bool isEmpty
    {
        get
        {
            if (_count <= 0)
                return true;
            else
                return false;
        }
    }
    #endregion Properties -------------------------------------------------

    #region Constructors --------------------------------------------------
    /// <summary>
    /// Holds information for a collected letter such as which letter it represents
    /// and how many of the letter is collected
    /// </summary>
    /// <param name="name">The name of the letter user collected</param>
    public CollectedLetter(string name)
    {
        _name = name;
        _count = 0;
    }

    /// <summary>
    /// Holds information for a collected letter such as which letter it represents
    /// and how many of the letter is collected
    /// </summary>
    /// <param name="name">The name of the letter user collected</param>
    /// <param name="count">The count of the collected letter</param>
    public CollectedLetter(string name, int count)
    {
        _name = name;
        _count = count;
    }
    #endregion Constructors -----------------------------------------------
}
