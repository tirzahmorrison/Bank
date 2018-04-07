using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
           string dataFile = @"C:\Users\tmorrison\source\repos\Bank\data\data";
            Atm Atm;

            if (File.Exists(dataFile))
            {
                var fileContents = File.ReadAllText(dataFile);
                Atm = JsonConvert.DeserializeObject<Atm>(fileContents);
            }
            else
            {
                Atm = new Atm();
            }
        }
    }
}
