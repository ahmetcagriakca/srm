using Fix.Mvc;
using IdentityServer.Models;
using System.Collections.Generic;
using System.Linq;
namespace IdentityServer.Security.Models
{

    public static class RoleModeler
    {
        public static Role CreateRequestToModel(this CreateRoleRequest request)
        {
            var entity = new Role
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static void UpdateRequestToModel(this UpdateRoleRequest request, ref Role entity)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;
        }

        public static object ToResponse(this IEnumerable<Role> entities)
        {
            var values = from entity in entities
                         select new GetRoleResponse
                         {
                             Id = entity.Id,
                             Name = entity.Name,
                             Description = entity.Description,
                             IsActive = entity.IsActive,
                         };
            return new ServiceResult<IEnumerable<GetRoleResponse>>(values);
        }

        public static object ToResponse(this Role entity)
        {
            var value = new GetRoleResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
            };
            return new ServiceResult<GetRoleResponse>(value);
        }
        public static Role ToModel(this RoleStoreRequest request)
        {
            var model = new Role()
            {
                Name = request.Name,
                IsActive = request.IsActive,
                Description = request.Description,
                Permissions = request.Permissions.Select(x => new RolePermission()
                {
                    Permission = x
                }).ToList()
            };
            return model;
        }
        public static void BindTo(this RoleUpdateRequest request, Role toBind)
        {
            toBind.Name = request.Name;
            toBind.IsActive = request.IsActive;
            toBind.Description = request.Description;

            toBind.Permissions = request.Permissions.Select(x => new RolePermission()
            {
                Permission = x
            }).ToList();
        }

    }
}
