using EasyConsole;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Bank
{
    class Program
    {
        static Atm Atm;
        static string dataFile = @"C:\Users\tmorrison\source\repos\Bank\data\data";
        static string transactionLogPath = @"C:\Users\tmorrison\source\repos\Bank\data\transactionLog.txt";

        static void Main(string[] args)
        {
            if (File.Exists(dataFile))
            {
                var fileContents = File.ReadAllText(dataFile);
                Atm = JsonConvert.DeserializeObject<Atm>(fileContents);
            }
            else
            {
                Atm = new Atm();
            }
            var menu = new Menu()
                .Add("login", Login)
                .Add("Create New Account", CreateNewAccount)
                .Add("Exit", () => System.Environment.Exit(0));
            while (true)
            {
                Console.Clear();
                menu.Display();
                SaveData();
            }
        }
        static void SaveData()
        {
            var data = JsonConvert.SerializeObject(Atm);
            File.WriteAllText(dataFile, data);
        }
        static void AppendToLog(string line)
        {
            using (StreamWriter w = File.AppendText(transactionLogPath))
            {
                w.WriteLine(line);
            }
        }
        static void Login()
        {
            var clientName = Input.ReadString("Please enter your user name:");
            Console.WriteLine("Enter pin number:");
            var pin = GetConsolePassword();
            Client lClient;
            if (Atm.clientMap.ContainsKey(clientName))
            {
                 lClient = Atm.clientMap[clientName];
                if (lClient.LogIn(pin))
                {
                    DisplayClientMenu(lClient);
                }
                else
                {
                    Console.WriteLine("Invalid pin");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Cannot find user");
                Console.ReadLine();
            }

        }
        static void CreateNewAccount() 
        {
            var clientName = Input.ReadString("To create a user name, please enter your first and last name:");
            Console.WriteLine("Create a 4 digit pin number:");
            var pin = GetConsolePassword();
            var client = Atm.CreateClient(clientName, pin);  //create client in database
            AppendToLog(String.Format("Account created for {0}", client.Name));
            DisplayClientMenu(client);
        }
        static void DisplayClientMenu(Client mClient) 
        {
            Console.Clear();//display balances and create a new menu to display to logged in client
            foreach(var accountData in mClient.AccountList)
            {
                Console.WriteLine("Your {0} balance is: ${1} ", accountData.Value.Description, accountData.Value.Balance);
            }
            var menu = new Menu()
                .Add("Deposit", () => Deposit(mClient))
                .Add("Withdraw", () => Withdraw(mClient))
                .Add("Transfer", () => Transfer(mClient))
                .Add("Logout", () => Console.WriteLine("Bye"));
            menu.Display();
        }
        static void Deposit(Client dClient)
        {
            Account account;
            var menu = new Menu();
            foreach(var accountData in dClient.AccountList)
            {
                menu.Add(accountData.Value.Description, () => {
                    account = accountData.Value;
                    var amount = Input.ReadString("Enter deposit amount");
                    account.Deposit((float)Convert.ToDouble(amount));
                    AppendToLog(String.Format("Deposited ${0} to {1}'s {2} account", amount, dClient.Name, account.Description));
                    DisplayClientMenu(dClient);
                });
            }
            menu.Display();

        }
        static void Withdraw(Client dClient)
        {
            Account account;
            var menu = new Menu();
            foreach (var accountData in dClient.AccountList)
            {
                menu.Add(accountData.Value.Description, () => {
                    account = accountData.Value;
                    var amount = Input.ReadString("Enter withdraw amount");
                    account.Withdraw((float)Convert.ToDouble(amount));
                    AppendToLog(String.Format("Withdrew ${0} from {1}'s {2} account", amount, dClient.Name, account.Description));

                    DisplayClientMenu(dClient);
                });
            }
            menu.Display();

        }
        static void Transfer(Client dClient)
        {
            Account account;
            var menu = new Menu();
            foreach (var accountData in dClient.AccountList)
            {
                menu.Add(accountData.Value.Description, () => {
                    account = accountData.Value;
                    var amount = Input.ReadString("Enter deposit amount");
                    var to = Input.ReadString("Please enter the name or account number you wish to transfer to.");
                    Atm.Transfer(account.AccountNumber, to, (float)Convert.ToDouble(amount));
                    AppendToLog(String.Format("Transfered ${0} from {1}'s {2} account to {3}", amount, dClient.Name, account.Description, to));

                    DisplayClientMenu(dClient);
                });
            }
            menu.Display();

        }
        private static string GetConsolePassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');  //thanks Charley! lol
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }
    }
}
