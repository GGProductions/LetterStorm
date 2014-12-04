using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGProductions.LetterStorm.InGameHelpClasses
{
    /// <summary>
    /// Keeps track of all score points accumulated by a user in one playthrough
    /// </summary>
    public class ScoreKeeper
    {
        #region Private Variables ---------------------------------------------
        int _score = 0;
        #endregion Private Variables ------------------------------------------

        #region Enums ---------------------------------------------------------
        /// <summary>
        /// Enumeration representing the different tasks a player can accomplish that should be rewarded with points
        /// </summary>
        public enum PlayerAchievement
        {
            /// <summary>Player has successfully defeated a smart enemy</summary>
            DefeatSmartEnemy = 1,

            /// <summary>Player has successfully defeated a dumb enemy</summary>
            DefeatDumbEnemy,

            /// <summary>Player has collected a dropped letter</summary>
            CollectLetter,

            /// <summary>Player has collected a power-up</summary>
            CollectPowerUp,

            /// <summary>Player has successfully defeated part of the boss</summary>
            DefeatBossPart,

            /// <summary>Player has successfully defeated the boss by spelling a word</summary>
            DefeatBoss
        }

        /// <summary>
        /// Enumeration representing the different bad things that can happen to a player for which he should lose points
        /// </summary>
        public enum PlayerFailure
        {
            /// <summary>Player has sustained damage from a smart enemy</summary>
            HitBySmartEnemy = 1,

            /// <summary>Player has sustained damage from a dumb enemy</summary>
            HitByDumbEnemy,
            
            /// <summary>Player has sustained damage from a projectile fired by the boss</summary>
            HitByBossProjectile,

            /// <summary>Player has sustained damage from the boss ramming him</summary>
            HitByBossBody,

            /// <summary>Player fired the wrong letter at the boss, or missed the boss with his letter</summary>
            FiredBadLetter
        }
        #endregion Enums ------------------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// The number of points the player has accumulated during the current playthrough
        /// </summary>
        public int Score
        {
            get { return _score; }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Keeps track of all score points accumulated by a user in one playthrough
        /// </summary>
        public ScoreKeeper()
        {
            _score = 0;
        }
        
        /// <summary>
        /// Keeps track of all score points accumulated by a user in one playthrough
        /// </summary>
        /// <param name="startingScore">The score the player should start with</param>
        public ScoreKeeper(int startingScore)
        {
            _score = startingScore;
        }

        #endregion Constructors -----------------------------------------------

        #region Methods -------------------------------------------------------
        /// <summary>
        /// Increase the score based on a task the player has accomplished
        /// </summary>
        /// <param name="achievement">The task accomplished by the user for which he should be rewarded score points</param>
        public void Increase(PlayerAchievement achievement)
        {
            switch (achievement)
            {
                case PlayerAchievement.DefeatSmartEnemy:
                    _score += 50;
                    break;
                case PlayerAchievement.DefeatDumbEnemy:
                    _score += 20;
                    break;
                case PlayerAchievement.CollectLetter:
                    _score += 10;
                    break;
                case PlayerAchievement.CollectPowerUp:
                    _score += 20;
                    break;
                case PlayerAchievement.DefeatBossPart:
                    _score += 100;
                    break;
                case PlayerAchievement.DefeatBoss:
                    _score += 500;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Decrease the score based on a bad thing that has happened to the player
        /// </summary>
        /// <param name="failure">A bad thing that has happened to the player for which he should be punished by reducing his score points</param>
        public void Decrease(PlayerFailure failure)
        {
            switch (failure)
            {
                case PlayerFailure.HitBySmartEnemy:
                    _score -= 10;
                    break;
                case PlayerFailure.HitByDumbEnemy:
                    _score -= 5;
                    break;
                case PlayerFailure.HitByBossProjectile:
                    _score -= 10;
                    break;
                case PlayerFailure.HitByBossBody:
                    _score -= 25;
                    break;
                case PlayerFailure.FiredBadLetter:
                    _score -= 10;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Reset the score to 0
        /// </summary>
        public void Reset()
        {
            _score = 0;
        }
        #endregion Methods ----------------------------------------------------
    }
}
