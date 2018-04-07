using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Atm
    {
        private Dictionary<string, Account> AccountMap = new Dictionary<string, Account>();
        private Dictionary<string, Client> ClientMap = new Dictionary<string, Client>();
        private string transactionLogPath = @"C:\Users\tmorrison\source\repos\Bank\data\transactionLog.txt";
        private FileStream transactionLog;

        public Atm()
        {
            transactionLog = File.Open(transactionLogPath, FileMode.OpenOrCreate); //opening a file to add lines to the end
        }
        public void CreateAccount(Client c, string description, double intRate)
        {
            var rnd = new Random();
            Account newAccount;
            string num;
            do
            {
                num = rnd.Next(30100000, 30199999).ToString();
            } while (AccountMap.ContainsKey(num));
            newAccount = new Account(num, description, intRate);
            c.AddAccount(newAccount);
        }
    }
}


