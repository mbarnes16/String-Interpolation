
  
using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "data.txt";
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arr = line.Split(",");
                        DateTime date = DateTime.Parse(arr[0]);
                        int[] hoursOfSleep = Array.ConvertAll(arr[1].Split("|"), int.Parse);
                        int total = 0;
                        foreach (var item in hoursOfSleep)
                        {
                            total += item;
                        }
                        double average = (double)total / 7;  
                        Console.WriteLine($"Week ending in {date:MMM} {date:%d}, {date:yyyy}");
                        string[] days = { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su", "Tot", "Avg" };
                        Console.WriteLine($"{days[0],3} {days[1],3} {days[2],3} {days[3],3} {days[4],3} {days[5],3} {days[6],3} {days[7],4} {days[8],4}");
                        Console.WriteLine(" --  --  --  --  --  --  --  ---  ---");
                        Console.WriteLine($"{hoursOfSleep[0],3} {hoursOfSleep[1],3} {hoursOfSleep[2],3} {hoursOfSleep[3],3} {hoursOfSleep[4],3} {hoursOfSleep[5],3} {hoursOfSleep[6],3} {total,4} {average, 4:n1}");
                        Console.WriteLine("");
                    }
                }

            }
        }
    }
}