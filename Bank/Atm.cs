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
            AccountMap.Add(newAccount.AccountNumber, newAccount); //adds new account to account
        }
        public Client CreateClient(string name, string pin)
        {
            if(ClientMap.ContainsKey(name))
            {
                return null;    //don't allow duplicate client names
            }
            var c = new Client(name, pin);
            CreateAccount(c, "Checking", 0); //create checking
            CreateAccount(c, "Savings", 1.2);  //create savings
            ClientMap.Add(name, c);
            return c;
        }
        public bool Transfer(string from, string to, float amount)
        {
            var fromAccount = AccountMap[from]; //look up source account
            Account toAccount;
            if (AccountMap.ContainsKey(to)) //if client enters account# 
            {
                toAccount = AccountMap[to]; //look up account number in map
            }
            else if(ClientMap.ContainsKey(to))
            {
                toAccount = ClientMap[to].AccountList.Where(a => a.Value.Description == "Checking").FirstOrDefault().Value; //defaults to clients checking account if client name is passed in
            }
            else
            {
                return false; //cannot find account numer or client name
            }
            if (fromAccount.Withdraw(amount))
            {
                toAccount.Deposit(amount); //deposit successful
                return true;
            }
            return false; //deposit not successful
        }
    }
}


