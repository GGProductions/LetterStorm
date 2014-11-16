using UnityEngine;
using System.Collections;
using System;

public class PowerUp
{
    #region Private Variables ---------------------------------------------
    private string _name;
    private int _count;
    private bool _isEmpty;
    private DateTime _expireAt;
    #endregion Private Variables ------------------------------------------

    #region Properties ----------------------------------------------------
    /// <summary>
    /// The name of the power-up
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
    /// The counter for the power-up
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
    /// Returns true if there is none of a power-up
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

    /// <summary>
    /// The time and date the powerup should expire
    /// </summary>
    public DateTime ExpireAt
    {
        get { return _expireAt; }
        set { _expireAt = value; }
    }
    #endregion Properties -------------------------------------------------

    #region Constructors --------------------------------------------------
    /// <summary>
    /// Holds information for a collected letter such as which letter it represents
    /// and how many of the letter is collected
    /// </summary>
    /// <param name="name">The name of the letter user collected</param>
    public PowerUp(string name)
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
    public PowerUp(string name, int count)
    {
        _name = name;
        _count = count;
    }
    #endregion Constructors -----------------------------------------------

    /// <summary>
    /// Increments the count of a power-up
    /// </summary>
    public void IncrementCount()
    {
        _count++;
    }

    /// <summary>
    /// Decrements the count of a power-up
    /// </summary>
    public void DecrementCount()
    {
        if (_count - 1 < 0)
            _count = 0;
        else
            _count--;
    }

    /// <summary>
    /// Check if the powerup has expired
    /// </summary>
    public bool HasExpired()
    {
        if (_expireAt != null && _expireAt != DateTime.MinValue)
        {
            return (_expireAt < DateTime.Now);
        }
        else 
            return true;
    }

    /// <summary>
    /// Set the time at which the powerup should expire
    /// </summary>
    /// <param name="secondsFromNow">The number of seconds from now the powerup should expire</param>
    public void SetExpireTime(int secondsFromNow)
    {
        _expireAt = DateTime.Now.AddSeconds(secondsFromNow);
    }
}
