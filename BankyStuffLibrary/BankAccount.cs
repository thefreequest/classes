using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Humanizer;

namespace BankyStuffLibrary
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance 
        { 
            get
            {
                decimal balance = 0;
              
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }


        /*
        --------------------------------------------------------------------------------------------------------------------
        This is a data member. It's private, which means it can only be accessed by code inside the BankAccount class. 
        It's a way of separating the public responsibilities (like having an account number) from the private implementation 
        (how account numbers are generated). 
        It is also static, which means it is shared by all of the BankAccount objects. 
        The value of a non-static variable is unique to each instance of the BankAccount object. 
        ---------------------------------------------------------------------------------------------------------------------
        */
        private static int accountNumberSeed = 1234567890;

        //Declare a List of Transaction objects to hold a the list transactions for a given account
        private List<Transaction> allTransactions = new List<Transaction>();

        //Constructor of the class
        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
        }


        public void MakeDeposit(decimal amount, DateTime date, string note) 
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

 
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            /*
            This uses the StringBuilder class to format a string that contains one line for each transaction. 
            You've seen the string formatting code earlier in these tutorials. 
            One new character is \t. That inserts a tab to format the output.
            */
            var report = new System.Text.StringBuilder();
            
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            report.AppendLine("----\t\t------\t-------\t----");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.AmountForHumans}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }
    }
}
