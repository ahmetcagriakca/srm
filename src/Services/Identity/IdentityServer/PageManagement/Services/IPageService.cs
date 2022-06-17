using Fix;
using IdentityServer.Models.PageManagement;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.PageManagement.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageService : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<Page> Get();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<Page> GetParents();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="componentName"></param>
        /// <param name="order"></param>
        /// <param name="icon"></param>
        /// <param name="showOnMenu"></param>
        /// <param name="parentId"></param>
        /// <param name="pageRoles"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Page> Search(long? id, string url, string name, string componentName, int? order, string icon,
            bool? showOnMenu, int? parentId, List<int> pageRoles, bool? isActive);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Page GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parentId"></param>
        /// <param name="roleIdList"></param>
        void Create(Page entity, int? parentId, List<int> roleIdList);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parentId"></param>
        /// <param name="roleIdList"></param>
        void Update(Page entity, int? parentId, List<int> roleIdList);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <returns></returns>
        IEnumerable<Page> GetUserPages(List<int> userRoleIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Page CheckRoleComponent(int roleId, string path);
    }
}
