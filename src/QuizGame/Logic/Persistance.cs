using Newtonsoft.Json;
using QuizGame.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace QuizGame.Logic
{
    //foloseste libraria Newtonsoft pentru serializare de json in obiecte .NET(C#)
    public static class Persistance
    {
        //ia un fisier il citeste, si converteste continutul intr-un obiect Quiz
        //ia ca parametrii calea catre un fisier
        public static Quiz Load(string name)
        {
            //citeste tot continutul textului
            string json = System.IO.File.ReadAllText(name);
            //il serializeaza (transforma intr-o lista de intrebari)
            ObservableCollection<Question> qt = JsonConvert.DeserializeObject<ObservableCollection<Question>>(json);
            //creeaza un quiz cu lista de mai sus
            qt.Add(new Question());
            //returneaza quiz-ul
            return new Quiz(qt);
        }

        //ia calea catre un fisier si scrie in el din obiectul quizzin json
        //ia ca patrametrii calea catre fisier si un obiect quiz
        public static void SaveAs(string name, Quiz qz)
        {
            ObservableCollection<Question> qt = qz.Questions;

            //verifica ca ultima intrebare sa nu fie goala(dummy)
            if(qt[qt.Count - 1].Content == "")
            {
                qt.RemoveAt(qt.Count - 1);
            }

            //transforma obiectul in json
            string js = JsonConvert.SerializeObject(qt);
            //il scrie in fiser sau creeaza si scrie in fiser daca nu exista fistierul
            System.IO.File.WriteAllText(name, js);
        }
    }
}
