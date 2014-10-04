using System;

namespace GGProductions.LetterStorm.Data.Collections
{
    /// <summary>
    /// Represents a Lesson or category of Words the user can be tested on
    /// </summary>
    [Serializable]
    public class Lesson
    {
        #region Private Variables ---------------------------------------------
        // Create the private variables that the this class's 
        //  properties will use to store their data
        private string _name;
        private Guid _id;
        private WordSet _words;
        #endregion Private Variables ------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// The name or title of this lesson
        /// </summary>
        public String Name
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
        /// The unique ID of this Lesson
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// The List of Words associated with this Lesson that
        /// the user can be tested on
        /// </summary>
        public WordSet Words
        {
            get
            {
                return _words;
            }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents a Lesson or category of Words the user can be tested on
        /// </summary>
        public Lesson()
        {
            _words = new WordSet();
        }

        /// <summary>
        /// Represents a Lesson or category of Words the user can be tested on
        /// </summary>
        /// <param name="name">The name of the Lesson</param>
        /// <remarks>Use this for creating a new Lesson</remarks>
        public Lesson(String name)
        {
            _name = name;
            _id = Guid.NewGuid();
            _words = new WordSet();
        }
        #endregion Constructors -----------------------------------------------
    }
}
