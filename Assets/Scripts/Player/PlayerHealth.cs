using UnityEngine;
using System.Collections;

public class PlayerHealth
{

    #region Private Variables ---------------------------------------------
    private int _minHealth = 0;
    private int _maxHealth = 100;
    private int _curHealth = 100;

    /// <summary>
    /// The amount of the minimum health of player
    /// </summary>
    public int MinHealth
    {
        get { return _minHealth; }
        set { _minHealth = value; }
    }

    /// <summary>
    /// The amount of the maximum health of player
    /// </summary>
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    /// <summary>
    /// The amount of the current health of player
    /// </summary>
    public int CurHealth
    {
        get { return _curHealth; }
        set { _curHealth = value; }
    }
    #endregion Private Variables ------------------------------------------

    #region Methods -------------------------------------------------------
    /// <summary>
    /// Decreases current player health by an integer. Player health cannot be below the specified minimum.
    /// </summary>
    /// <param name="decreaseFactor">The amount to decrease current health by</param>
    public void decreaseHealth(int decreaseFactor)
    {
        _curHealth = _curHealth - decreaseFactor <= _minHealth ? _minHealth : _curHealth - decreaseFactor;
    }

    /// <summary>
    /// Increases current player health by an integer. Player health cannot be above the specified maximum.
    /// </summary>
    /// <param name="increaseFactor">The amount to increase current health by</param>
    public void increaseHealth(int increaseFactor)
    {
        _curHealth = _curHealth + increaseFactor > _maxHealth ? _maxHealth : _curHealth + increaseFactor;
    }

    /// <summary>
    /// Returns true if current player health is the minimum (minimum is default 0).
    /// </summary>
    public bool hasNoHealth()
    {
        return _curHealth <= _minHealth ? true : false;
    }

    /// <summary>
    /// Returns true if current player health is above the minimum (minimum is default 0).
    /// </summary>
    public bool hasHealth()
    {
        return _curHealth > _minHealth ? true : false;
    }
    #endregion Methods ----------------------------------------------------
}
