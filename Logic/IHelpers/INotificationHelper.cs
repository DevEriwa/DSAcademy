using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
	public interface INotificationHelper
	{
		Task<CustomNotificationViewModel> GetNotificationSettings(Guid? branchId);
		CustomNotificationViewModel GetCompanyCustomSettings(Guid companyBranchId, CustomNotificationViewModel customNotificationViewModel, List<string> checkedAdminSettings, List<string> uncheckedAdminSettings);
    }
}
