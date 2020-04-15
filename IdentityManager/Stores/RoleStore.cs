using IdentityManager.DAL;
using IdentityManager.Entities.Custom;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManager.Stores
{
    public class RoleStore : IRoleStore<SiteRole>
    {
        private RoleService _roleService { get; set; }

        public RoleStore(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IdentityResult> CreateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _roleService.CreateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _roleService.UpdateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _roleService.DeleteAsync(role, cancellationToken);
        }

        public Task<string> GetRoleIdAsync(SiteRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(SiteRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(SiteRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(SiteRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(SiteRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<SiteRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _roleService.FindByIdAsync(roleId, cancellationToken);
        }

        public async Task<SiteRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _roleService.FindByNameAsync(normalizedRoleName, cancellationToken);
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
