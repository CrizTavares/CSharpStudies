using System;
using Entities;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Studies
{
    public class Program
    {
        static void Main(string[] args)
        {
            //File Example
            //c:\temp\in.txt
            //Maria,maria @gmail.com,3200.00
            //Alex,alex @gmail.com,1900.00
            //Marco,marco @gmail.com,1700.00
            //Bob,bob @gmail.com,3500.00
            //Anna,anna @gmail.com,2800.00

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            try
            {
                if (path.Length > 0)
                {
                    List<Employee> list = new List<Employee>();

                    using (StreamReader sr = File.OpenText(path))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] fields = sr.ReadLine().Split(",");
                            string name = fields[0];
                            string email = fields[1];
                            double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                            list.Add(new Employee(name, email, salary));
                        }
                    }

                    Console.Write("Enter salary: ");
                    double filter = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    Console.WriteLine($"Email of people whose salary is more than {filter.ToString("F2")}:");

                    var result = list.Where(x => x.Salary > filter).OrderBy(x => x.Email).Select(x => x.Email);

                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    throw new Exception("Empty file");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error: {e.Message}!");
            }
        }
    }
}
