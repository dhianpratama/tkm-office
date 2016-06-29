using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services
{
    public interface ISecurityService
    {
        MasterUser GetCurrentUser();
        long GetCurrentInstitutionId();
        MasterInstitution GetCurrentUserDefaultInstitution();
        List<string> GetCurrentUserAcl();
        long GetCurrentUserId();
    }
}