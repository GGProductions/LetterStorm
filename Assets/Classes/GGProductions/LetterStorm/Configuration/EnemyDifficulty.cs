using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGProductions.LetterStorm.Configuration
{
    /// <summary>
    /// Represents the various settings that affect the game's enemy difficulty
    /// </summary>
    public class EnemyDifficulty
    {
        #region Private Variables ---------------------------------------------
        // Private variables used to store the Properties' values
        private int _id;
        private string _name;
        private string _description;
        private float _gameSpeed = 1.0f;
        private int _initialHealth = 100;
        private int _damageToPlayerPerHit = 10;
        private int _maxEnemiesOnScreen = 5;
        private float _bossHealthHandicap = 1.0f;
        private bool _enableBossRageMode = true;
        #endregion Private Variables ------------------------------------------
        

        #region Properties ----------------------------------------------------
        /// <summary>The unique id of this difficulty level</summary>
        public int ID
        {
            get { return _id; }
        }
        
        /// <summary>The name of this difficulty level.</summary>
        /// <example>"Easy"</example>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>The description of this difficulty level.</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>The speed at which the screen scrolls and enemies move.  The default value is 1.0</summary>
        public float GameSpeed
        {
            get { return _gameSpeed; }
            set { _gameSpeed = value; }
        }

        /// <summary>The amount of health the player starts the first level with.</summary>
        public int InitialHealth
        {
            get { return _initialHealth; }
            set { _initialHealth = value; }
        }

        /// <summary>The amount of damage taken by the player when he is hit by a projectile or enemy</summary>
        public int DamageToPlayerPerHit
        {
            get { return _damageToPlayerPerHit; }
            set { _damageToPlayerPerHit = value; }
        }

        /// <summary>The (approximate) maximum number of enemies displayed on the screen simultaneously</summary>
        public int MaxEnemiesOnScreen 
        {
            get { return _maxEnemiesOnScreen; }
            set { _maxEnemiesOnScreen = value; }
        }

        /// <summary>
        /// The amount by which the boss's health should be decreased or increased.  This applies only to 
        /// parts of the boss that can be destroyed using traditional projectiles (vs letters).  
        /// The default value is 1.0
        /// </summary>
        public float BossHealthHandicap
        {
            get { return _bossHealthHandicap; }
            set { _bossHealthHandicap = value; }
        }

        /// <summary>Whether the boss should charge the user when he fires the wrong letter</summary>
        public Boolean EnableBossRageMode
        {
            get { return _enableBossRageMode; }
            set { _enableBossRageMode = value; }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents the various settings that affect the game's enemy difficulty
        /// </summary>
        /// <param name="id">The unique id to be used for this difficulty level</param>
        public EnemyDifficulty(int id)
        {
            _id = id;
        }

        /// <summary>
        /// Represents the various settings that affect the game's enemy difficulty
        /// </summary>
        /// <param name="id">The unique id to be used for this difficulty level</param>
        /// <param name="name">The name of this difficulty level</param>
        /// <param name="description">The description of this difficulty level</param>
        /// <param name="gameSpeed">The speed at which the screen scrolls and enemies move.  The default value is 1.0</param>
        /// <param name="initialHealth">The amount of health the player starts the first level with</param>
        /// <param name="damageToPlayerPerHit">The amount of damage taken by the player when he is hit by a projectile or enemy</param>
        /// <param name="maxEnemiesonScreen">The (approximate) maximum number of enemies displayed on the screen simultaneously</param>
        /// <param name="bossHealthHandicap">
        /// The amount by which the boss's health should be decreased or increased.  
        /// This applies only to parts of the boss that can be destroyed using traditional projectiles (vs letters).  
        /// The default value is 1.0
        /// </param>
        /// <param name="enableBossRageMode">Whether the boss should charge the user when he fires the wrong letter</param>
        public EnemyDifficulty(int id, string name, string description, float gameSpeed, 
            int initialHealth, int damageToPlayerPerHit, int maxEnemiesonScreen, float bossHealthHandicap, bool enableBossRageMode)
        {
            _id = id;
            _name = name;
            _description = description;
            _gameSpeed = gameSpeed;
            _initialHealth = initialHealth;
            _damageToPlayerPerHit = damageToPlayerPerHit;
            _maxEnemiesOnScreen = maxEnemiesonScreen;
            _bossHealthHandicap = bossHealthHandicap;
            _enableBossRageMode = enableBossRageMode;
        }
        #endregion Constructors -----------------------------------------------
    }
}
