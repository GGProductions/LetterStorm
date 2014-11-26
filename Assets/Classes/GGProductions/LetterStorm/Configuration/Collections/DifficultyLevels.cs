using GGProductions.LetterStorm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGProductions.LetterStorm.Configuration.Collections
{
    /// <summary>
    /// Collection of the various enemy difficulties tha game can be configured with
    /// </summary>
    /// <remarks>Three default difficulties are automatically included, though new ones can be dynamically added</remarks>
    public class DifficultyLevels: List<EnemyDifficulty>
    {
        #region Constructors --------------------------------------------------
        /// <summary>
        /// Collection of the various enemy difficulties tha game can be configured with
        /// </summary>
        public DifficultyLevels()
        {
            // Create the default enemy difficulty levels if this list does not contain any others
            if (this.Count == 0)
                CreateDefaultDifficultyLevels();
        }
        #endregion Constructors -----------------------------------------------

        #region Populators ----------------------------------------------------
        /// <summary>
        /// Create the default difficulty levels
        /// </summary>
        private void CreateDefaultDifficultyLevels()
        {
            // Create the "Easy" difficulty level for young children
            EnemyDifficulty easy = new EnemyDifficulty(
                1,
                "Easy",
                "Enemies are slower and spawn in smaller groups, the player takes less damage per hit, and bosses are easier to defeat.  Recommended for young children who are new to gaming.",
                0.7f,   // Game speed: slower than normal
                100,    // Initial health: normal
                5,      // Damage taken per hit: less than normal
                4,      // Max enemies on screen: less than normal
                0.5f,   // Boss health modifier: less than normal
                false,  // Don't enable boss rage mode
                0.5f,   // Boss cannon tracking accuracy: significant lag
                2,      // Boss cannon projectiles/second: slow fire rate
                0.7f,   // Boss rage mode charge speed: less than normal
                10,     // Big boss window to accept letters: larger than normal
                5);     // Big boss window to deflect letters: normal
                

            // Create the "Normal" difficulty level for children with some gaming experience
            EnemyDifficulty normal = new EnemyDifficulty(
                5,
                "Normal",
                "Enemies confront the user in mid-sized groups, the player takes reasonable damage per hit, and bosses are challenging.  Recommended for children who have some experience gaming.",
                1.0f,   // Game speed: normal
                100,    // Initial health: normal
                10,     // Damage taken per hit: normal
                6,      // Max enemies on screen: normal
                1.0f,   // Boss health modifier: normal
                true,   // Enable boss rage mode
                0.75f,  // Boss cannon tracking accuracy: slight lag
                3,      // Boss cannon projectiles/second: average fire rate
                1.0f,   // Boss rage mode charge speed: normal
                5,      // Big boss window to accept letters: normal
                5);     // Big boss window to deflect letters: normal

            // Create the "Hard" difficulty level for older children with significant gaming experience
            EnemyDifficulty hard = new EnemyDifficulty(
                9,
                "Hard",
                "Enemies are faster and spawn in large groups, the player takes more damage per hit, and bosses are significantly challenging.  Recommended for older children who have significant experience gaming.",
                1.3f,   // Game speed: faster than normal
                100,    // Initial life count: normal
                20,     // Damage taken per hit: more than normal
                10,     // Max enemies on screen: more than normal
                1.5f,   // Boss health modifier: more than normal
                true,   // Enable boss rage mode
                1.0f,   // Boss cannon tracking accuracy: no lag
                5,      // Boss cannon projectiles/second: fast fire rate
                1.3f,   // Boss rage mode charge speed: faster than normal
                5,      // Big boss window to accept letters: normal
                10);    // Big boss window to deflect letters: larger than normal

            // Add the enemy difficulty levels to this list
            this.Add(easy);
            this.Add(normal);
            this.Add(hard);
        }
        #endregion Populators -------------------------------------------------

        #region Retrieval Methods ---------------------------------------------
        /// <summary>
        /// Retrieve the EnemyDifficulty level associated with the specified ID
        /// </summary>
        /// <param name="difficultyId">The id of the EnemyDifficulty level to retrieve</param>
        /// <exception cref="EnemyDifficultyNotFoundException">Thrown when the difficultyId
        /// specified does not match any of the enemy difficulty levels in this collection</exception>
        public EnemyDifficulty GetDifficultyById(int difficultyId)
        {   // If at least one EnemyDifficulty in this collection has the ID specified, retrieve it
            if (this.Count(d => d.ID.Equals(difficultyId)) > 0)
                return this.First(d => d.ID.Equals(difficultyId));
            // Else, if no difficulties have the specified ID, thrown an exception
            else
                throw new EnemyDifficultyNotFoundException();
        }

        /// <summary>
        /// Retrieve the EnemyDifficulty level associated with the specified ID
        /// </summary>
        /// <param name="difficultyName">The id of the EnemyDifficulty level to retrieve</param>
        /// <exception cref="EnemyDifficultyNotFoundException">Thrown when the difficultyId
        /// specified does not match any of the enemy difficulty levels in this collection</exception>
        public EnemyDifficulty GetDifficultyByName(String difficultyName)
        {   // If at least one EnemyDifficulty in this collection has the ID specified, retrieve it
            if (this.Count(d => d.Name.Equals(difficultyName, StringComparison.OrdinalIgnoreCase)) > 0)
                return this.First(d => d.Name.Equals(difficultyName, StringComparison.OrdinalIgnoreCase));
            // Else, if no difficulties have the specified ID, thrown an exception
            else
                throw new EnemyDifficultyNotFoundException();
        }
        #endregion Retrieval Methods ------------------------------------------
    }
}
