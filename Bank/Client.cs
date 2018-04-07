using System.Collections.Generic;
using System.Security;

namespace Bank
{
    internal class Client
    {
        private string name;
        public string Name { get { return name; } }
        private string pin;
        private Dictionary<string, Account> accountList = new Dictionary<string, Account>();
        public Dictionary<string, Account> AccountList { get { return accountList; } }

        public Client(string n, string p)
        {
            name = n;
            pin = p;
        }
        public void AddAccount(Account account)
        {
            accountList.Add(account.AccountNumber, account); //adds new account to client
        }
        public bool LogIn(SecureString pin) //checks inputted pin to compare to client pin
        {
            if (pin.Equals(this.pin))
            {
                return true; //return correct pin
            }
            else
            {
                return false; //return incorrect pin
            }
        }
    }
}