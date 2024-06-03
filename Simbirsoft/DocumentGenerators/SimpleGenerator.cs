using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Simbirsoft.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Simbirsoft.DocumentGenerators
{
    internal class SimpleGenerator
    {
        private readonly DateTime _from;
        private readonly DateTime _to;

        public SimpleGenerator(DateTime from, DateTime to)
        {
            _from = from;
            _to = to;
        }

        private class EmployeeWithService
        {
            public Employee Employee { get; set; }
            public List<Service> Services { get; set; }
        }

        public async Task<byte[]> CreatePdfDocument()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var employeesWithServices = (await ModelContext.Instance.Services
                .Where(e => e.DateStart < _to && e.DateStart > _from)
                .ToListAsync())
                .GroupBy(g => g.Employee)
                .Select(s => new EmployeeWithService { Employee = s.Key, Services = s.ToList() })
                .ToList();

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginTop(15);
                    page.MarginBottom(2);
                    page.MarginLeft(15);
                    page.MarginRight(12);
                    page.PageColor(Colors.Transparent);

                    page.Content().Column(column =>
                    {
                        column.Item().AlignLeft().PaddingLeft(15).Text(t =>
                        {
                            t.DefaultTextStyle(s => s.FontSize(16).FontFamily(Fonts.Arial).Bold());
                            t.Span($"Отчёт по услугам СимбирСофт");
                        });

                        column.Item().AlignLeft().PaddingTop(35).PaddingLeft(15).Text(t =>
                        {
                            t.DefaultTextStyle(s => s.FontSize(12).FontFamily(Fonts.Arial));
                            t.Span($"Количество выполненных услуг по сотрудникам:");
                        });

                        column.Item().PaddingTop(20).PaddingLeft(15).PaddingRight(15).Element(e => ComposeTableEmployee(e, employeesWithServices));
                    });
                });
            }).GeneratePdf();
        }

        private void ComposeTableEmployee(IContainer container, List<EmployeeWithService> employeesWithServices)
        {
            var headers = new string[] { "Id", "ФИО работника", "Количество выполненных услуг", "Процент от общего количества", "Выручка", "Процент от общей выручки"};

            var summaryCount = employeesWithServices.SelectMany(s => s.Services).Count();
            var summaryPrice = employeesWithServices.SelectMany(s => s.Services).Sum(s => s.Price);

            var style = new TextStyle().FontFamily(Fonts.Arial).FontColor(Colors.Black).FontSize(10);

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    for (var i = 0; i < headers.Count(); i++)
                    {
                        if (i == 0)
                        {
                            columns.ConstantColumn(50);
                        }
                        else
                        {
                            columns.RelativeColumn();
                        }
                    }
                });

                foreach (var header in headers)
                {
                    BuildCell(table.Cell(), header, style.LineHeight(1f));
                }

                foreach (var emp in employeesWithServices.OrderByDescending(e => e.Services.Count))
                {
                    var countServices = emp.Services.Count;
                    var employeePay = emp.Services.Sum(s => s.Price);

                    BuildCell(table.Cell(), emp.Employee.Id.ToString(), style);
                    BuildCell(table.Cell(), emp.Employee.Fullname, style);
                    BuildCell(table.Cell(), countServices.ToString(), style);
                    BuildCell(table.Cell(), $"{Math.Round((float)countServices / summaryCount * 100, 2)}", style);
                    BuildCell(table.Cell(), employeePay.ToString(), style);
                    BuildCell(table.Cell(), $"{Math.Round(employeePay / summaryPrice * 100, 2)}", style);
                }
            });
        }

        private static IContainer BuildCell(IContainer container, string text, TextStyle style, float border = 0.75f)
        {
            container
                .Border(border)
                .Background(Colors.White)
                .Padding(5)
                .AlignLeft()
                .Text(t =>
                {
                    t.DefaultTextStyle(s => s = style);
                    t.Span($"{text}");
                });

            return container;
        }
    }
}
