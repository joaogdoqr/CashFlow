﻿using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository) : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadOnlyRepository _repository = repository;

    private const string CURRENCY_SYMBOL = "€";

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _repository.FilterByDate(date);

        if(expenses.Count == 0)
            return [];

        using var workbook = new XLWorkbook();

        workbook.Author = "CashFlow";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var yearMonth = date.ToString("Y");

        var worksheet = workbook.Worksheets.Add(yearMonth);

        InsertHeader(worksheet);

        var raw = 2;
        foreach(var expense in expenses)
        {
            worksheet.Cell($"A{raw}").Value = expense.Title;
            worksheet.Cell($"B{raw}").Value = expense.Date;
            worksheet.Cell($"C{raw}").Value = ConvertPaymentType(expense.PaymentType);
            worksheet.Cell($"D{raw}").Value = expense.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";
            worksheet.Cell($"E{raw}").Value = expense.Description;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var pathInMemory = new MemoryStream();

        workbook.SaveAs(pathInMemory);

        return pathInMemory.ToArray();
    }

    private static string ConvertPaymentType(PaymentType payment)
    {
        return payment switch
        {
            PaymentType.Cash => ResourcePaymentType.CASH,
            PaymentType.CreditCard => ResourcePaymentType.CREDIT_CARD,
            PaymentType.DebitCard => ResourcePaymentType.DEBIT_CARD,
            PaymentType.ElectronicTransfer => ResourcePaymentType.ELETRONIC_TRANSFER,
            _ => ""
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
