using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.DAL
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var departments = new List<Department>
            {
                new Department{Name="Администрация"},
                new Department{Name="Общий отдел"},
                new Department{Name="ИТ отдел"},
                new Department{Name="Бухгалтерия"},
                new Department{Name="Отдел продаж"}
            };

            departments.ForEach(d => context.Departments.Add(d));
            context.SaveChanges();

            var kindsOfExpenses = new List<KindOfExpenses>
            {
                new KindOfExpenses{Name="Производственные расходы", Description="Расходы на обеспечение основной деятельности"},
                new KindOfExpenses{Name="Коммерческие расходы", Description=""},
                new KindOfExpenses{Name="Непроизводственные расходы", Description=""},
                new KindOfExpenses{Name="Инвестиционные расходы ", Description="Расходы на воспроизводство основного и увеличение оборотного капитала"},
                new KindOfExpenses{Name="Общехозяйственные расходы", Description=""}
            };

            kindsOfExpenses.ForEach(k => context.KindsOfExpenses.Add(k));
            context.SaveChanges();

            var kindsOfExpensesDepartmentRelations = new List<KindOfExpensesDepartmentRelation>
            {
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=1, DepartmentID=1, Limit=250000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=1, DepartmentID=2, Limit=150000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=1, DepartmentID=3, Limit=150000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=2, DepartmentID=1, Limit=100000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=4, DepartmentID=4, Limit=500000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=5, DepartmentID=2, Limit=100000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=3, DepartmentID=3, Limit=50000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=3, DepartmentID=2, Limit=50000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=2, DepartmentID=5, Limit=150000},
                new KindOfExpensesDepartmentRelation{KindOfExpensesID=5, DepartmentID=5, Limit=50000}
            };

            kindsOfExpensesDepartmentRelations.ForEach(rel => context.KindOfExpensesDepartmentRelations.Add(rel));
            context.SaveChanges();
            
            var employees = new List<Employee>
            {
                new Employee{LastName="Смирнов",FirstName="Алексей", MiddleName="Иванович", DepartmentID=2},
                new Employee{LastName="Алексеев",FirstName="Вячеслав", MiddleName="Игоревич", DepartmentID=1},
                new Employee{LastName="Соловьев",FirstName="Максим", MiddleName="Петрович", DepartmentID=3},
                new Employee{LastName="Савельев",FirstName="Константин", MiddleName="Сергеевич", DepartmentID=2},
                new Employee{LastName="Зацепин",FirstName="Сергей", MiddleName="Львович", DepartmentID=4},
                new Employee{LastName="Аверьянов",FirstName="Юрий", MiddleName="Константинович", DepartmentID=3},
                new Employee{LastName="Петров",FirstName="Лев", MiddleName="Владимирович", DepartmentID=2},
                new Employee{LastName="Сергеев",FirstName="Сергей", MiddleName="Сергеевич", DepartmentID=5},
                new Employee{LastName="Иванов",FirstName="Михаил", MiddleName="Васильевич", DepartmentID=1},
                new Employee{LastName="Баранов",FirstName="Иван", MiddleName="Петрович", DepartmentID=5}

            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();

            var expenses = new List<Expense>
            {
                new Expense{EmployeeID=1, KindOfExpensesID=1, Date=DateTime.Parse("2015-03-01"), Summ=1500},
                new Expense{EmployeeID=2, KindOfExpensesID=1, Date=DateTime.Parse("2015-03-07"), Summ=2578.55m},
                new Expense{EmployeeID=3, KindOfExpensesID=1, Date=DateTime.Parse("2015-03-11"), Summ=2500},
                new Expense{EmployeeID=4, KindOfExpensesID=5, Date=DateTime.Parse("2015-03-22"), Summ=7412},
                new Expense{EmployeeID=5, KindOfExpensesID=4, Date=DateTime.Parse("2015-03-22"), Summ=952.31m},
                new Expense{EmployeeID=6, KindOfExpensesID=1, Date=DateTime.Parse("2015-03-27"), Summ=4500},
                new Expense{EmployeeID=7, KindOfExpensesID=5, Date=DateTime.Parse("2015-04-08"), Summ=250},
                new Expense{EmployeeID=8, KindOfExpensesID=2, Date=DateTime.Parse("2015-04-11"), Summ=1000},
                new Expense{EmployeeID=9, KindOfExpensesID=2, Date=DateTime.Parse("2015-04-25"), Summ=2400},
                new Expense{EmployeeID=10, KindOfExpensesID=5, Date=DateTime.Parse("2015-04-27"), Summ=5500}
            };
            expenses.ForEach(exp => context.Expenses.Add(exp));
            context.SaveChanges();
        }
    }
}