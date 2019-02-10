using QuizGame.Logic;
using QuizGame.Model;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame
{
    public partial class QuizerGame : Window
    {
        private GameBridge B;
        public GameBridge BR { get => B; set => B = value; }

        public QuizerGame(Quiz _qz)
        {
            InitializeComponent();
            //ca sa pot face binding
            this.DataContext = this;
            this.B = new GameBridge(
                this.QuestionContent,
                this.Answers,
                _qz
                );
        }


        //neimplemetat
        private void GameClickHandler(object sender, RoutedEventArgs e)
        {
        }


        //daca da click pe butoanele de jos
        //detecteaza pe care si face ceva
        private void BottomClick(object sender, RoutedEventArgs e)
        {
            var a = (Button)e.OriginalSource;

            switch (a.Name)
            {
                case "Next":
                    B.NextQuestion();
                    break;
                case "Prev":
                    B.PrevQuestion();
                    break;
                case "Sub":
                    this.Submit();
                    break;
                default:
                    return;
            }
        }

        //cand da click pe un checkbox de la raspuns ii modifica 
        //la raspuns starea daca checkboxul este bifat sau nu
        //cu adevarat sau fals
        private void CheckedClick(object sender, RoutedEventArgs e)
        {
            var a = (CheckBox)e.OriginalSource;
            bool state = (bool)a.IsChecked;
            int index = this.B.Submited.CurrentQuestion.Answers.IndexOf(((Answer)(((ListBoxItem)a.Tag).DataContext)));
            this.B.SelectAnswer(index, state);
        }

        //creeaza un dialog care intreaba daca utilizatorul este sigur
        //daca nu iese afara din functie
        //daca da
        //calculeaza scorul
        //afiseaza un alt dialog care arata scorul si daca apasa ok inchide aplicatia
        //si nu poate apasa decat okay
        private void Submit()
        {
            if (MessageBox.Show("Are You Sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                B.Submit();
                string dialog = "Your Score is: " + this.B.Score + " out of " + (this.B.Submited.Questions.Count);
                if(MessageBox.Show(dialog, "Finaly", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
        }
    }
}
