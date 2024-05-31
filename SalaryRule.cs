using AplikacjaWyplat.Models;
using AplikacjaWyplat.Pages.Enums;
using NRules;
using NRules.Fluent;
using NRules.Fluent.Dsl;

public class SalaryRule : Rule
{
    public override void Define()
    {
        Employer employee = null;

        When()
            .Match(() => employee);

        Then()
            .Do(ctx => CalculateTotalSalary(employee));
    }

    private void CalculateTotalSalary(Employer employee)
    {
        switch (employee.jobPosition)
        {
            case JobPosition.Manager:
                employee.Salary = 8000;
                break;
            case JobPosition.Programmer:
                employee.Salary = 6000;
                break;
            case JobPosition.Analyst:
                employee.Salary = 5500;
                break;
            case JobPosition.PM:
                employee.Salary = 7500;
                break;
            case JobPosition.Tester:
                employee.Salary = 5000;
                break;
        }

        // Oblicz całkowitą pensję
        double delegationBonus = employee.NumberOfDelegation * 100; // Przykładowy bonus za delegację
        double overtimeBonus = employee.Overtime * 50;     // Przykładowy bonus za nadgodziny

        employee.TotalSalary = employee.Salary + delegationBonus + overtimeBonus;
    }

    public string main()
    {
        var repository = new RuleRepository();
        repository.Load(x => x.From(typeof(SalaryRule).Assembly));

        var factory = repository.Compile();
        var session = factory.CreateSession();

        // Przykładowy pracownik
        var employee = new Employer
        {
            jobPosition = JobPosition.Programmer,
            NumberOfDelegation = 5,
            Overtime = 10
        };

        // Wstaw pracownika do sesji i uruchom reguły
        session.Insert(employee);
        session.Fire();

        // Wyświetl obliczoną pensję
        string answ = $"Total Salary for {employee.jobPosition}: {employee.TotalSalary}";

        return answ;
    }

}
