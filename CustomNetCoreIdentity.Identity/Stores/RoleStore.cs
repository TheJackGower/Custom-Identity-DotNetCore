using System;
using System.Threading;
using System.Threading.Tasks;
using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CustomNetCoreIdentity.Stores
{
    public class RoleStore : IRoleStore<SiteRole>
    {
        private ISiteRoleRepository _siteRoleRepository { get; set; }

        public RoleStore(ISiteRoleRepository siteRoleRepository)
        {
            _siteRoleRepository = siteRoleRepository;
        }

        public async Task<IdentityResult> CreateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.CreateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.UpdateAsync(role, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(SiteRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.DeleteAsync(role, cancellationToken);
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

            return await _siteRoleRepository.FindByIdAsync(roleId, cancellationToken);
        }

        public async Task<SiteRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _siteRoleRepository.FindByNameAsync(normalizedRoleName, cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this != null)
                {
                    //this.Dispose();
                }
            }
        }
    }
}
