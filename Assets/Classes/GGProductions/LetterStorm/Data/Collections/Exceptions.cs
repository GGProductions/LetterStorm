using System;

namespace GGProductions.LetterStorm.Data.Collections
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
}
