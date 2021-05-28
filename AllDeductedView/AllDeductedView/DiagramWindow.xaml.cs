using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
    /// Логика взаимодействия для DiagramWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public DiagramWindow(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            var info = logic.GetDiagramInfo(new ReportBindingModel
            {
                DateFrom = DatePikerFrom.SelectedDate,
                DateTo = DatePikerTo.SelectedDate
            });

            ((PieSeries)TotalCountChart.Series[0]).ItemsSource = info.CountForm;
            ((ColumnSeries)CountByMounthChart.Series[0]).ItemsSource = info.CountBase;

        }
    }
}
