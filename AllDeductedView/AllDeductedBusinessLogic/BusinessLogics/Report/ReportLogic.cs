using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Enums;
using AllDeductedBusinessLogic.HelperModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private IStudentStorage studentStorage;
        private IStudyingStatusStorage studyingStatusStorage;
        private IThreadStorage threadStorage;

        public ReportLogic(IStudentStorage studentStorage, IStudyingStatusStorage studyingStatusStorage, IThreadStorage threadStorage)
        {
            this.studentStorage = studentStorage;
            this.studyingStatusStorage = studyingStatusStorage;
            this.threadStorage = threadStorage;
        }

        public List<StudyingStatusViewModel> GetStatus(ReportBindingModel model)
        {
            return studyingStatusStorage.GetFilteredList(new StudyingStatusBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                ProviderId = model.ProviderId
            });
        }

        private List<ReportDisciplineViewModel> GetDisciplines(ReportBindingModel model)
        {
            var records = new List<ReportDisciplineViewModel>();
            var students = studentStorage.GetFullList()
                .Where(rec => model.Students
                .Select(rec => rec.Id)
                .Contains(rec.Id))
                .ToList();
            foreach (var student in students)
            {
                records.Add(new ReportDisciplineViewModel
                {
                    StudentName = student.LastName,
                    Disciplines = student.Disciplines,
                });
            }
            return records;
        }

        public DiagramInfo GetDiagramInfo(ReportBindingModel model)
        {
            var status = GetStatus(model);
            var countForm = status.GroupBy(rec => Enum.GetName(typeof(StudyingForm),rec.StudyingForm))
                .Select(rec => new Tuple<string, int> (rec.Key, rec.Count()))
                .OrderBy(rec => rec.Item1)
                .ToList();
            var countBase = status.GroupBy(rec => Enum.GetName(typeof(StudyingBase), rec.StudyingBase))
                .Select(rec => new Tuple<string, int>(rec.Key, rec.Count()))
                .OrderBy(rec => rec.Item1)
                .ToList();
            return new DiagramInfo
            {
                CountBase = countBase,
                CountForm = countForm
            };
        }

        public void SaveToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин",
                Disciplines = GetDisciplines(model)
            });
        }

        public void SaveToExcelFile(ReportBindingModel model)
        {
            
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин",
                Disciplines = GetDisciplines(model)
            });
        }

        public void SaveToPdfFile(ReportBindingModel model)
        {
            SaveToPdfUpd.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список статусов",
                Statuses = GetStatus(model),
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value
            });
        }
    }
}
