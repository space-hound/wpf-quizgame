using System.Collections.ObjectModel;

namespace QuizGame.Model
{
    public class FinalQuiz
    {
        private ObservableCollection<Question> questions;

        public ObservableCollection<Question> Questions { get => questions; set => questions = value; }

        public FinalQuiz(Quiz _quiz)
        {
            this.questions = _quiz.Questions;
        }
    }
}
