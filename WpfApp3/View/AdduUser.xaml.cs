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
using System.Windows.Shapes;
using WpfApp3.Controllers;
using WpfApp3.Models;

namespace WpfApp3.View
{
    /// <summary>
    /// Логика взаимодействия для AdduUser.xaml
    /// </summary>
    public partial class AdduUser : Window
    {


        public Exam NewExam { get; set; }
        //Констуктор для формы добавить 
        public AdduUser()
        {
            InitializeComponent();

            
        }

        //Констутор для формы Изминить 
        public AdduUser(Exam exam)
        {

            InitializeComponent();
            ChangeStudent(exam);

        }

        private void BtSaveClick(object sender, RoutedEventArgs e)
        {

            int.TryParse(tbxRating.Text, out int x);

            NewExam = new Exam
            {
                Name = tbxName.Text,
                SurnameNP = tbxSurnameNP.Text,
                Passport = tbxPassport.Text,
                Date = DateTime.Parse(tbxDate.Text),
                Rating = x
            };
            DialogResult = true;

        } //BTOK

        public void ChangeStudent(Exam exam)
        {
            Title = "";
            tbxName.Text = exam.Name;
            tbxSurnameNP.Text = exam.SurnameNP;
            tbxPassport.Text = exam.Passport;
            tbxDate.Text = exam.Date.ToShortDateString();
            tbxRating.Text = exam.Rating.ToString();
        } // ChangeStudent
    }
}