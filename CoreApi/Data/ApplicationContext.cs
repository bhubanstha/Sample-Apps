using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser,AppRole,int>
    {

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {

        }
    }

    public class AppUser : IdentityUser<int>
    {

    }

    public class AppRole: IdentityRole<int>
    {

    }
}
