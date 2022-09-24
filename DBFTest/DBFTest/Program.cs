using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDbfReader;
using dBASE.NET;
using System.IO;

namespace DBFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            string path = @"E:\DBF\test.dbf";
            try
            {
                if (File.Exists(path))
                {
                    // чтение dbf с помощью NDbfReader
                    using (var table = Table.Open(path))
                    {
                        var reader = table.OpenReader(Encoding.UTF8);
                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                Name = reader.GetValue("NAME").ToString(),
                                Age = Convert.ToInt32(reader.GetValue("AGE"))
                            });
                        }
                    }
                    // запись в dbf с помощью dBASE.NET
                    Dbf dbf = new Dbf();
                    dbf.Read(path);
                    DbfRecord record = dbf.CreateRecord();
                    record.Data[0] = "Petr";
                    record.Data[1] = 25;
                    dbf.Write(path);
                    persons.ForEach(p => Console.WriteLine(p.Name + " " + p.Age));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
