using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GGProductions.LetterStorm.Data.Collections
{
    /// <summary>
    /// Represents a set of Lessons
    /// </summary>
    [Serializable]
    public class LessonCollection: Collection<Lesson>
    {
        #region Constructors --------------------------------------------------
        /// <summary>
        /// Represents a set of Lessons
        /// </summary>
        public LessonCollection()
            : base()
        {
        }
        #endregion Constructors -----------------------------------------------

        #region Retrieval Methods ---------------------------------------------
        /// <summary>
        /// Retrieve the Lesson associated with the specified ID
        /// </summary>
        /// <param name="lessonId">The id of the Lesson to retrieve</param>
        /// <exception cref="LessonNotFoundException">Thrown when the lessonId
        /// specified does not match any of the Lessons in this collection</exception>
        public Lesson GetLessonById(Guid lessonId)
        {   // If at least one Lesson in this collection has the ID specified, retrieve it
            if (this.Count(l => l.ID.Equals(lessonId)) > 0)
                return this.First(l => l.ID.Equals(lessonId));
            // Else, if not lessons have the specified ID, thrown an exception
            else
                throw new LessonNotFoundException();
        }
        #endregion Retrieval Methods ------------------------------------------
    }
}
