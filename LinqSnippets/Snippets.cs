using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars = { 
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            // 1. SELECT * of Cars
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE Car is Audi
            var audiList = from car in cars
                           where car.Contains("Audi")
                           select car;

            foreach (var car in audiList)
            {
                Console.WriteLine(car);
            }
        }

        static public void LinqNumbers() 
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each number multiplied by 3
            // take all number, but 9
            // order numbers by ascending value

            var processedNumberList = numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);
        }

        static public void SearchExamples() 
        {
            List<string> textList = new List<string>
            {
                "a", "bx",
                "c", "d", "e", "cj", "f", "c"
            };

            // 1. First of all elements
            var first = textList.First();

            // 2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains "z" or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            // 5. Last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            // 6. Single Values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain { 4, 8 }
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            string[] myOptions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };

            var myOpinionSelection = myOptions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise(){ 
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]{
                        new Employee
                        { 
                            Id = 1,
                            Name = "Juan",
                            Email = "juan@mail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "William",
                            Email = "william@mail.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Mariana",
                            Email = "mariana@mail.com",
                            Salary = 4000
                        }
                    },
                },
                new Enterprise(){
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]{
                        new Employee
                        {
                            Id = 4,
                            Name = "Juan",
                            Email = "juan@mail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "William",
                            Email = "william@mail.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Mariana",
                            Email = "mariana@mail.com",
                            Salary = 4000
                        }
                    },
                }
            };

            // Obtain all Employees of all Enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if ant list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least has an employee with at least 1000 of salary
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                enterprise.Employees.Any(employee => employee.Salary >= 1000)
                );
        }

        static public void LinqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN
            var commonResult = from firstElement in firstList
                               join secondElement in secondList
                               on firstElement equals secondElement
                               select new { firstElement, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                firstElement => firstElement,
                secondElement => secondElement,
                (firstElement, secondElement) => new { firstElement, secondElement }
                );

            //OUTER JOIN - LEFT
            var leftOuterJoin = from firstElement in firstList
                                join secondElement in secondList
                                on firstElement equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where firstElement != temporalElement
                                select new { Element = firstElement };

            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join firstElement in firstList
                                 on secondElement equals firstElement
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[] {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP
            var skipTwoFirstValues = myList.Skip(2);

            var skipLastTwoValues = myList.SkipLast(2);

            var skipWhileSmallerThan = myList.SkipWhile(num => num < 4);

            // TAKE
            var takeTwoFirstValues = myList.Take(2);
            
            var takeLastTwoValues = myList.Take(2);

            var takeWhileSmallerThan = myList.TakeWhile(num => num < 4);

            // TODO:

            // VARIABLES

            // ZIP

            // REPEAT

            // ALL

            //AGREGATE

            // DISTINCT

            // GROUP BY
        }
    }
}
