using Fix.Utility;
using Fix.Mvc;
using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Security.Models
{
    public static class AccountControllerModeler
    {
        public static ServiceResult<IEnumerable<GetUserResponse>> ToAccountIndexResponse(this IPagedList<User> users)
        {
            var items = users.Select(en => new GetUserResponse
            {
                Id = en.Id,
                Name = en.Name,
                Surname = en.Surname,
                UserName = en.UserName,
                Email = en.Email,
                MobilePhone = en.MobilePhone,
                IsActive = en.IsActive,
                Company = en.Company != null ? new UserCompanyResponse
                {
                    Id = en.Company.Id,
                    Name = en.Company.Name,
                    SystemName = en.Company.SystemName
                } : null
            });
            return
                //new ActionResult<ServiceResult<IEnumerable<GetUserResponse>>>(
                new ServiceResult<IEnumerable<GetUserResponse>>(items);
            //);
        }

        public static ServiceResult<IEnumerable<GetAccountResponse>> ToAccountResponse(this IQueryable<User> users)
        {
            var items = users.Select(en => new GetAccountResponse
            {
                Id = en.Id,
                Name = en.Name,
                Surname = en.Surname,
                UserName = en.UserName,
                Company = new UserCompanyResponse
                {
                    Id = en.Company.Id,
                    Name = en.Company.Name,
                    SystemName = en.Company.SystemName
                }
            });

            return new ServiceResult<IEnumerable<GetAccountResponse>>(items);
        }
        public static User ToModel(this CreateAccountRequest request)
        {
            var model = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                Password = request.Password
            };
            return model;
        }
        public static User ToModel(this AccountUpdateRequest request, User instance)
        {
            instance.Name = request.Name;
            instance.Surname = request.Surname;
            instance.ModifiedOn = request.ModifiedOn;
            var toRemove = instance.UserInRoles.Select(x => x.Id).Except(request.RoleIds).ToList();
            var toAdd = request.RoleIds.Except(instance.UserInRoles.Select(x => x.Id)).ToList();

            foreach (var id in toRemove)
            {
                var item = instance.UserInRoles.FirstOrDefault(x => x.Id == id);
                instance.UserInRoles.Remove(item);
            }

            foreach (var id in toAdd)
            {
                instance.UserInRoles.Add(new UserInRole() { Id = id });
            }
            return instance;
        }
        public static ServiceResult<GetUserResponse> ToResponse(this User user)
        {
            return new ServiceResult<GetUserResponse>(
              new GetUserResponse
              {
                  Id = user.Id,
                  UserName = user.UserName,
                  Name = user.Name,
                  Surname = user.Surname,
                  Email = user.Email,
                  MobilePhone = user.MobilePhone,
                  Company = new UserCompanyResponse
                  {
                      Id = user.Company.Id,
                      Name = user.Company.Name,
                      SystemName = user.Company.SystemName
                  },

                  Roles = user.UserInRoles.Select(x => new UserRoleResponse
                  {
                      Id = x.Role.Id,
                      Name = x.Role.Name,
                      Description = x.Role.Description,
                      IsActive = x.Role.IsActive
                  })
              }

            );
        }

    }
}
