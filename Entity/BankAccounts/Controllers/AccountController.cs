using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BankAccounts.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Globalization;

namespace BankAccounts.Controllers
{
    public class AccountController : Controller
    {
        public NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
        BankContext _context;
        public AccountController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("account/new")]
        public IActionResult NewAccount()
        {
            TempData["id"] = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpPost]
        [Route("account/create")]
        public IActionResult CreateAccount(AccountViewModel account, int UserId)
        {
            if(!ModelState.IsValid)
            {
                return View("NewAccount");
            }
            else
            {
                User AccountHolder = _context.Users.SingleOrDefault(user => user.UserId == UserId);
                Account NewAccount = new Account
                {
                    AccountType = account.AccountType,
                    AccountNickname = account.AccountNickname,
                    User = AccountHolder
                };

                _context.Add(NewAccount);
                _context.SaveChanges();
                return RedirectToAction("UserHome", "User");
            }
        }

        [HttpGet]
        [Route("account/{id}")]
        public IActionResult ShowAccount(int id)
        {
            TempData["id"] = HttpContext.Session.GetInt32("UserId");
            Account ShowAccount = _context.Accounts.SingleOrDefault(account => account.AccountId == id);
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == (int)TempData["id"]);
            if(ShowAccount.UserId != CurrentUser.UserId)
            {
                return RedirectToAction("UserHome", "User");
            }
            ViewBag.UserId = ShowAccount.UserId;
            ViewBag.AccountId = ShowAccount.AccountId;
            ViewBag.AccountType = ShowAccount.AccountType;
            ViewBag.AccountBalance = ShowAccount.AccountBalance.ToString("C", nfi);
            ShowAccount.AccountBalance.ToString("C", CultureInfo.CurrentCulture);
            List<Transaction> AllTransactions = _context.Transactions.Include(transaction => transaction.Account).ToList();
            ViewBag.Transactions = AllTransactions;
            return View();
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult Transact(Transaction NewTransaction, int AcctId, int UserId)
        {
            Account UpdateAccount = _context.Accounts.SingleOrDefault(account => account.AccountId == AcctId);
            float BalanceCheck = UpdateAccount.AccountBalance + NewTransaction.TransAmount;
            if(BalanceCheck < 0)
            {
                TempData["error"] = "You have insufficient funds.";
            }
            else
            {
                Transaction transaction = new Transaction
                {
                    UserID = UserId,
                    AccountId = AcctId,
                    TransAmount = NewTransaction.TransAmount,
                    Date = DateTime.Now
                };
                UpdateAccount.AccountBalance += NewTransaction.TransAmount;
                _context.Add(transaction);
                _context.SaveChanges();
            }
            return Redirect($"/account/{AcctId}");
        }
    }
}