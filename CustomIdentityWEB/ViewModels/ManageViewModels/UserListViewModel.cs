using IdentityManager.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWEB.ViewModels.ManageViewModels
{
    public class UserListViewModel
    {
        public int Count { get; set; }

        public List<SiteUser> List { get; set; }
    }
}
