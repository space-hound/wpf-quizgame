using System;
using System.Collections.ObjectModel;

namespace QuizGame.Model
{
    public class Quiz
    {
        /**********************************************************/
        //---> FIELDS
        /**********************************************************/

        #region declarations
        private ObservableCollection<Question> questions;
        private int currentIndex;
        #endregion

        /**********************************************************/
        //---> PROPERTIES
        /**********************************************************/

        #region properties
        public ObservableCollection<Question> Questions
        {
            get
            {
                return this.questions;
            }

            set
            {
                this.questions = value;
            }
        }
        public int CurrentIndex
        {
            get
            {
                return this.currentIndex;
            }
            set
            {
                //daca indexul curent este mai mare decat nr de intrebari arunca eroare
                if (value < 0 || value >= this.questions.Count)
                    throw new IndexOutOfRangeException();
                this.currentIndex = value;
            }
        }
        //poti accesa dar nu modifica intrebarea curenta
        //dependenta de indexul curent si foloseste indexul curent
        //nu exista un cam intrebare curent, nefiind necesar daca exista index curent
        public Question CurrentQuestion
        {
            get
            {
                return this.questions[this.currentIndex];
            }
        }
        #endregion

        /**********************************************************/
        //---> CONSRTUCTEUR
        /**********************************************************/

        #region constructor
        //primul default care se executa in cazul in care se creeaza un formular nou
        public Quiz()
        {
            this.questions = new ObservableCollection<Question>();
            this.questions.Add(new Question());
            this.currentIndex = 0;
        }
        //al doilea care ia ca param o lista de intrebari pentru cazul in care se incaracdin fiser un quiz    
        public Quiz(ObservableCollection<Question> _q)
        {
            this.questions = _q;
            this.currentIndex = 0;
        }
        #endregion

        /**********************************************************/
        //---> METHODS
        /**********************************************************/

        #region methods
        //adauga o noua intrebare si si schimba indexul curent
        public void NewQuestion()
        {
            this.questions.Add(new Question());
            this.currentIndex++;
        }
        //sterge o intrebare dupa un obiect intrebare daca exista in lista
        public void RemoveQuestion(Question q)
        {
            this.questions.Remove(q);

        }
        //sterge o intrebare la pozitia "index"
        public void RemoveQuestionAt(int index)
        {
            this.questions.RemoveAt(index);
        }
        //schimba intrebarea curenta cu urmatoarea
        public void NextQuestion()
        {
            if (this.currentIndex + 1 >= this.questions.Count)
                this.currentIndex = 0;
            else
                this.currentIndex += 1;
        }
        //schimba intrebarea curenta cu precedenta
        public void PrevQuestion()
        {
            if (this.currentIndex - 1 < 0)
                this.currentIndex = this.questions.Count - 1;
            else
                this.currentIndex -= 1;
        }
        #endregion
    }
}
