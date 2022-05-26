using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Services.Interface
{
    public interface ITransactionServices
    {
        Response CreateNewTransaction(Transaction transaction);

        Response FindTransactionByDate(DateTime date);
        Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeWithdrawl(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeFundsTransfer(string FromAccountNumber, string ToAcccountNumber, double Amount, string TransactionPin);
    
    }
}
