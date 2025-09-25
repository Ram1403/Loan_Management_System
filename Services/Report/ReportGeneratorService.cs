using System;
using ClosedXML.Excel;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Loan_Management_System.Services.Report
{
    public class ReportGeneratorService
    {
        private readonly LoanDbContext _context;

        public ReportGeneratorService(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateReportAsync(string reportType, int adminId, string format = "pdf")
        {
            string fileName = $"{reportType.Replace(" ", "_")}_{DateTime.UtcNow:yyyyMMddHHmmss}.{format}";
            string filePath = Path.Combine("Reports", fileName);

            Directory.CreateDirectory("Reports");

            switch (reportType)
            {
                case "NPA Report":
                    var npas = await _context.Npas
                        .Include(n => n.Loan)
                        .ThenInclude(l => l.LoanApplication)
                        .ThenInclude(a => a.Customer)
                        .ToListAsync();

                    if (format == "pdf") GenerateNpaPdf(npas, filePath);
                    else GenerateNpaExcel(npas, filePath);
                    break;

                case "Repayment Report":
                    var repayments = await _context.Repayments
                        .Include(r => r.Loan)
                        .ThenInclude(l => l.LoanApplication)
                        .ThenInclude(a => a.Customer)
                        .ToListAsync();

                    if (format == "pdf") GenerateRepaymentPdf(repayments, filePath);
                    else GenerateRepaymentExcel(repayments, filePath);
                    break;

                case "Loan Scheme Report":
                    var schemes = await _context.LoanSchemes
                        .Include(s => s.LoanApplications)
                        .ToListAsync();

                    if (format == "pdf") GenerateSchemePdf(schemes, filePath);
                    else GenerateSchemeExcel(schemes, filePath);
                    break;

                default:
                    throw new ArgumentException("Invalid report type");
            }

            // Save metadata in Report table
            var report = new Models.Report
            {
                GeneratedBy = adminId,
                ReportType = reportType,
                GeneratedDate = DateTime.UtcNow,
                Parameters = $"Format={format}",
                FilePath = filePath,
                CreatedAt = DateTime.UtcNow
            };
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return filePath;
        }

        // ------------------ PDF GENERATORS ------------------

        private void GenerateNpaPdf(List<Npa> npas, string filePath)
        {
            int totalNpa = npas.Count;
            decimal totalOverdue = npas.Sum(n => n.OverdueAmount);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("NPA Report").Bold().FontSize(18);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Total NPAs: {totalNpa}");
                        col.Item().Text($"Total Overdue: {totalOverdue:C}");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.ConstantColumn(60);
                                c.RelativeColumn();
                                c.ConstantColumn(100);
                                c.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Loan ID").Bold();
                                header.Cell().Text("Customer").Bold();
                                header.Cell().Text("Overdue").Bold();
                                header.Cell().Text("Days").Bold();
                            });

                            foreach (var n in npas)
                            {
                                table.Cell().Text(n.LoanId.ToString());
                                table.Cell().Text($"{n.Loan.LoanApplication.Customer.FirstName} {n.Loan.LoanApplication.Customer.LastName}");
                                table.Cell().Text(n.OverdueAmount.ToString("C"));
                                table.Cell().Text(n.DaysOverdue.ToString());
                            }
                        });
                    });
                });
            }).GeneratePdf(filePath);
        }

        private void GenerateRepaymentPdf(List<Repayment> repayments, string filePath)
        {
            decimal totalCollected = repayments.Sum(r => r.Amount);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("Repayment Report").Bold().FontSize(18);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Total Repayments: {repayments.Count}");
                        col.Item().Text($"Total Collected: {totalCollected:C}");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.ConstantColumn(60);
                                c.RelativeColumn();
                                c.ConstantColumn(80);
                                c.ConstantColumn(120);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Loan ID").Bold();
                                header.Cell().Text("Customer").Bold();
                                header.Cell().Text("Amount").Bold();
                                header.Cell().Text("Date").Bold();
                            });

                            foreach (var r in repayments)
                            {
                                table.Cell().Text(r.LoanId.ToString());
                                table.Cell().Text($"{r.Loan.LoanApplication.Customer.FirstName} {r.Loan.LoanApplication.Customer.LastName}");
                                table.Cell().Text(r.Amount.ToString("C"));
                                table.Cell().Text(r.PaymentDate.ToShortDateString());
                            }
                        });
                    });
                });
            }).GeneratePdf(filePath);
        }

        private void GenerateSchemePdf(List<LoanScheme> schemes, string filePath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("Loan Scheme Report").Bold().FontSize(18);
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(60);
                            c.RelativeColumn();
                            c.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Scheme ID").Bold();
                            header.Cell().Text("Scheme Name").Bold();
                            header.Cell().Text("Applications").Bold();
                        });

                        foreach (var s in schemes)
                        {
                            table.Cell().Text(s.SchemeId.ToString());
                            table.Cell().Text(s.SchemeName);
                            table.Cell().Text(s.LoanApplications.Count.ToString());
                        }
                    });
                });
            }).GeneratePdf(filePath);
        }

        // ------------------ EXCEL GENERATORS ------------------

        private void GenerateNpaExcel(List<Npa> npas, string filePath)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("NPA Report");

            ws.Cell(1, 1).Value = "Loan ID";
            ws.Cell(1, 2).Value = "Customer";
            ws.Cell(1, 3).Value = "Overdue";
            ws.Cell(1, 4).Value = "Days";

            int row = 2;
            foreach (var n in npas)
            {
                ws.Cell(row, 1).Value = n.LoanId;
                ws.Cell(row, 2).Value = $"{n.Loan.LoanApplication.Customer.FirstName} {n.Loan.LoanApplication.Customer.LastName}";
                ws.Cell(row, 3).Value = n.OverdueAmount;
                ws.Cell(row, 4).Value = n.DaysOverdue;
                row++;
            }

            wb.SaveAs(filePath);
        }

        private void GenerateRepaymentExcel(List<Repayment> repayments, string filePath)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Repayment Report");

            ws.Cell(1, 1).Value = "Loan ID";
            ws.Cell(1, 2).Value = "Customer";
            ws.Cell(1, 3).Value = "Amount";
            ws.Cell(1, 4).Value = "Date";

            int row = 2;
            foreach (var r in repayments)
            {
                ws.Cell(row, 1).Value = r.LoanId;
                ws.Cell(row, 2).Value = $"{r.Loan.LoanApplication.Customer.FirstName} {r.Loan.LoanApplication.Customer.LastName}";
                ws.Cell(row, 3).Value = r.Amount;
                ws.Cell(row, 4).Value = r.PaymentDate;
                row++;
            }

            wb.SaveAs(filePath);
        }

        private void GenerateSchemeExcel(List<LoanScheme> schemes, string filePath)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Scheme Report");

            ws.Cell(1, 1).Value = "Scheme ID";
            ws.Cell(1, 2).Value = "Scheme Name";
            ws.Cell(1, 3).Value = "Applications";

            int row = 2;
            foreach (var s in schemes)
            {
                ws.Cell(row, 1).Value = s.SchemeId;
                ws.Cell(row, 2).Value = s.SchemeName;
                ws.Cell(row, 3).Value = s.LoanApplications.Count;
                row++;
            }

            wb.SaveAs(filePath);
        }
    }
}




