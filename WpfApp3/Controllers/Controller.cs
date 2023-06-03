using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfApp3.Models;

namespace WpfApp3.Controllers
{
    [Serializable]
   public class Controller
    {
        //название вуза
        public List<Exam> Exams { get; set; }
        public string MainName { get; set; }
        //Констукторы 
        public Controller() { }
        public Controller(string mainname, List<Exam> exams)
        {
            MainName = mainname;
            Exams = exams;
        }

        public void GenerData()
        {
            Exams = new List<Exam>
            {
                new Exam("Физика", "Иванов И.А.", "ВК994591", new DateTime(2021,04,12), _random.Next(2, 6)),
                new Exam("Химия", "Петров Н.А.", "ВВ561231", new DateTime(2021,04,14), _random.Next(2, 6)),
                new Exam("Алгебра", "Сидоров И.О.", "ВК909043", new DateTime(2021,04,17), _random.Next(2, 6)),
                new Exam("География", "Кранин Л.Л.", "ВК563191", new DateTime(2021,04,12), _random.Next(2, 6)),
                new Exam("Биология", "Царев О.Р.", "ВВ904312", new DateTime(2021,04,10), _random.Next(2, 6)),
                new Exam("Геометрия", "Андропов Д.Д.", "ВН677675", new DateTime(2021,04,13), _random.Next(2, 6)),
                new Exam("История", "Лунин Т.Б.", "ВР709120", new DateTime(2021,04,10), _random.Next(2, 6)),
                new Exam("Физика", "Цаплин И.А.", "ВК545092", new DateTime(2021,04,19), _random.Next(2, 6)),
                new Exam("Химия", "Ветров П.В.", "ВВ633341", new DateTime(2021,04,16), _random.Next(2, 6)),
                new Exam("История", "Орлов Д.И.", "ВП765432", new DateTime(2021,04,18), _random.Next(2, 6))
            };
        } // GenerData

        // Получение данных

        //Зпаполнение с помощью Linq
        public IEnumerable GetStudents() =>
            Exams.Select(exam => new
            {

                exam.Name,
                exam.SurnameNP,
                exam.Date,
                exam.Rating
            });

        public void DeleteExam(int index) =>
        Exams.RemoveAt(index);

        public static Random _random = new Random();

        public static OpenFileDialog Ofd()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Файл данных для загрузки",
                Filter = "Файлы XML (*.xml)|*.xml|Все файлы (*.*)|*.*",
                FilterIndex = 0,
                //нашел что бы в корень проекта попадало
                InitialDirectory = Environment.CurrentDirectory
            };
            return ofd;
        }//Ofd

        //вынес создание Sfd в контролер
        public static SaveFileDialog Sfd()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = @"Data.xml",
                Title = "Файл данных для сохранения",
                Filter = "Файлы XML (*.xml)|*.xml|Все файлы (*.*)|*.*",
                FilterIndex = 0,
                //что бы в корень проекта попадало
                InitialDirectory = Environment.CurrentDirectory
            };
            return sfd;
        }//Sfd


        //Десириализация 
        public Controller Deserial(string fileName)
        {
            //конструктор форматтера
            XmlSerializer formatter = new XmlSerializer(typeof(Controller));
            //создаем контролер для возрата его в форму
            Controller controller;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                controller = (Controller) formatter.Deserialize(fs);
            } // using
            return controller;
        }//Deserial

        //сериализация
        public void Serial(Controller controller, string fileName)
        {
            //если список пустой, нечего сериализовать
            if (controller.Exams.Count == 0)
                throw new Exception();
            //конструктор форматтера
            XmlSerializer formatter = new XmlSerializer(typeof(Controller));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, controller);
            } // using
        }//Serial

    }
}
        
    

