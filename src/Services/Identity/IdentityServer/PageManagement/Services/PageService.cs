using Fix;
using Fix.Data;
using IdentityServer.Models.PageManagement;
using IdentityServer.Security.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.PageManagement.Services
{
    public class PageService : IPageService
    {
        private readonly IRepository<Page> repository;
        private readonly IAccountService accountService;

        public PageService(IRepository<Page> repository,
            IAccountService accountService)
        {
            this.repository = repository;
            this.accountService = accountService;
        }
        public IEnumerable<Page> Search(long? id, string url, string name, string componentName, int? order, string icon, bool? showOnMenu, int? parentId, List<int> pageRoles, bool? isActive)
        {
            var result = GetWithRelation()
                .Where(en => (id == null || en.Id == id)
                             && (url.IsNullOrEmpty() || en.Url.ToUpper().Contains(url.ToUpper()))
                             && (name.IsNullOrEmpty() || en.Name.ToUpper().Contains(name.ToUpper()))
                             && (componentName.IsNullOrEmpty() || en.ComponentName.ToUpper().Contains(componentName.ToUpper()))
                             && (order == null || en.Order == order)
                             && (icon.IsNullOrEmpty() || en.Icon == icon)
                             && (showOnMenu == null || en.ShowOnMenu == showOnMenu)
                             && (parentId == null || en.ParentId == parentId)
                             && ((pageRoles == null || pageRoles.Count == 0) || en.PageRoles.Any(eno => pageRoles.Any(enx => enx == eno.RoleId)))
                             && (isActive == null || en.IsActive == isActive)
                );
            return result;
        }
        public Page GetById(int id)
        {
            var entity = GetWithRelation().FirstOrDefault(en => en.Id == id);
            return entity;
        }

        public IQueryable<Page> GetWithRelation()
        {
            var entities = repository.GetAllWithoutRestriction()
                .Include(en => en.Parent)
                .Include(en => en.Children).OrderBy(en => en.Order)
                .Include(en => en.PageRoles)
                .ThenInclude(en => en.Role);
            return entities;
        }

        public IQueryable<Page> Get()
        {
            var entities = GetWithRelation().Where(en => en.IsActive);
            return entities;
        }


        public IQueryable<Page> GetParents()
        {
            var entities = GetWithRelation()
                .Where(en => en.IsActive && en.ParentId == null && en.ComponentName.IsNullOrEmpty());
            return entities;
        }

        /// <summary>
        /// Öğrenci oluşturma
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parentId"></param>
        /// <param name="roleIdList"></param>
        public void Create(Page entity, int? parentId, List<int> roleIdList)
        {
            entity.IsActive = true;
            entity.Parent = repository.Table.FirstOrDefault(en => en.Id == parentId);
            roleIdList?.ForEach(en =>
            {
                var role = accountService.GetRole(en);

                entity.PageRoles.Add(new PageRole() { Role = role });
            });
            repository.Add(entity);
        }

        /// <summary>
        /// Öğrenci Güncelleme
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parentId"></param>
        /// <param name="roleIdList"></param>
        public void Update(Page entity, int? parentId, List<int> roleIdList)
        {

            entity.Parent = repository.Table.FirstOrDefault(en => en.Id == parentId);
            if (entity.PageRoles.Any())
            {
                
                roleIdList ??= new List<int>();
                var list = entity.PageRoles.Where(en => !(roleIdList.Contains(en.Role.Id)));
                var length = list.Count();
                for (int i = 0; i < length; i++)
                {
                    var item = list.First();
                    entity.PageRoles.Remove(item);
                    roleIdList?.Remove(item.Role.Id);
                }
            }
            roleIdList?.ForEach(en =>
            {
                if (!entity.PageRoles.Any(eno => eno.Role.Id == en))
                {
                    var role = accountService.GetRole(en);
                    entity.PageRoles.Add(new PageRole() { Role = role });
                }
            });

            ///bölge değişikliği yapıldıysa taslaklar siliniyor.
            repository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            repository.Delete(entity);
        }

        public IEnumerable<Page> GetUserPages(List<int> userRoleIds)
        {
            var pages = GetWithRelation();
            var rolePages = pages
                .Where(c =>
                            c.ShowOnMenu
                            && c.IsActive
                            && c.ParentId == null
                            && c.PageRoles != null
                            && c.PageRoles.Any()
                            && c.PageRoles.Any(pp => userRoleIds.Any(en => en == pp.RoleId.ToInteger())))
                .OrderBy(en => en.Order).ThenBy(en => en.Name).ThenBy(en => en.Name)
                .ToList();
            rolePages.ForEach(en =>
            {
                en.Children = en.Children.OrderBy(enx => enx.Order).ThenBy(enx => enx.Name).ToList();
            });
            return rolePages;
        }

        /// <inheritdoc />
        public Page CheckRoleComponent(int roleId, string path)
        {
            var pages = GetWithRelation();
            var list = pages.Where(en => en.Url == "/" + path && en.PageRoles.Any(enx => enx.Role.Id == roleId));
            return list.FirstOrDefault();
        }
    }
}
