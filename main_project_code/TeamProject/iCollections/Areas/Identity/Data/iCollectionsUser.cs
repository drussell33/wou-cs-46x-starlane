using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace iCollections.Areas.Identity.Data
{
    public class iCollectionsUser : IdentityUser
    {
        [PersonalData]
        public string AppUserName { get; set; }

    }
}
