using Logic.IHelpers;
using System;
using System.Collections.Generic;
using Core.Models;
using System.Linq;
using Core.Enum;
using System.Threading.Tasks;
using Core.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Helpers
{
    public class DropdownHelper : IDropdownHelper
    {
        private readonly AppDbContext _context;

        public DropdownHelper(AppDbContext context)
        {
            _context = context;
        }

        public List<CommonDropDown> GetDropDownEnumsList()
        {
            var common = new CommonDropDown()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var enumList = ((YesNoEnum[])Enum.GetValues(typeof(YesNoEnum))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            enumList.Insert(0, common);
            return enumList;
        }
		public List<CommonDropDown> GetProgramEnumsList()
		{
			var common = new CommonDropDown()
			{
				Id = 0,
				Name = "-- Select --"
			};
			var enumList = ((ProgramEnum[])Enum.GetValues(typeof(ProgramEnum))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
			enumList.Insert(0, common);
			return enumList;
		}
		public List<CommonDropDown> GetApplicationStatusDropDownEnumsList()
        {
            var common = new CommonDropDown()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var enumList = ((ApplicationStatus[])Enum.GetValues(typeof(ApplicationStatus))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            enumList.Insert(0, common);
            return enumList;
        }

        public List<TrainingCourse> DropdownOfCourses()
        {
            try
            {
                var common = new TrainingCourse()
                {
                    Id = 0,
                    Title = "Select Course"
                };
                var listOfCourses = _context.TrainingCourse.Where(x => x.Id != 0 && x.IsActive == true && x.IsDeleted == false).OrderBy(p => p.Title).ToList();
                listOfCourses.Insert(0, common);
                return listOfCourses;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<CommonDropDown> DropdownOfCoursesWhereIsTested(string userId)
        {
            try
            {
                var common = new CommonDropDown()
                {
                    Id = 0,
                    Name = "Select Course"
                };
                var listOfTestTaken = _context.TestResults.Where(x => x.UserId == userId).Include(x => x.Course).ToList();
                var drp = listOfTestTaken.Select(x => new CommonDropDown
                {
                    Id = x.Course.Id,
                    Name = x.Course.Title,
                }).ToList();


                drp.Insert(0, common);
                return drp;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<CommonDropDown> JobTypes()
        {
            var newJobTypeList = new List<CommonDropDown>();
            var common = new CommonDropDown()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var jobList = ((JobType[])Enum.GetValues(typeof(JobType))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            foreach (var jobType in jobList)
            {
                if (jobType.Name == "Full_Time_Premise")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Full-time (On Premise)";
                    newJobTypeList.Add(newCommon);
                }
                else if (jobType.Name == "Part_Time_Premise")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Part-time (On Premise)";
                    newJobTypeList.Add(newCommon);
                }
                else if (jobType.Name == "Full_Time_Home")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Full-time (On Home)";
                    newJobTypeList.Add(newCommon);
                }
                else
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Part-time (On Home)";
                    newJobTypeList.Add(newCommon);
                }
            }
            newJobTypeList.Insert(0, common);
            return newJobTypeList;
        }
        public List<CommonDropDown> JobTypesForSearch()
        {
            var newJobTypeList = new List<CommonDropDown>();
            var common = new CommonDropDown()
            {
                Id = 0,
                Name = "-- Select job type --"
            };
            var jobList = ((JobType[])Enum.GetValues(typeof(JobType))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            foreach (var jobType in jobList)
            {
                if (jobType.Name == "Full_Time_Premise")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Full-time (On Premise)";
                    newJobTypeList.Add(newCommon);
                }
                else if (jobType.Name == "Part_Time_Premise")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Part-time (On Premise)";
                    newJobTypeList.Add(newCommon);
                }
                else if (jobType.Name == "Full_Time_Home")
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Full-time (On Home)";
                    newJobTypeList.Add(newCommon);
                }
                else
                {
                    var newCommon = new CommonDropDown();
                    newCommon.Id = jobType.Id;
                    newCommon.Name = "Part-time (On Home)";
                    newJobTypeList.Add(newCommon);
                }
            }
            newJobTypeList.Insert(0, common);
            return newJobTypeList;
        }
        public List<CommonDropDown> GetExamTypeList()
        {
            var common = new CommonDropDown()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var enumList = ((ExamType[])Enum.GetValues(typeof(ExamType))).Select(c => new CommonDropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            enumList.Insert(0, common);
            return enumList;
        }
		public List<Company> GetAllCompany()
		{
			var listOfCompany = new List<Company>();
			var common = new Company()
			{
				Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
				Name = "Switch Branch"
			};
			var loggedInUser = Session.GetCurrentUser();
			var branches = _context.Companies.Where(x => !x.Deleted && x.Id == loggedInUser.CompanyId).ToList();
			branches.Insert(0, common);
			return branches;
		}

	}
}
