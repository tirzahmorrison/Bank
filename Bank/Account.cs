namespace Bank
{
    internal class Account
    {
        public string AccountNumber { get; set; } // public getter for account number to keep data private (error handling)
        public string Description { get; set; }
        public float Balance { get; set; } // public getter for balance to keep private (error handling)
        public double InterestRate { get; set; } // public getter for interest rate (error handling)

        public Account(string actNum, string desc, double intRate)
        {
            AccountNumber = actNum; // assign incoming parameter to the classes field
            Description = desc; //assign incoming parameter to the classes field 
            InterestRate = intRate; // assign incoming parameter to the classes field
            Balance = 0;
        }
        public void Deposit(float amount) //depositing amount
        {
            Balance = Balance + amount;
        }
        public bool Withdraw(float amount) //withdrawing amount, don't let balance go over 0
        {
            if (amount >Balance)
            {
                return false; //withdraw unsuccessful
            }
            Balance = Balance - amount;
            return true; //withdraw successful

        }
    }
}