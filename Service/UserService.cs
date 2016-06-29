using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Core;
using Core.Models.Master;
using Core.Services;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<MasterUser> _masterUserRepository;
        private readonly ISecurityService _securityService;
        private readonly IRepository<MasterUserInstitution> _masterUserInstitutionRepository;

        public UserService(IRepository<MasterUser> masterUserRepository, ISecurityService securityService,
            IRepository<MasterUserInstitution> masterUserInstitutionRepository)
        {
            _masterUserRepository = masterUserRepository;
            _securityService = securityService;
            _masterUserInstitutionRepository = masterUserInstitutionRepository;
        }

        public bool Exist(string userName)
        {
            return _masterUserRepository.Query().FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower()) != null;
        }

        public void CreateUser(string userName, string password)
        {
            var salt = Guid.NewGuid().ToString();
            var user = new MasterUser {Salt = salt, Password = HashPassword(password, salt), UserName = userName, FullName = userName};
            _masterUserRepository.Save(user);
            _masterUserRepository.Commit();
        }

        private string HashPassword(string password, string salt)
        {
            var sha = SHA1.Create();
            var preHash = Encoding.UTF32.GetBytes(password + salt);
            var hash = sha.ComputeHash(preHash);
            return Convert.ToBase64String(hash);
        }

        public MasterUser Authenticate(string userName, string password)
        {
            var user = _masterUserRepository.Query().FirstOrDefault(u => u.UserName == userName);
            if (user == null) return null;
            var passwordHashed = HashPassword(password, user.Salt);
            return passwordHashed == user.Password ? user : null;
        }

        public void CreateNewUser(string userName, string fullName, string password)
        {
            var salt = Guid.NewGuid().ToString();
            var institutionId = _securityService.GetCurrentInstitutionId();
            var user = new MasterUser
            {
                Salt = salt,
                Password = HashPassword(password, salt),
                UserName = userName,
                FullName = fullName,
                DefaultInstitutionId = institutionId
            };
            
            _masterUserRepository.Save(user);

            var userInstitution = new MasterUserInstitution()
            {
                User = user,
                InstitutionId = institutionId
            };
            _masterUserInstitutionRepository.Save(userInstitution);

            _masterUserRepository.Commit();
        }

        public void UpdateUserData(long userId, string fullName, long defaultInstitutionId, List<MasterUserInstitution> institutions)
        {
            var user = _masterUserRepository.Query().Include(u => u.Institutions).FirstOrDefault(u => u.UserId == userId);
            if (user == null) throw new Exception("User not exists");

            user.FullName = fullName;
            institutions.ToList().ForEach(d =>
            {
                var dataInstitution = user.Institutions.FirstOrDefault(i => i.InstitutionId == d.InstitutionId);
                if (dataInstitution != null)
                {
                    dataInstitution.IsActive = d.IsActive;
                }
                else
                {
                    var newDataUserInstitution = new MasterUserInstitution
                    {
                        UserId = user.UserId,
                        InstitutionId = d.InstitutionId,
                        IsActive = true
                    };
                    user.Institutions.Add(newDataUserInstitution);
                }
            });

            _masterUserRepository.Save(user);
            _masterUserRepository.Commit();
        }


        public void Delete(MasterUser data)
        {
            _masterUserRepository.Delete(data);
            _masterUserRepository.Commit();
        }

        public MasterUserView FetchOne(long userId)
        {
            var user = _masterUserRepository.Query().FirstOrDefault(u => u.UserId == userId);
            if (user == null) return null;
            var data = new MasterUserView()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName
            };
            return data;
        }

        public List<MasterUserView> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result =
                _masterUserRepository.Query()
                    .Include(u => u.Institutions)
                    .Include(u => u.Institutions.Select(i => i.Institution))
                    .Where(u => u.IsActive && u.Institutions.Any(i => i.IsActive && i.InstitutionId == institutionId));
            searchQuery.TotalData = BaseSearchQueryExpression<MasterUser>.DefaultQueryExpression(ref result, searchQuery);
            return
                result.ToList()
                    .Select(
                        r =>
                            new MasterUserView()
                            {
                                UserId = r.UserId,
                                UserName = r.UserName,
                                FullName = r.FullName,
                                DefaultInstitutionId = r.DefaultInstitutionId,
                                Institutions = r.Institutions
                            })
                    .ToList();
        }

        public void ResetPassword(string userName, string newPassword)
        {
            var user = _masterUserRepository.Query().FirstOrDefault(u => u.UserName == userName);
            if (user == null) throw new Exception("User not exists");

            user.Password = HashPassword(newPassword, user.Salt);
            _masterUserRepository.Save(user);
            _masterUserRepository.Commit();
        }

        public void ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = _masterUserRepository.Query().FirstOrDefault(u => u.UserName == userName);
            if (user == null) throw new Exception("User not exists");

            var hashedOldPassword = HashPassword(oldPassword, user.Salt);
            if (user.Password != hashedOldPassword) throw new Exception("Wrong Old Password");

            user.Password = HashPassword(newPassword, user.Salt);

            _masterUserRepository.Save(user);
            _masterUserRepository.Commit();
        }
    }
}
