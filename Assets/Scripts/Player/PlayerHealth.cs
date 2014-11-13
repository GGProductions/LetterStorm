using UnityEngine;
using System.Collections;

public class PlayerHealth
{

    #region Private Variables ---------------------------------------------
    private int? _minHealth = null;
    private int? _maxHealth = null;
    private int? _curHealth = null;

    /// <summary>
    /// The amount of the minimum health of player
    /// </summary>
    public int? MinHealth
    {
        get 
        {
            if (_minHealth == null)
            {
                _minHealth = 0;
            }
            return (int)_minHealth;
        }
        set { _minHealth = value; }
    }

    /// <summary>
    /// The amount of the maximum health of player
    /// </summary>
    public int? MaxHealth
    {
        get {
            if (_maxHealth == null)
            {
                _maxHealth = 100;
            }
            return (int)_maxHealth; 
        }
        set { _maxHealth = value; }
    }

    /// <summary>
    /// The amount of the current health of player
    /// </summary>
    public int? CurHealth
    {
        get {

            if (_curHealth == null)
            {
                _curHealth = MaxHealth;
            }
            return (int)_curHealth; 
        
        }
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
