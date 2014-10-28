using System;

namespace GGProductions.LetterStorm.Data
{
    /// <summary>
    /// Exception thrown when an attempt is made to retrieve an untested Word
    /// from a WordSet that does not contain any untested Words.
    /// </summary>
    public class NoUntestedWordsException : InvalidOperationException
    {
        /// <summary>
        /// Exception thrown when an attempt is made to retrieve an untested Word
        /// from a WordSet that does not contain any untested Words.
        /// </summary>
        public NoUntestedWordsException()
            : base("The user has been tested against all Words in the current Word Set.  An untested Word is not available to be retrieved.")
        {
        }
    }

    /// <summary>
    /// Exception thrown when an attempt is made to retrieve a lesson using a 
    /// GUID that does not match any of the existing lessons' IDs
    /// </summary>
    public class LessonNotFoundException : NullReferenceException
    {
        /// <summary>
        /// Exception thrown when an attempt is made to retrieve a lesson using a 
        /// GUID that does not match any of the existing lessons' IDs
        /// </summary>
        public LessonNotFoundException():
            base("The specified lesson could not be located.  It may have been deleted.") 
        { 
        }
    }

    /// <summary>
    /// Exception thrown when an attempt is made to retrieve an enemy difficulty level using 
    /// an id that does not match any of the existing difficulty levels' IDs
    /// </summary>
    public class EnemyDifficultyNotFoundException : NullReferenceException
    {
        /// <summary>
        /// Exception thrown when an attempt is made to retrieve an enemy difficulty level using 
        /// an id that does not match any of the existing difficulty levels' IDs
        /// </summary>
        public EnemyDifficultyNotFoundException() :
            base("The specified enemy difficulty level could not be located.  It may have been deleted.")
        {
        }
    }
}
