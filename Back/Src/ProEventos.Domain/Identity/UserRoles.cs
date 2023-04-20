using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProEventos.Domain.Identity
{
    public class UserRoles: IdentityUserRole<int>
    {
        // Um usuario tem diversas ROLES
        //as ROLES podem pertencer a varios usuarios
        // Associação MUITOS pra MUITOS
        public User User { get; set; }
        public Role Role { get; set; }
    }
}