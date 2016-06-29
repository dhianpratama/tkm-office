using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using Core;
using Core.Models.Master;
using Core.Services;

namespace Service
{
    public class SecurityService : ISecurityService
    {
        private readonly IRepository<MasterUser> _masterUserRepository;
        private readonly IRepository<MasterInstitution> _institutionRepository;

        public SecurityService(IRepository<MasterUser> masterUserRepository, IRepository<MasterInstitution> institutionRepository)
        {
            _masterUserRepository = masterUserRepository;
            _institutionRepository = institutionRepository;
        }


        public MasterUser GetCurrentUser()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var idStr = identity.Claims.FirstOrDefault(i => i.Type == "id");
            long id;
            if (idStr != null)
            {
                id = long.Parse(idStr.Value);
            }
            else
            {
                return null;
            }
            var user = _masterUserRepository.Query().FirstOrDefault(u => u.UserId == id);
            if (user == null) return null;
            user.Password = "";
            user.Salt = "";
            return user;
        }

        public long GetCurrentUserId()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var idStr = identity.Claims.FirstOrDefault(i => i.Type == "id");
            long id;
            if (idStr != null)
            {
                id = long.Parse(idStr.Value);
            }
            else
            {
                return 0;
            }
            return id;
        }

        public long GetCurrentInstitutionId()
        {
            return 1;
        }

        public MasterInstitution GetCurrentUserDefaultInstitution()
        {
            var user = GetCurrentUser();
            if (user == null) return null;

            var defaultInstitution =
                _institutionRepository.Query().FirstOrDefault(i => i.InstitutionId == user.DefaultInstitutionId);
            return defaultInstitution;
        }

        public List<string> GetCurrentUserAcl()
        {
            return new List<string>(new String[] { "test" });
        }
    }
}