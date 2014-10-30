using System;

namespace GGProductions.LetterStorm.Data
{
    /// <summary>
    /// Represents a word that a user can be tested against, 
    /// as well as its associated hint and metadata
    /// </summary>
    [Serializable]
    public class Word: IComparable
    {
        #region Private Variables ---------------------------------------------
        // Create the private variables that the this class's 
        //  properties will use to store their data
        private string _text;
        private string _hint;
        
        // Do not include this field in the save file
        [NonSerialized]
        private bool _tested;
        #endregion Private Variables ------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// The text of the word
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// The hint associated with the word
        /// </summary>
        public string Hint
        {
            get
            {
                return _hint;
            }
            set
            {
                _hint = value;
            }
        }

        /// <summary>
        /// Flag indicating whether the user was tested on how to spell this word
        /// </summary>  
        public bool Tested
        {
            get
            {
                return _tested;
            }
            set
            {
                _tested = value;
            }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents a word that a user can be tested against, 
        /// as well as its associated hint and metadata
        /// </summary>
        public Word()
        {
        }

        /// <summary>
        /// Represents a word that a user can be tested against, 
        /// as well as its associated hint and metadata
        /// </summary>
        /// <param name="text">The text of the word the user should be tested on</param>
        public Word(string text)
        {
            _text = text;
            _hint = String.Empty;
        }

        /// <summary>
        /// Represents a word that a user can be tested against, 
        /// as well as its associated hint and metadata
        /// </summary>
        /// <param name="text">The text of the word the user should be tested on</param>
        /// <param name="hint">The hint associated with the word</param>
        public Word(string text, string hint)
        {
            _text = text;
            _hint = hint;
        }
        #endregion Constructors -----------------------------------------------

        #region Overridden Methods --------------------------------------------
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _text + "||" + _hint;
            //return base.ToString();
        }
        #endregion Overridden Methods -----------------------------------------

        #region IComparable Methods -------------------------------------------
        /// <summary>
        /// Compares the current instance with another object of the same type 
        /// and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order 
        /// as the other object
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>-1 = this is less than obj; 0 = this is equal to obj;
        /// 1 = this is greater than obj</returns>
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)    // If the object to compare this instance to is null,
                return 1;       // note that this instance is greater than the object

            // Attempt to convert obj to the same type as this, and compare each's 
            // Text property to determine in what order they should be sorted
            Word otherWord = obj as Word;
            return this.Text.CompareTo(otherWord.Text);
        }
        #endregion IComparable Methods ----------------------------------------
    }
}
