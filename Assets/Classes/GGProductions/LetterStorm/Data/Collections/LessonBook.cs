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
        [Obsolete("This method is deprecated.  Please use CreateSampleLessons() instead")]
        public void CreateSampleLesson()
        {
            CreateSampleLessons();
        }

        /// <summary>
        /// Create a sample Lessons for K5 through Grade 4 with a few basic words and hints
        /// </summary>
        public void CreateSampleLessons()
        {
            // Create a sample Kindergarden Lesson with a few words and hints
            Lesson sampleK5Lesson = new Lesson("K5 Sample");
            sampleK5Lesson.Words.Add(new Word("slug", "(noun) a homeless snail"));
            sampleK5Lesson.Words.Add(new Word("cat", "(noun) the most famous animal on YouTube"));
            sampleK5Lesson.Words.Add(new Word("dog", "(noun) man's best friend"));
            sampleK5Lesson.Words.Add(new Word("bug", "(noun) a small insect"));
            sampleK5Lesson.Words.Add(new Word("fish", "(noun) <blank> and chips"));
            sampleK5Lesson.Words.Add(new Word("zebra", "(noun) black and white stripes"));
            sampleK5Lesson.Words.Add(new Word("lion", "(noun) king of the jungle"));
            sampleK5Lesson.Words.Add(new Word("bird", "(noun) I haz wings"));
            sampleK5Lesson.Words.Add(new Word("snake", "(noun) I slither"));
            sampleK5Lesson.Words.Add(new Word("deer", "(noun) I freez in the headlights "));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleK5Lesson);

            // Create a sample Grade 1 Lesson with a few words and hints
            Lesson sampleGrade1Lesson = new Lesson("First Grade Sample");
            sampleGrade1Lesson.Words.Add(new Word("tag", "(noun) a game in which one player chases the others and tries to touch one 'your IT'"));
            sampleGrade1Lesson.Words.Add(new Word("burn", "(verb) to be on fire"));
            sampleGrade1Lesson.Words.Add(new Word("globe", "(noun) a round ball that has a map of the earth drawn on it"));
            sampleGrade1Lesson.Words.Add(new Word("add", "(verb) to combine two or more numbers into one sum"));
            sampleGrade1Lesson.Words.Add(new Word("mask", "(noun) a cover or partial cover for the face usually made of cloth with openings for the eyes"));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleGrade1Lesson);

            // Create a sample Grade 2 Lesson with a few words and hints
            Lesson sampleGrade2Lesson = new Lesson("Second Grade Sample");
            sampleGrade2Lesson.Words.Add(new Word("cowboy", "(noun) a distinctively dressed horseman tending large herds of beef cattle"));
            sampleGrade2Lesson.Words.Add(new Word("poor", "(noun) one that has little money – usually used collectively"));
            sampleGrade2Lesson.Words.Add(new Word("bedtime", "(noun) the period set apart to settle for sleeping"));
            sampleGrade2Lesson.Words.Add(new Word("doctor", "(noun) a practitioner of medicine, dentistry or veterinary medicine"));
            sampleGrade2Lesson.Words.Add(new Word("prize", "(noun) an honor or reward striven for in a competitive contest"));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleGrade2Lesson);

            // Create a sample Grade 3 Lesson with a few words and hints
            Lesson sampleGrade3Lesson = new Lesson("Third Grade Sample");
            sampleGrade3Lesson.Words.Add(new Word("safety", "(noun) freedom from exposure to danger"));
            sampleGrade3Lesson.Words.Add(new Word("birthday", "(noun) an anniversary of being born"));
            sampleGrade3Lesson.Words.Add(new Word("wildlife", "(noun) living things that are neither human nor domesticated"));
            sampleGrade3Lesson.Words.Add(new Word("freeze", "(verb) to stand or remain without movement or activity of any kind"));
            sampleGrade3Lesson.Words.Add(new Word("tuesday", "(noun) the day following Monday"));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleGrade3Lesson);

            // Create a sample Grade 4 Lesson with a few words and hints
            Lesson sampleGrade4Lesson = new Lesson("Fourth Grade Sample");
            sampleGrade4Lesson.Words.Add(new Word("arrow", "(noun) a weapon shot from a bow that consists of a straight slender shaft with a point or sharp head and feathers or vanes fastened near the end"));
            sampleGrade4Lesson.Words.Add(new Word("detective", "(noun) a plainclothes police officer"));
            sampleGrade4Lesson.Words.Add(new Word("infant", "(noun) a child in the first year of life : baby"));
            sampleGrade4Lesson.Words.Add(new Word("motion", "(noun) an act or instance of moving the body or any of its members : gesture"));
            sampleGrade4Lesson.Words.Add(new Word("sketch", "(verb) to draw or paint a rough drawing representing an object or scene and often made as a preliminary study"));

            // Add the sample Lesson to the LessonCollection
            _lessons.Add(sampleGrade4Lesson);
        }
        #endregion Populators -------------------------------------------------


    }
}
