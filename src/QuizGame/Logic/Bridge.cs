using QuizGame.CustomControls;
using QuizGame.Model;
using System.Collections.Generic;
using System.Windows.Controls;

namespace QuizGame.Logic
{
    public class Bridge
    {
        #region controls
        //referinte catre controlaele din MainWindow (interfata "admin")
        private TextBox QuestionContent;
        private TextBox AnswerContent;
        private CheckBox AnswerState;

        private ListView Answers;
        private NumericControl QuestionIndex;
        #endregion

        #region fields
        public  Quiz Q;
        #endregion

        #region constructo
        //constructorul care ia ca parametrii controalele respective
        public Bridge(
            TextBox _QuestionContent, 
            TextBox _AnswerContent, 
            CheckBox _AnswerState, 
            ListView _Answers, 
            NumericControl _QuestionIndex)
        {
            this.QuestionContent = _QuestionContent;
            this.AnswerContent = _AnswerContent;
            this.AnswerState = _AnswerState;
            this.Answers = _Answers;
            this.QuestionIndex = _QuestionIndex;

            //intitializam un quiz gol la inceput
            this.Q = new Quiz();
            this.onLoad();
        }
        #endregion

        #region utilitati
        //updateaza controlul numeric care schimba intrebarile cu nr de intrebari existente
        private void updateMaxim()
        {
            this.QuestionIndex.Maximum = this.Q.Questions.Count - 1;
        }
        //updateaza sursa ListView-ului la schimbarea Quiz-ului
        private void updateSource()
        {
            this.Answers.ItemsSource = this.Q.CurrentQuestion.Answers;
        }
        //updateaza sursa si maximul 
        private void onLoad()
        {
            this.updateMaxim();
            this.updateSource();
        }
        private void update()
        {
            this.updateMaxim();
            this.updateSource();
        }
        #endregion

        #region functionalitati
        //adaug un raspuns la intrebarea curenta
        public void AddAnswer()
        {
            //creez un raspuns care ia ca parametru textul din TextBox raspuns si 
            //starea Checkboxului de langa raspuns
            Answer answer = new Answer(this.AnswerContent.Text, (bool)this.AnswerState.IsChecked);
            //adaug raspunsul la intrebarea curenta
            Q.CurrentQuestion.Answers.Add(answer);
            //sterg continutul ramas in urma Texboxului raspuns 
            this.ClearAnswer();
        }
        //sterg continutul ramas in urma Texboxului raspuns 
        private void ClearAnswer()
        {
            this.AnswerContent.Text = "";
            this.AnswerState.IsChecked = false;
        }
        //sterg raspunsul selectat din listview din lista cu raspunsuri a intrebarii curente
        //sau raspunsurile selectate in cazul in care sunt mai multe
        public void DeleteAnswers()
        {
            List<int> indexes = new List<int>();
            foreach(var item in this.Answers.SelectedItems)
            {
                int index = this.Answers.Items.IndexOf(item);
                indexes.Add(index);
            }

            foreach(int i in indexes)
            {
                this.Q.CurrentQuestion.Answers.RemoveAt(i);
            }
        }
        //adaug intrebarea la lista de intrebari
        public void AddQuestion()
        {
            //adaug continutul intrebarii curente (intrebarea in sine)
            this.Q.CurrentQuestion.Content = this.QuestionContent.Text;
            //creez o intrebare si ma mut la ea si o fac intrebarea curenta
            this.Q.NewQuestion();
            //sterg continutul intrebarii ramse de la anterioara
            this.ClearQuestion();
            //daca ma duc spre o intrebare care exista atunci ii pun continutul
            this.UpdateView();
        }
        //sterge continutul textului din textboxul intrebare
        private void ClearQuestion()
        {
            this.QuestionContent.Text = "";
        }
        //daca urmatoarea intrebare are continut il afisez in textbox
        private void UpdateView()
        {
            this.update();
            if (this.Q.CurrentQuestion.Content != "")
            {
                this.QuestionContent.Text = this.Q.CurrentQuestion.Content;
            }
            else
            {
                this.ClearQuestion();
            }
            this.QuestionIndex.Value = this.Q.CurrentIndex;
        }
        //sterge o intrebare cu raspuns cu tot
        public void RemoveQuestion()
        {
            int index = this.Q.CurrentIndex;

            if (index == this.Q.Questions.Count - 1 && this.Q.Questions.Count != 1)
            {
                return;
            }

            if (index == 0 && this.Q.Questions.Count == 1)
            {
                this.Q.Questions.Add(new Question());
                this.Q.Questions.RemoveAt(0);
            }
            else if(index == 0 && this.Q.Questions.Count > 1)
            {
                this.Q.Questions.RemoveAt(0);
            }
            else
            {
                this.Q.Questions.RemoveAt(index);
                this.Q.CurrentIndex -= 1;
            }

            this.UpdateView();
        }
        //la schimbarea unei intrebari
        public void ChangeQuestion()
        {
            this.Q.CurrentIndex = this.QuestionIndex.Value;
            this.UpdateView();
        }
        //reincarca un quiz
        public void reload(Quiz qz)
        {
            this.Q = qz;
            this.onLoad();
            this.UpdateView();
        }
        #endregion
    }
}
