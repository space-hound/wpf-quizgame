using System;


namespace QuizGame.Model
{
    //flag ca poate fi serializat(transformat in text, json in cazul nostru)
    [Serializable]
    public class Answer
    {

        #region proprietati
        //defineste raspunsul ca fiind adevarat sau fals
        private bool isTrue;
        public bool IsTrue { get => isTrue; set => isTrue = value; }

        //continutul intrebarii, intrebarea in sine
        private string content;
        public string Content { get => content; set => content = value; }
        #endregion

        #region constructor
        public Answer(string _content, bool _isTrue = false)
        {
            this.content = _content;
            this.isTrue = _isTrue;
        }
        public Answer()
        {
            this.content = "";
            this.isTrue = false;
        }
        #endregion
    }
}