//using System;
//using ClosedXML.Excel;
//using Loan_Management_System.Models;
//using Microsoft.EntityFrameworkCore;
//using Loan_Management_System.Data;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//namespace Loan_Management_System.Services.Report
//{
//    public class ReportGeneratorService
//    {
//        private readonly LoanDbContext _context;

//        public ReportGeneratorService(LoanDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<string> GenerateReportAsync(string reportType, int adminId, string format = "pdf")
//        {
//            string fileName = $"{reportType}_{DateTime.UtcNow:yyyyMMddHHmmss}.{format}";
//            string filePath = Path.Combine("Reports", fileName);

//            Directory.CreateDirectory("Reports"); // ensure folder exists

//            if (reportType == "NPA Report")
//            {
//                var npas = await _context.Npas
//                    .Include(n => n.Loan)
//                    .ThenInclude(l => l.LoanApplication)
//                    .ThenInclude(a => a.Customer)
//                    .ToListAsync();

//                if (format == "pdf")
//                    GenerateNpaPdf(npas, filePath);
//                else
//                    GenerateNpaExcel(npas, filePath);
//            }


//            var report = new Loan_Management_System.Models.Report //use lie tis since folder name is also Report
//            {
//                GeneratedBy = adminId,
//                ReportType = reportType,
//                GeneratedDate = DateTime.UtcNow,
//                Parameters = "Generated by Admin",
//                FilePath = filePath,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Reports.Add(report);
//            await _context.SaveChangesAsync();

//            return filePath;
//        }

//        private void GenerateNpaPdf(List<Npa> npas, string filePath)
//        {
//            Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    page.Header().Text("NPA Report").Bold().FontSize(20);
//                    page.Content().Table(table =>
//                    {
//                        table.ColumnsDefinition(c =>
//                        {
//                            c.ConstantColumn(80);
//                            c.RelativeColumn();
//                            c.ConstantColumn(100);
//                            c.ConstantColumn(100);
//                        });

//                        table.Header(header =>
//                        {
//                            header.Cell().Text("Loan ID").Bold();
//                            header.Cell().Text("Customer").Bold();
//                            header.Cell().Text("Overdue Amount").Bold();
//                            header.Cell().Text("Days Overdue").Bold();
//                        });

//                        foreach (var n in npas)
//                        {
//                            table.Cell().Text(n.LoanId.ToString());
//                            table.Cell().Text($"{n.Loan.LoanApplication.Customer.FirstName} {n.Loan.LoanApplication.Customer.LastName}");
//                            table.Cell().Text(n.OverdueAmount.ToString("C"));
//                            table.Cell().Text(n.DaysOverdue.ToString());
//                        }
//                    });
//                });
//            }).GeneratePdf(filePath);
//        }

//        private void GenerateNpaExcel(List<Npa> npas, string filePath)
//        {
//            using var workbook = new XLWorkbook();
//            var ws = workbook.Worksheets.Add("NPA Report");

//            ws.Cell(1, 1).Value = "Loan ID";
//            ws.Cell(1, 2).Value = "Customer";
//            ws.Cell(1, 3).Value = "Overdue Amount";
//            ws.Cell(1, 4).Value = "Days Overdue";

//            int row = 2;
//            foreach (var n in npas)
//            {
//                ws.Cell(row, 1).Value = n.LoanId;
//                ws.Cell(row, 2).Value = $"{n.Loan.LoanApplication.Customer.FirstName} {n.Loan.LoanApplication.Customer.LastName}";
//                ws.Cell(row, 3).Value = n.OverdueAmount;
//                ws.Cell(row, 4).Value = n.DaysOverdue;
//                row++;
//            }

//            workbook.SaveAs(filePath);
//        }
//    }
//}
