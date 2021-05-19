using AllDeductedBusinessLogic.Enums;
using AllDeductedBusinessLogic.HelperModels;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Linq;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    class SaveToPdfUpd
    {
        public static void CreateDoc(PdfInfo info)
        {
            PdfWriter writer = new PdfWriter(info.FileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            AddParagraphCenter(info.Title, document);
            AddParagraphCenter($"с {info.DateFrom.ToShortDateString()} по { info.DateTo.ToShortDateString()}", document);

            Table tableStatuses = new Table(6, false);
            tableStatuses.SetWidth(new UnitValue(UnitValue.PERCENT, 100));
            tableStatuses.SetMarginBottom(30);

            Cell threadCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Поток"));

            Cell StudentCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Зачетка студента"));

            Cell CoursCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Курс"));

            Cell FormCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Форма обучения"));

            Cell BaseCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("База обучения"));

            Cell DateCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Дата создания"));

            tableStatuses.AddCell(threadCell);
            tableStatuses.AddCell(StudentCell);
            tableStatuses.AddCell(CoursCell);
            tableStatuses.AddCell(FormCell);
            tableStatuses.AddCell(BaseCell);
            tableStatuses.AddCell(DateCell);

            foreach (var status in info.Statuses)
            {

                threadCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(status.ThreadName));

                StudentCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(status.StudentId.ToString()));

                FormCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(Enum.GetName(typeof(StudyingForm), status.StudyingForm)));

                BaseCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(Enum.GetName(typeof(StudyingBase), status.StudyingBase)));

                CoursCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(status.Course.ToString()));

                DateCell = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(status.DateCreate.ToShortDateString()));

                tableStatuses.AddCell(threadCell);
                tableStatuses.AddCell(StudentCell);
                tableStatuses.AddCell(CoursCell);
                tableStatuses.AddCell(FormCell);
                tableStatuses.AddCell(BaseCell);
                tableStatuses.AddCell(DateCell);
            }
            document.Add(tableStatuses);
            document.Close();
        }

        private static void AddParagraphCenter(string text, Document document)
        {
            Paragraph paragraph = new Paragraph(text)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(paragraph);
        }

        private static void AddBoldParagraphLeft(string text, Document document)
        {
            Paragraph paragraph = new Paragraph(text)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(18)
               .SetBold();

            document.Add(paragraph);
        }

        private static void AddParagraphLeft(string text, Document document)
        {
            Paragraph paragraph = new Paragraph(text)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(16);

            document.Add(paragraph);
        }
    }
}
