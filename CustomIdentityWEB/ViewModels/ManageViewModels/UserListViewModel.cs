using IdentityManager.Entities.Custom;
using System.Collections.Generic;

namespace CustomIdentityWEB.ViewModels.ManageViewModels
{
    public class UserListViewModel
    {
        public int Count { get; set; }

        public List<SiteUser> List { get; set; }
    }
}
