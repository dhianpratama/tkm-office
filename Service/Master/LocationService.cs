using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;
using EF.SP;

namespace Service.Master
{
    public class LocationService : ILocationService
    {
        private readonly ISpWrapper _spWrapper;
        private readonly ISecurityService _securityService;

        public LocationService(ISpWrapper spWrapper, ISecurityService securityService)
        {
            _spWrapper = spWrapper;
            _securityService = securityService;
        }

        public void Save(MasterLocationWithParentLocationId data)
        {
            var currentUser = _securityService.GetCurrentUser();
            var institutionId = _securityService.GetCurrentInstitutionId();
            
            if (data.LocationId == 0)
            {
                if (data.ParentLocationId == null)
                {
                    var sp = new MasterLocationInsertRootNode()
                    {
                        ParamInstitutionId = institutionId,
                        ParamLocationCode = data.LocationCode,
                        ParamLocationName = data.LocationName,
                        ParamLocationTypeId = data.LocationTypeId,
                        ParamCreatedBy = currentUser.UserName,
                        ParamCreatedDate = DateTime.Now
                    };
                    _spWrapper.ExecuteNonQueryStoredProcedure(sp);
                }
                else
                {
                    var sp = new MasterLocationInsert()
                    {
                        ParamInstitutionId = institutionId,
                        ParamLocationCode = data.LocationCode,
                        ParamLocationName = data.LocationName,
                        ParamLocationTypeId = data.LocationTypeId,
                        ParamParentLocationId = data.ParentLocationId ?? 0,
                        ParamCreatedBy = currentUser.UserName,
                        ParamCreatedDate = DateTime.Now
                    };
                    _spWrapper.ExecuteNonQueryStoredProcedure(sp);

                }
            }
            else
            {
                var sp = new MasterLocationUpdate()
                {
                    ParamLocationId = data.LocationId,
                    ParamLocationCode = data.LocationCode,
                    ParamLocationName = data.LocationName,
                    ParamLocationTypeId = data.LocationTypeId,
                    ParamCreatedBy = currentUser.UserName,
                    ParamCreatedDate = DateTime.Now
                };
                _spWrapper.ExecuteNonQueryStoredProcedure(sp);
            }
        }

        public void Delete(MasterLocation data)
        {
            var sp = new MasterLocationDelete()
            {
                ParamLocationId = data.LocationId
            };
            _spWrapper.ExecuteNonQueryStoredProcedure(sp);
        }

        public List<MasterLocationQuery> FetchAll()
        {
            var sp = new MasterLocationFetchAll();
            var listLocation = _spWrapper.ExecuteQueryStoredProcedure(sp);
            return listLocation.ToList();
        }

        public List<MasterLocationQuery> FetchAllByInstitution()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var sp = new MasterLocationFetchAllByInstitutionId()
            {
                ParamInstitutionId = institutionId
            };
            var listLocation = _spWrapper.ExecuteQueryStoredProcedure(sp);
            return listLocation.ToList();
        }

        public MasterLocationQuery FetchOne(long locationId)
        {
            var sp = new MasterLocationFetchOne()
            {
                ParamLocationId = locationId
            };
            var location = _spWrapper.ExecuteQueryStoredProcedure(sp);
            return location.FirstOrDefault();
        }

        public List<MasterLocationDropdownList> FetchAllForDropdownList()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var sp = new MasterLocationFetchAllForDropdownList()
            {
                ParamInstitutionId = institutionId
            };
            var listLocation = _spWrapper.ExecuteQueryStoredProcedure(sp);
            return listLocation.ToList();
        }
    }
}
