using QuizGame.Model;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using QuizGame.Logic;
using Microsoft.Win32;

namespace QuizGame
{
    public partial class MainWindow : Window
    {
        #region declarations
        private Bridge B;

        //proprietatea pridge care este data ca DataContext ca sa pot face binding
        //pe proprietatile lui cum ar fi lista de intrebari, intrebare curenta, samd
        public Bridge BR { get => B; set => B = value; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //construieste obiectul bridge care face legatura intre Quiz ca obiect si window-ul curent
            this.B = new Bridge(
                this.QuestionContent, 
                this.AnswerContent, 
                this.AnswerState, 
                this.Answers, 
                this.QuestionIndex
                );
        }

        /**********************************************************/
        //---> FILE MENU RELATED EVENTS
        /**********************************************************/
        private void FileClickHandler(object sender, RoutedEventArgs e)
        {
            MenuItem target = (MenuItem)e.OriginalSource;

            if(target.Name == "LoadDb")
            {
                //deschid un dialog de incaract fisier
                var ofd = new OpenFileDialog
                {
                    Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt"
                };

                //daca dau open si selectez un fisier imi da calea catre el
                if(ofd.ShowDialog() == true)
                {
                    //creeaza un quiz din fisierul incarcat
                    Quiz qz = Persistance.Load(ofd.FileName);
                    this.B.reload(qz);
                }
            }
            if(target.Name == "SaveAsDb")
            {
                //deschide un save file dialog
                var sfd = new SaveFileDialog
                {
                    Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt"
                };

                //daca dau save si selectez sau creez un fiser
                if (sfd.ShowDialog() == true)
                {
                    //calea returnata
                    string filename = sfd.FileName;
                    //il creeaza la calea returnata
                    Persistance.SaveAs(filename, this.B.Q);
                }
            }
            //daca butonul este exit aplicatia se inchide
            if(target.Name == "Exit")
            {
                this.Close();
            }
        }

        /**********************************************************/
        //---> ANSWER RELATED EVENTS
        /**********************************************************/
        //daca da click pe add de la sectiunea de raspunsuri adauga un raspuns
        private void AddAnswerClick(object sender, RoutedEventArgs e)
        {
            B.AddAnswer();
        }
        //daca da click pe delete de la sectiunea de raspunsuri sterge raspunsul selectata
        private void RemoveAnswerClick(object sender, RoutedEventArgs e)
        {
            B.DeleteAnswers();
        }

        /**********************************************************/
        //---> QUESTION RELATED EVENTS
        /**********************************************************/
        private void BottomClick(object sender, RoutedEventArgs e)
        {
            var a = (Button)e.OriginalSource;

            switch (a.Name)
            {
                case "AddQuestion":
                    B.AddQuestion();
                    break;
                case "RemoveQuestion":
                    B.RemoveQuestion();
                    break;
                case "SaveQuestion":
                    this.QzGame();
                    break;
                default:
                    return;
            }

        }

        //daca se schimba numarul din numeric control schimba si intrebarea cu nr respectiv
        private void OnNumberChanged(object sender, PropertyChangedEventArgs e)
        {
            B.ChangeQuestion();
        }

        //daca dau start
        private void QzGame()
        {
            //se creeaza un window nou cu quizul de aici
            //cu toate raspunsurile false 
            //la care utilizatorul trebuie sa raspunda
            QuizerGame QZ = new QuizerGame(this.B.Q);
            //il afiseaza
            QZ.Show();
            //iar pe acesta il inchide
            this.Close();
        }
    }
}
