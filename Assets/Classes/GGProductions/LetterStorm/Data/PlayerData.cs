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
        private int _lifeCount;
        private int _level;
        private int _currentScore;
        private int _enemyDifficultyId;
        private Guid _currentLessonId;
        #endregion Private Variables ------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// All the Lessons for a specific user
        /// </summary>
        public LessonBook Curriculum
        {
            get { return _curriculum; }
            set { _curriculum = value; }
        }

        /// <summary>
        /// The number of lives the user has
        /// </summary>
        public int LifeCount
        {
            get { return _lifeCount; }
            set { _lifeCount = value; }
        }

        /// <summary>
        /// The number of the level the data was saved on
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// The score the user has earned between starting a new game and saving (vs his high score)
        /// </summary>
        public int CurrentScore
        {
            get { return _currentScore; }
            set { _currentScore = value; }
        }

        /// <summary>
        /// The id of the enemy difficulty level chosen for the game playthrough
        /// </summary>
        public int EnemyDifficultyId
        {
            get { return _enemyDifficultyId; }
            set { _enemyDifficultyId = value; }
        }

        /// <summary>
        /// The id of the lesson chosen for the game playthrough
        /// </summary>
        public Guid CurrentLessonId
        {
            get { return _currentLessonId; }
            set { _currentLessonId = value; }
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
