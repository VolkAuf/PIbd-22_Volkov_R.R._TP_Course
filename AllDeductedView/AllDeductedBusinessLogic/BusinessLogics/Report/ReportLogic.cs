using AllDeductedBusinessLogic.BindingModels;
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
            });
        }

        public void SaveToWordFile(ReportBindingModel model)
        {
            var records = new List<ReportDisciplineViewModel>();
            var students = studentStorage.GetFullList().Where(rec => model.Students.Select(rec => rec.Id).Contains(rec.Id)).ToList();
            foreach(var student in students)
            {
                records.Add(new ReportDisciplineViewModel
                {
                    StudentName = student.LastName,
                    Disciplines = student.Disciplines,
                });
            }
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин",
                Disciplines = records,
            });
        }

        public void SaveToExcelFile(ReportBindingModel model)
        {
            var records = new List<ReportDisciplineViewModel>();
            var students = studentStorage.GetFullList().Where(rec => model.Students.Select(rec => rec.Id).Contains(rec.Id)).ToList();
            foreach (var student in students)
            {
                records.Add(new ReportDisciplineViewModel
                {
                    StudentName = student.LastName,
                    Disciplines = student.Disciplines,
                });
            }
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин",
                Disciplines = records,
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
