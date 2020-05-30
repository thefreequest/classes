using System;
using BankyStuffLibrary;


namespace classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount("Adan", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance}  initial balance.");
            account.MakeDeposit(300, DateTime.Now, "Deposit one");
            Console.WriteLine($"Account {account.Number} for {account.Owner} has {account.Balance} after last transaction.");
            account.MakeWithdrawal(200, DateTime.Now, "Withdrawal one");
            Console.WriteLine($"Account {account.Number} for {account.Owner} has {account.Balance} after last transaction.");
            
            //When you are executing code than can throw exceptions sorround it with try/catch
            try
            {
                account.MakeWithdrawal(400, DateTime.Now, "Withdrawal two");
                Console.WriteLine($"Account {account.Number} for {account.Owner} has {account.Balance} after last transaction.");
            }
            catch (InvalidOperationException e)
            {

                Console.WriteLine($"Exception caught, Attempt to overdraw, this withdraw exceeded your balance({account.Balance})");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine(account.GetAccountHistory());

        }

    }
}
