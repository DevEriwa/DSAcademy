using Core.Db;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DSAcademy.Controllers
{
	public class AccountsController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUserHelper _userHelper; 

		public AccountsController
			(AppDbContext context,
			UserManager<ApplicationUser> userManger,
			SignInManager<ApplicationUser> signInManager,
			IUserHelper userHelper)
		{
			_context = context;
			_userManager = userManger;
			_signInManager = signInManager;
			_userHelper = userHelper;
		}

		[HttpGet]
		public IActionResult RegisterAdmin()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AdminRegisteration()
		{
			return View();
		}

		[HttpGet]
		public IActionResult RegisterStudent()
		{
			return View();
		}

		[HttpPost]
		public IActionResult StudentRegisteration()
		{
			return View();
		}
		
		
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
	}
}
