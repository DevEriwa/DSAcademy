using Core.Models;

namespace Logic.IHelpers
{
    public interface IDropdownHelper
    {
        List<CommonDropDown> GetDropDownEnumsList();
        List<CommonDropDown> GetApplicationStatusDropDownEnumsList();
        List<TrainingCourse> DropdownOfCourses();
        List<CommonDropDown> DropdownOfCoursesWhereIsTested(string userId);
        List<CommonDropDown> JobTypes();
        List<CommonDropDown> JobTypesForSearch();
        List<CommonDropDown> GetExamTypeList();
        List<Company> GetAllCompany();
        List<CommonDropDown> GetProgramEnumsList();
	}
}
