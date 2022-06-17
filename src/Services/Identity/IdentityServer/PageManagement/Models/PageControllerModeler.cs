using Fix;
using Fix.Mvc;
using IdentityServer.Models;
using IdentityServer.Models.PageManagement;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.PageManagement.Models
{

    /// <summary>
    /// 
    /// </summary>
    public static class PageControllerModeler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Page CreateRequestToModel(this CreatePageRequest request)
        {
            var entity = new Page
            {
                Url = request.Url,
                Name = request.Name,
                ComponentName = request.ComponentName,
                Icon = request.Icon,
                IsActive = request.IsActive,
                ShowOnMenu = request.ShowOnMenu,
                Order = request.Order,
            };
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entity"></param>
        public static void UpdateRequestToModel(this UpdatePageRequest request, ref Page entity)
        {
            entity.Url = request.Url;
            entity.Name = request.Name;
            entity.ComponentName = request.ComponentName;
            entity.Icon = request.Icon;
            entity.IsActive = request.IsActive;
            entity.ShowOnMenu = request.ShowOnMenu;
            entity.Order = request.Order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static ServiceResult<List<GetPageResponse>> ToResponse(this IEnumerable<Page> entities)
        {
            var values = from entity in entities
                         select new GetPageResponse
                         {
                             Id = entity.Id,
                             Url = entity.Url,
                             Name = entity.Name,
                             ComponentName = entity.ComponentName,
                             Icon = entity.Icon,
                             IsActive = entity.IsActive,
                             ShowOnMenu = entity.ShowOnMenu,
                             Order = entity.Order,
                             ParentId = entity.ParentId,
                             Parent = entity.Parent != null ? new Page()
                             {
                                 Id = entity.Parent.Id,
                                 Url = entity.Parent.Url,
                                 Name = entity.Parent.Name,
                                 ComponentName = entity.Parent.ComponentName,
                                 Icon = entity.Parent.Icon,
                                 IsActive = entity.Parent.IsActive,
                                 ShowOnMenu = entity.Parent.ShowOnMenu,
                                 Order = entity.Parent.Order,
                                 IsParent = entity.Parent.ComponentName.IsNullOrEmpty(),
                                 PageRoles = entity.Parent.PageRoles.Select(eny => new PageRole()
                                 {
                                     Id = eny.Id,
                                     RoleId = eny.RoleId,
                                     PageId = eny.PageId,
                                     Role = new Role()
                                     {
                                         Id = eny.Role.Id,
                                         Name = eny.Role.Name,
                                         Description = eny.Role.Description,
                                     }
                                 }).ToList()

                             } : null,
                             Children = entity.Children.Where(en => en.IsActive && en.ShowOnMenu).Select(en => new Page()
                             {

                                 Id = en.Id,
                                 Url = en.Url,
                                 Name = en.Name,
                                 ComponentName = en.ComponentName,
                                 Icon = en.Icon,
                                 IsActive = en.IsActive,
                                 ShowOnMenu = en.ShowOnMenu,
                                 Order = en.Order,
                                 Children = en.Children.Where(en=> en.IsActive && en.ShowOnMenu).Select(enx => new Page()
                                 {

                                     Id = enx.Id,
                                     Url = enx.Url,
                                     Name = enx.Name,
                                     ComponentName = enx.ComponentName,
                                     Icon = enx.Icon,
                                     IsActive = enx.IsActive,
                                     ShowOnMenu = enx.ShowOnMenu,
                                     Order = enx.Order,
                                     IsParent = enx.ComponentName.IsNullOrEmpty(),
                                     PageRoles = enx.PageRoles.Select(eny => new PageRole()
                                     {
                                         Id = eny.Id,
                                         RoleId = eny.RoleId,
                                         PageId = eny.PageId,
                                         Role = new Role()
                                         {
                                             Id = eny.Role.Id,
                                             Name = eny.Role.Name,
                                             Description = eny.Role.Description,
                                         }
                                     }).ToList()
                                 }).ToList(),
                                 IsParent = en.ComponentName.IsNullOrEmpty(),
                                 PageRoles = en.PageRoles.Select(enz => new PageRole()
                                 {
                                     Id = enz.Id,
                                     RoleId = enz.RoleId,
                                     PageId = enz.PageId,
                                     Role = new Role()
                                     {
                                         Id = enz.Role.Id,
                                         Name = enz.Role.Name,
                                         Description = enz.Role.Description,
                                     },
                                 }).ToList()
                             }).ToList(),
                             IsParent = entity.ComponentName.IsNullOrEmpty(),
                             PageRoles = entity.PageRoles.Select(en => new PageRole()
                             {
                                 Id = en.Id,
                                 RoleId = en.RoleId,
                                 PageId = en.PageId,
                                 Role = new Role()
                                 {
                                     Id = en.Role.Id,
                                     Name = en.Role.Name,
                                     Description = en.Role.Description,
                                 },
                             }
                                 ).ToList()

                         };
            return new ServiceResult<List<GetPageResponse>>(values.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ServiceResult<GetPageResponse> ToResponse(this Page entity)
        {
            var value = new GetPageResponse
            {
                Id = entity.Id,
                Url = entity.Url,
                Name = entity.Name,
                ComponentName = entity.ComponentName,
                Icon = entity.Icon,
                IsActive = entity.IsActive,
                ShowOnMenu = entity.ShowOnMenu,
                Order = entity.Order,
                Parent = entity.Parent,
                Children = entity.Children,
                PageRoles = entity.PageRoles
            };
            return new ServiceResult<GetPageResponse>(value);
        }
    }
}
