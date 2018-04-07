namespace Bank
{
    internal class Account
    {
        private string accountNumber;
        public string AccountNumber { get { return accountNumber; } } // public getter for account number to keep data private (error handling)
        private string description;
        public string Description { get { return description; } }
        private float balance = 0;
        public float Balance { get { return balance; } } // public getter for balance to keep private (error handling)
        private double interestRate; 
        public double InterestRate { get { return interestRate; } } // public getter for interestrate (error handling)

        public Account(string actNum, string desc, double intRate)
        {
            accountNumber = actNum; // assign incoming parameter to the classes field
            description = desc; //assign incoming parameter to the classes field 
            interestRate = intRate; // assign incoming parameter to the classes field
        }
        public void Deposit(float amount) //depositing amount
        {
            balance = balance + amount;
        }
        public bool Withdraw(float amount) //withdrawing amount, don't let balance go over 0
        {
            if(amount > balance)
            {
                return false; //withdraw unsuccessful
            }
            balance = balance - amount;
            return true; //withdraw successful

        }
    }
}