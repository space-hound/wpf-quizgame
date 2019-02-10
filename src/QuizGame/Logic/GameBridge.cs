using QuizGame.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace QuizGame.Logic
{
    public class GameBridge
    {
        //bridge-ul responsabil pentru al doilea window

        #region control
        TextBlock question;
        ListBox answers;
        #endregion

        #region fields
        //quizul original
        Quiz submited;
        public Quiz Submited { get => submited; set => submited = value; }
        //quizul copie cu toate raspunsurile false
        //se modifica pe parcurs la setarea de catre utilizator
        Quiz final;
        //scoriul final calculat doar la apsarea butonului submit
        int score;
        public int Score { get => score; set => score = value; }
        #endregion

        #region constructor
        public GameBridge(TextBlock _question, ListBox _answers, Quiz _final)
        {
            this.question = _question;
            this.answers = _answers;
            this.final = _final;
            this.checkLast();
            this.updateSubmited();
            this.updateView();
            this.score = 0;
        }
        #endregion

        #region metode
        //verifica daca ultima intrebare este goala si o scoate
        void checkLast()
        {
            int at = this.final.Questions.Count - 1;
            if(this.final.Questions[at].Content == "")
            {
                this.final.Questions.RemoveAt(at);
            }
        }
        //creeaza quiz-ul cu toate raspunsurile false copie dupa cel original
        void updateSubmited()
        {
            ObservableCollection<Answer> a;
            ObservableCollection<Question> q = new ObservableCollection<Question>();

            foreach(Question cq in this.final.Questions)
            {
                a = new ObservableCollection<Answer>();

                foreach(Answer cc in cq.Answers)
                {
                    a.Add(new Answer(cc.Content));
                }

                q.Add(new Question(cq.Content, a));
            }

            this.submited = new Quiz(q);
        }
        void updateView()
        {
            this.question.Text = this.submited.CurrentIndex + " - " + this.submited.CurrentQuestion.Content;
            this.answers.ItemsSource = this.submited.CurrentQuestion.Answers;
        }
        //schimba intrebarea curent
        public void NextQuestion()
        {
            this.submited.NextQuestion();
            this.updateView();
        }
        public void PrevQuestion()
        {
            this.submited.PrevQuestion();
            this.updateView();
        }
        //schimba un raspuns al intrebarii
        public void SelectAnswer(int index, bool state)
        {
            this.submited.CurrentQuestion.Answers[index].IsTrue = state;
        }
        //verifica quizul si calculeaza scorul si il incarac in variabila score de mai sus
        public void Submit()
        {
            int no = this.final.Questions.Count;

            for (int i = 0; i < no; i++)
            {
                Question fn = this.final.Questions[i];
                Question sb = this.submited.Questions[i];

                int k = fn.Answers.Count;
                bool add = true;

                for(int j = 0; j < k; j++)
                {
                    if(fn.Answers[j].IsTrue != sb.Answers[j].IsTrue)
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                {
                    this.score += 1;
                }
            }
        }
        #endregion

    }
}
