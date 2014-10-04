using System;
using System.Collections.Generic;
using System.Linq;

namespace GGProductions.LetterStorm.Data.Collections
{
    /// <summary>
    /// Represents a List of Words the user can be tested on
    /// </summary>
    [Serializable]
    public class WordSet : List<Word>
    {
        #region Private Variables ---------------------------------------------
        /// <summary>Random number generator</summary>
        /// <remarks>This is stored as part of the class instead of within a 
        /// function so that it produces different numbers when used multiple 
        /// times in a row, and to reduce initialization time</remarks>
        private static Random rand = null;
        #endregion Private Variables ------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents a List of Words the user can be tested on
        /// </summary>
        public WordSet(): base()
        {
            
        }
        #endregion Constructors -----------------------------------------------
        
        #region Psuedo-Enumerators --------------------------------------------
        /// <summary>
        /// Retrieve the next Word from this Word Set.  The Word will be the 
        /// first untested word in the sorted List.
        /// </summary>
        /// <exception cref="NoUntestedWordsException">Thrown if the WordSet
        /// does not contain any words the user has not been tested against</exception>
        public Word GetNextWord()
        {
            // Create the query to retrieve all Words that the user has not 
            // been tested on, ordered from shortest to longest
            var queryUntestedWords = from Word w in this
                                     where w.Tested == false
                                     orderby w.Text ascending
                                     select w;

            // If the user has not already been tested on all Words in the WordSet...
            if (queryUntestedWords.Count() > 0)
            {
                // Select the first Word from the sorted result set
                Word nextWord = queryUntestedWords.ElementAt(0);

                // Find that word in the original List and mark it as tested so it is not returned again
                int indexOfNextWord = this.IndexOf(nextWord);
                this[indexOfNextWord].Tested = true;

                // Return the next word to the caller
                return nextWord;
            }
            // Else if the user has already been tested against all words 
            // in the WordSet, throw a NoUntestedWordsException
            else
                throw new NoUntestedWordsException();
        }

        /// <summary>
        /// Retrieve a random word from this Word Set.  The word will be taken
        /// from a subset composing of all words of the shortest length.
        /// </summary>
        /// <exception cref="NoUntestedWordsException">Thrown if the WordSet
        /// does not contain any words the user has not been tested against</exception>
        public Word GetRandomWord()
        {
            // Create the query to retrieve all words that the user has not 
            // been tested on, ordered from shortest to longest
            var queryUntestedWords = from Word w in this
                                     where w.Tested == false
                                     orderby w.Text ascending
                                     select w;

            // If the user has not already been tested on all words in the WordSet...
            if (queryUntestedWords.Count() > 0)
            {
                // Find the length of the shortest word in the result set
                int shortestUntestedWordLength = queryUntestedWords.ElementAt(0).Text.Length;

                // Create a subquery to retrieve all the shortest words
                var queryShortestWords = from Word w in queryUntestedWords
                                         where w.Text.Length == shortestUntestedWordLength
                                         select w;

                // If the random number generator has not been initialized, do so
                if (rand == null)
                    rand = new Random();

                // Select a random word from the shortest-words result set
                Word randomWord = queryShortestWords.ElementAt(rand.Next(queryShortestWords.Count()));

                // Find that word in the original List and mark it as tested so it is not returned again
                int indexOfRandomWord = this.IndexOf(randomWord);
                this[indexOfRandomWord].Tested = true;

                // Return the random shortest word to the caller
                return randomWord;
            }
            // Else if the user has already been tested against all words 
            // in the WordSet, throw a NoUntestedWordsException
            else
                throw new NoUntestedWordsException();
        }
        #endregion Psuedo-Enumerators -----------------------------------------

        #region State Functions -----------------------------------------------
        /// <summary>
        /// Determine if the WordSet contains words that 
        /// the user has not been tested on
        /// </summary>
        public bool ContainsUntestedWords()
        {
            // Create a query to retrieve all words that the user has 
            // not been tested on
            var queryUntestedWords = from Word w in this
                                     where w.Tested == false
                                     select w;
            // Return whether any such words still exist
            return queryUntestedWords.Count() > 0;
        }

        /// <summary>
        /// Reset all Words in this WordSet to be untested
        /// </summary>
        /// <remarks>WARNING: Only call this before starting the game play;
        /// calling during a level or between levels may result in the
        /// user being retested on the same words during the same session</remarks>
        public void ResetAllWordsToUntested()
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Tested = false;
        }
        #endregion State Functions --------------------------------------------
    }
}
