using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bank
{
    internal class Client
    {
        public string Name { get; set; }
        [JsonProperty]
        private string pin;
        public Dictionary<string, Account> AccountList { get; set; }

        public Client(string n, string p)
        {
            Name = n;
            pin = p;
            AccountList = new Dictionary<string, Account>();
        }
        public void AddAccount(Account account)
        {
            AccountList.Add(account.AccountNumber, account); //adds new account to client
        }
        public bool LogIn(string pin) //checks inputted pin to compare to client pin
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