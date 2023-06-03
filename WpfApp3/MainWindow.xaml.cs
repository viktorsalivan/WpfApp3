using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Controllers;
using WpfApp3.Models;
using WpfApp3.View;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //для работы с файлами
        private OpenFileDialog _ofd;

        private SaveFileDialog _sfd;


        private Controller _controller;
        
        public MainWindow()
        {
            
            InitializeComponent();
            _controller = new Controller();
            _controller.GenerData();
            
        }

        /// <summary>
        /// Команда выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void command_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Меню
        /// </summary>
        /// <param name="MainMenu"></param>
        /// 
        // Команда добавить
       
        private void command_add(object sender, RoutedEventArgs e)
        {
            AdduUser adduUser = new AdduUser();
            if (adduUser.ShowDialog() == true)
            {
                _controller.Exams.Add(adduUser.NewExam);

                DgMain.ItemsSource = _controller.GetStudents();
               
            }  // if

        }
        //Команда изминить
        private void command_change(object sender, RoutedEventArgs e)
        {
            AdduUser adduUser = new AdduUser(_controller.Exams[DgMain.SelectedIndex]);
            if (adduUser.ShowDialog() == true)
            {
                _controller.Exams[DgMain.SelectedIndex] = adduUser.NewExam;

                DgMain.ItemsSource = _controller.GetStudents();
                
            }  // if
        }
        //Команда Удалить
        private void command_delete(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _controller.DeleteExam(DgMain.SelectedIndex);

                DgMain.ItemsSource = _controller.GetStudents();
            } // if
        }
        //Команда справка
        private void command_about(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("О программе", "Информация");
        }
        //Появление формы.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgMain.ItemsSource = _controller.GetStudents();
            sb_counter.Text = _controller.Exams.Count.ToString();
        }

        private void command_save(object sender, RoutedEventArgs e)
        {
            //создали 
            _sfd = Controller.Sfd();
            //стандартный диалог
            if (_sfd.ShowDialog() == true)
            {
                //попытка сериализации
                try
                {
                    //проверка
                    if (_controller == null)
                        throw new Exception();
                    //передаем в метод
                    _controller.Serial(_controller, _sfd.FileName);
                    MessageBox.Show("Сохранение выполнено", "Предупреждение", MessageBoxButton.OK,
                        MessageBoxImage.Information, MessageBoxResult.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("Сохранение не возможно", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                } // try-catch
            }//if
        }

        private void command_load(object sender, RoutedEventArgs e)
        {
            _ofd = Controller.Ofd();
            if (_ofd.ShowDialog() == true)
            {
                try
                {
                    //предупреждаем
                    if (MessageBoxResult.Yes == MessageBox.Show("При чтении удалится текущий список, продолжить?",
                        "Предупреждение!",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        //чистим
                        _controller.Exams.Clear();
                        //передаем в метод имя файла и получаем контролер
                        _controller = _controller.Deserial(_ofd.FileName);
                        //делаем привязку
                        DgMain.ItemsSource = _controller.Exams;
                    }
                    else
                        throw new Exception();

                    //if
                }
                catch (Exception)
                {
                    MessageBox.Show("Чтение не выполнено", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                } // try-catch
            }//if
        }
    }
}
