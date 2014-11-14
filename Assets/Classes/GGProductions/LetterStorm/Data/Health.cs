using UnityEngine;
using System.Collections;
using System;
using GGProductions.LetterStorm.Configuration;
using GGProductions.LetterStorm.Configuration.Collections;

public class Health
{
    #region Private Variables ---------------------------------------------
    private int? _minHealth = null;
    private int? _maxHealth = null;
    private int? _curHealth = null;
    private int _decreaseFactor = 10;
    #endregion Private Variables ------------------------------------------

    #region Properties ----------------------------------------------------
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
    #endregion Properties -------------------------------------------------
    
    /// <summary>
    /// Object use to control a character's health
    /// </summary>
    /// <param name="difficulty">The difficulty level associated with the current playthrough.  
    /// This is used to populate the maximum health and the amount of damage taken per hit.</param>
    public Health(EnemyDifficulty difficulty)
    {
        InitializeHealth(difficulty);
    }

    /// <summary>
    /// Initializes the current and maximum health of player, as well as the amount of damage he'll take per hit
    /// </summary>
    /// <param name="difficulty">The difficulty level associated with the current playthrough.  
    /// This is used to populate the maximum health and the amount of damage taken per hit.</param>
    public void InitializeHealth(EnemyDifficulty difficulty)
    {
        _curHealth = _maxHealth = difficulty.InitialHealth;
        _decreaseFactor = difficulty.DamageToPlayerPerHit;
    }

    #region Methods -------------------------------------------------------
    /// <summary>
    /// Decreases current player health by the default decrease factor. Player health cannot be below the specified minimum.
    /// </summary>
    public void DecreaseHealth()
    {
        DecreaseHealth(_decreaseFactor);
    }
    
    /// <summary>
    /// Decreases current player health by an integer. Player health cannot be below the specified minimum.
    /// </summary>
    /// <param name="decreaseFactor">The amount to decrease current health by</param>
    public void DecreaseHealth(int decreaseFactor)
    {
        _curHealth = (_curHealth - decreaseFactor <= _minHealth) ? _minHealth : (_curHealth - decreaseFactor);
    }

    /// <summary>
    /// Increases current player health by an integer. Player health cannot be above the specified maximum.
    /// </summary>
    /// <param name="increaseFactor">The amount to increase current health by</param>
    public void IncreaseHealth(int increaseFactor)
    {
        _curHealth = (_curHealth + increaseFactor > _maxHealth) ? _maxHealth : (_curHealth + increaseFactor);
    }

    /// <summary>
    /// Returns true if current player health is the minimum (minimum is default 0).
    /// </summary>
    public bool HasNoHealth()
    {
        return (_curHealth <= _minHealth);
    }

    /// <summary>
    /// Returns true if current player health is above the minimum (minimum is default 0).
    /// </summary>
    public bool HasHealth()
    {
        return (_curHealth > _minHealth);
    }
    #endregion Methods ----------------------------------------------------
}
