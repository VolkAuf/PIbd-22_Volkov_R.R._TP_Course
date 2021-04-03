using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.Enums;
using AllDeductedBusinessLogic.ViewModels;
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

        public int Id
        {
            set { id = value; }
        }
       
        private int? id;
        

        private readonly StudyingStatusLogic logicSS;
        private readonly StudentLogic logicS;
        public StudyingStatusWindow(StudyingStatusLogic logicSS, StudentLogic logicS)
        {
            InitializeComponent();
            this.logicSS = logicSS;
            this.logicS = logicS;
        }

        private void StudyingStatusesWindow_Load(object sender, RoutedEventArgs e)
        {
            List<StudentViewModel> list = logicS.Read(null);
            if (list != null)
            {
                comboBoxStudentId.ItemsSource = list;
                comboBoxStudentId.SelectedItem = null;
            }
            comboBoxStudyingBase.Items.Clear();
            foreach (string _base in Enum.GetNames(typeof(StudyingBase)))
            {
                comboBoxStudyingBase.Items.Add(_base);
            }
            comboBoxStudyingForm.Items.Clear();
            foreach (string form in Enum.GetNames(typeof(StudyingForm)))
            {
                comboBoxStudyingForm.Items.Add(form);
            }
            if (id.HasValue)
            {
                StudyingStatusViewModel status = logicSS.Read(new StudyingStatusBindingModel
                {
                    Id = id
                })?[0];

                comboBoxStudentId.SelectedValue = status.StudentId;
                comboBoxStudyingBase.SelectedItem = status.StudyingBase.ToString();
                comboBoxStudyingForm.SelectedItem = status.StudyingForm.ToString();
                textBoxCourse.Text = status.Course.ToString();
            }
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
                    Id = id,
                    StudentId = (int)comboBoxStudentId.SelectedValue,
                    StudyingForm = (StudyingForm)Enum.Parse(typeof(StudyingForm), comboBoxStudyingForm.SelectedValue.ToString()),
                    StudyingBase = (StudyingBase)Enum.Parse(typeof(StudyingBase), comboBoxStudyingBase.SelectedValue.ToString()),
                    Course = Convert.ToInt32(textBoxCourse.Text),
                    ProviderId = App.SelectProvider.Id
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
