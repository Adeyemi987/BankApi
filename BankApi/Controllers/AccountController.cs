using AutoMapper;
using BankApi.Models;
using BankApi.Models.DTO;
using BankApi.Profiles.DTO;
using BankApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //Inject Account Services
        private readonly IAccountServices _accountServices;
        IMapper _mapper;
        public AccountController(IAccountServices accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
        }

        //RegisterNew Account
        [HttpPost]
        [Route("create_new_account")]
        public IActionResult RegisterNewAccount([FromBody] RegisterAccDTO newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newAccount);
            }

            //map to the Account
            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountServices.Create(account, newAccount.Pin, newAccount.ConfirmPin));
        }

        //GetAllACcount
        [HttpGet]
        [Route("get_All_Accounts")]
        public IActionResult GetAllAccounts()
        {
            var account = _accountServices.GetAllAccounts();
            var cleanedAccounts = _mapper.Map<IList<GetAccountDTO>>(account);
            return Ok(cleanedAccounts);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthAccDTO authAccDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(authAccDTO);

            }
            //Lets map
            return Ok(_accountServices.Authenticate(authAccDTO.AccountNumber, authAccDTO.Pin));
        }
    }
}
