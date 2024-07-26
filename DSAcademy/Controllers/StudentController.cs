using Core.Db;
using Core.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Logic.AppHttpContext;

namespace DSAcademy.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDropdownHelper _dropdownHelper;
        private readonly IApplicationHelper _applicationHelper;
        private readonly IUserHelper _userHelper;
        private readonly IStudentHelper _studentHelper;

        public StudentController
            (AppDbContext context,
            UserManager<ApplicationUser> userManger,
            SignInManager<ApplicationUser> signInManager,
            IDropdownHelper dropdownHelper,
            IApplicationHelper applicationHelper,
            IUserHelper userHelper,
            IStudentHelper studentHelper)
        {
            _context = context;
            _userManger = userManger;
            _signInManager = signInManager;
            _dropdownHelper = dropdownHelper;
            _applicationHelper = applicationHelper;
            _userHelper = userHelper;
            _studentHelper = studentHelper;
        }
        public IActionResult Index()
        {
            var dashBoardData = _studentHelper.DashboardBuildingServices();
            return View(dashBoardData);
        }
        //Applicants GET ACTION
    }
}
