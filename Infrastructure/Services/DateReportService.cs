using Domain.Entities;
using Domain.Repository;

namespace Infrastructure.Services;

public class DateReportService : IDateReportService
{
    private readonly IRepository<FinanceOperation> _financeOperationsRepository;
    private readonly IRepository<IncomeExpenseCategory> _incomeExpensesRepository;

    public DateReportService(IRepository<FinanceOperation> financeOperationsRepository, IRepository<IncomeExpenseCategory> incomeExpensesRepository)
    {
        _financeOperationsRepository = financeOperationsRepository ?? throw new ArgumentNullException(nameof(financeOperationsRepository));
        _incomeExpensesRepository = incomeExpensesRepository ?? throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }

    public Task<DateReport> GetDailyReport(DateTime date)
    {
        if (date == null)
        {
            throw new ArgumentNullException(nameof(date));
        }

        DateReport report = new DateReport();
        List<FinanceOperation> operationsList = new List<FinanceOperation>();
        
        foreach (var operation in _financeOperationsRepository.GetAll().Result)
        {
            if (operation.Date.ToShortDateString() == date.ToShortDateString())
            {
                operationsList.Add(operation); //Adding operation to the list of all operations on this date
                GetTotalIncomeExpense(report, operation);
            }
        }

        report.Operations = operationsList;

        return Task.FromResult(report);
    }

    public Task<DateReport> GetDatePeriodReport(DateTime startDate, DateTime endDate)
    {
        if (startDate == null)
        {
            throw new ArgumentNullException(nameof(startDate));
        }
        if (endDate == null)
        {
            throw new ArgumentNullException(nameof(endDate));
        }
        
        DateReport report = new DateReport();
        List<FinanceOperation> operationsList = new List<FinanceOperation>();


        foreach (var operation in _financeOperationsRepository.GetAll().Result)
        {
            if (operation.Date >= startDate && operation.Date <= endDate)
            {
                operationsList.Add(operation); //Adding operation to the list of all operations on this date
                GetTotalIncomeExpense(report, operation);
            }
        }
        
        report.Operations = operationsList;
        
        return Task.FromResult(report);
    }


    private void GetTotalIncomeExpense(DateReport report, FinanceOperation financeOperation)
    {
        var category = _incomeExpensesRepository.Get(financeOperation.CategoryId);
        if (category.Result.FinanceActivityType == FinanceActivityEnum.Income)
        {
            report.TotalIncome += financeOperation.Amount;
        }
        else 
        {
            report.TotalExpense += financeOperation.Amount;
        } 
    }
}