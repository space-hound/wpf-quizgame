using System;
using System.Collections.ObjectModel;


namespace QuizGame.Model
{
    //flag ca poate fi serializat(transformat in text, json in cazul nostru)
    [Serializable]
    public class Question
    {
        #region proprietati
        //continutul intrebarii
        private string content;
        public string Content { get => content; set => content = value; }
        //raspunsurile intrebarii
        //ObservableCollection este o lista speciala care updateaza vizual ListView-ul daca aceasta se modifica
        //este necesar doar sa scoti sau adaugi un obiect in lista
        //si schimbarea se reflecta vizual fara nici o linie de cod necesara
        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers { get => answers; set => answers = value; }
        #endregion

        #region constructo
        public Question(string _content, ObservableCollection<Answer> _answers = null)
        {
            this.content = _content;
            this.answers = (_answers != null) ? _answers : new ObservableCollection<Answer>();
        }
        public Question()
        {
            this.content = "";
            this.answers = new ObservableCollection<Answer>();
        }
        #endregion
    }
}
