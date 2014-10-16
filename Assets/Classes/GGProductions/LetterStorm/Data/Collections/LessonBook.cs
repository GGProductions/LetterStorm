using System;

namespace GGProductions.LetterStorm.Data.Collections
{
    /// <summary>
    /// Represents all the Lessons for a specific user
    /// </summary>
    [Serializable]
    public class LessonBook
    {
        #region Private Variables ---------------------------------------------
        // Create the private variables that the this class's 
        //  properties will use to store their data
        private LessonCollection _lessons;
        #endregion Private Variables ------------------------------------------

        #region Properties ----------------------------------------------------
        /// <summary>
        /// The Lessons of Words the user can be tested on
        /// </summary>
        public LessonCollection Lessons
        {
            get
            {
                return _lessons;
            }
        }
        #endregion Properties -------------------------------------------------

        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents all the Lessons for a specific user
        /// </summary>
        public LessonBook()
        {
            _lessons = new LessonCollection();
        }
        #endregion Constructors -----------------------------------------------

        #region Populators ----------------------------------------------------
        /// <summary>
        /// Create a sample Lesson with a few basic words and hints
        /// </summary>
        public void CreateSampleLesson()
        {
            // Create a sample Lesson with a few words and hints
            Lesson sampleLesson = new Lesson("Sample Lesson");
            sampleLesson.Words.Add(new Word("cat", "the most famous animal on YouTube"));
            sampleLesson.Words.Add(new Word("dog", "man's best friend"));
            sampleLesson.Words.Add(new Word("fish", "<blank> and chips"));
            sampleLesson.Words.Add(new Word("horse", "people ride me"));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleLesson);
        }
        #endregion Populators -------------------------------------------------


    }
}
