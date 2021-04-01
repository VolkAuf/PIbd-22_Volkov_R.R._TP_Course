using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace AllDeductedView
{
    /// <summary>
    /// Логика взаимодействия для StudyingStatusWindow.xaml
    /// </summary>
    public partial class StudyingStatusWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int StudyingStatusId
        {
            set { }
        }
        public int StudentId
        {
            set { comboBoxStudentId.SelectedValue = value; }
        }

        public int Course
        {
            set
            {
                textBoxCourse.Text = value.ToString();
            }
        }

        private readonly StudyingStatusLogic logicSS;
        public StudyingStatusWindow(StudyingStatusLogic logicSS, StudentLogic logicS)
        {
            InitializeComponent();
            /*List<StudentViewModel> list = logicS.Read(null);
            if (list != null)
            {
                comboBoxStudentId.ItemsSource = list;
                comboBoxStudentId.SelectedItem = null;
            }*/
            comboBoxStudyingBase.Items.Clear();
            foreach (string position in Enum.GetNames(typeof(StudyingBase)))
            {
                comboBoxStudyingBase.Items.Add(position);
            }
            comboBoxStudyingForm.Items.Clear();
            foreach (string position in Enum.GetNames(typeof(StudyingForm)))
            {
                comboBoxStudyingForm.Items.Add(position);
            }
            this.logicSS = logicSS;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCourse.Text))
            {
                MessageBox.Show("Заполните поле Course", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxStudentId.SelectedValue == null)
            {
                MessageBox.Show("Выберите Student", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxStudyingForm.SelectedValue == null)
            {
                MessageBox.Show("Выберите Forms", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxStudyingBase.SelectedValue == null)
            {
                MessageBox.Show("Выберите Base", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicSS.CreateOrUpdate(new StudyingStatusBindingModel
                {
                    StudentId = (int)comboBoxStudentId.SelectedValue,
                    StudyingForm = (StudyingForm)Enum.Parse(typeof(StudyingForm), comboBoxStudyingForm.SelectedValue.ToString()),
                    StudyingBase = (StudyingBase)Enum.Parse(typeof(StudyingBase), comboBoxStudyingBase.SelectedValue.ToString()),
                    Course = Convert.ToInt32(textBoxCourse.Text),
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
