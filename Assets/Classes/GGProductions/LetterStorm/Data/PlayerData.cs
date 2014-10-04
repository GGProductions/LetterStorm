using System;

using GGProductions.LetterStorm.Data.Collections;
using UnityEngine;

namespace GGProductions.LetterStorm.Data
{
    /// <summary>
    /// Represents all data that can be saved and loaded
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        #region Private Variables ---------------------------------------------
        // Create the private variables that the this class's 
        //  properties will use to store their data
        private LessonBook _curriculum;
        #endregion Private Variables ------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// Represents all the Lessons for a specific user
        /// </summary>
        public LessonBook Curriculum
        {
            get
            {
                return _curriculum;
            }
            set
            {
                _curriculum = value;
            }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents all data that can be saved and loaded
        /// </summary>
        public PlayerData()
        {
            _curriculum = new LessonBook();
        }
        #endregion Constructors -----------------------------------------------
    }
}
