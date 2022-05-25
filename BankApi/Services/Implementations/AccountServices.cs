using BankApi.DATA;
using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Services.Implementations
{
    public class AccountServices : IAccountServices
    {
        private BankDbContext _context;
        public AccountServices(BankDbContext context)
        {
            _context = context;
        }

        public Account Authenticate(string AccountNumber, string pin)
        {
            //Lets make Authenticate 
            //Does Account exist for that number?
            var account = _context.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if (account == null)
            {
                return null;
            }
            //when we have a match,
            //Verify pinHash
            if (!VerifyPinHash(pin, account.PinHash, account.PinSalt))
            {
                return null;
            }

            //Authentication is passed 
            return account;
           
        }

        private static bool VerifyPinHash(string Pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(Pin))
            {
                throw new ArgumentNullException("Pin");
            }

            //Lets Verify the pin
            using (var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedPinHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));
                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != pinHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Account Create(Account account, string Pin, string ConfirmPin)
        {
            //This creates a new account
            if (_context.Accounts.Any(x => x.Email == account.Email))
            {
                throw new ApplicationException("Account with this Email already exist");
            }

            if (!Pin.Equals(ConfirmPin))
            {
                throw new ArgumentException("Pins does not match", "Pin");
            }

            //Now, all validation passes, Lets create an account 
            //I am Hashing/Encrypting password first
            byte[] pinHash, pinSalt;

            CreatePinHash(Pin, out pinHash, out pinSalt);

            account.PinHash = pinHash;
            account.PinSalt = pinSalt;

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }


        private static void CreatePinHash(string Pin, out byte[] pinHash, out byte[] pinSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
            }
        }

        public void Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
           return _context.Accounts.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _context.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault();
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public Account GetById(int id)
        {
            var account = _context.Accounts.Where(x => x.Id == id).FirstOrDefault();
            if (account == null )
            {
                return null;
            }
            return account;
        }


        public void Update(Account account, string pin = null)
        {
            //update is more tasky
            var accountToBeUpdated = _context.Accounts.Where(x => x.Email == account.Email).SingleOrDefault();
            if (accountToBeUpdated == null)
            {
                throw new ApplicationException("account does not exist");
            }

            //if it exists, lets listen for user existing to change any of its properties.
            if (!string.IsNullOrWhiteSpace(account.Email))
            {
                //This access the user wishes to change his Email
                //Checks if the one he is changing to is not already taken
                if (_context.Accounts.Any(x => x.Email == account.Email ))
                {
                    throw new ApplicationException("This email" + account.Email + "already exist");
                }
                //else change email for him
                accountToBeUpdated.Email = account.Email;

            }

            //Actually, we want the user to be able to change Email and phoneNumber
            //if it exists, lets listen for user existing to change any of its properties.
            if (!string.IsNullOrWhiteSpace(account.PhoneNumber))
            {
                //This access the user wishes to change his Phone Number
                //Checks if the one he is changing to is not already taken
                if (_context.Accounts.Any(x => x.Email == account.PhoneNumber))
                {
                    throw new ApplicationException("This email" + account.PhoneNumber + "already exist");
                }
                //else change email for him
                accountToBeUpdated.PhoneNumber = account.PhoneNumber;

            }

            if (!string.IsNullOrWhiteSpace(pin))
            {
                //This access the user wishes to change his pin
                //Checks if the one he is changing to is not already taken
                byte[] pinHash, pinSalt;
                CreatePinHash(pin, out pinHash, out pinSalt);
                accountToBeUpdated.PinHash = pinHash;
                accountToBeUpdated.PinSalt = pinSalt;
                //else change email for him
                accountToBeUpdated.PhoneNumber = account.PhoneNumber;

            }
            //now, persist this updaye to the db
            _context.Accounts.Update(accountToBeUpdated);
            _context.SaveChanges();

        }
    }
}
