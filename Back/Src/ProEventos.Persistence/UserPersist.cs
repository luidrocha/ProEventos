using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    // Herda de   GeralPersistence e implementa IUserPersist

    public class UserPersist : GeralPersistence, IUserPersist
    {
        private readonly ProEventosContext _context;

        // GeralPersistence também precisa receber o contexto
        // Como este classe herda de GeralPercistence, podemos usar qualquer metodo dela

        public UserPersist(ProEventosContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                                 .FindAsync(id);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var userRetorno = await _context.Users
                                  .SingleOrDefaultAsync(user => user.UserName.ToLower() == userName.ToLower());

            return userRetorno;

        }
    }
}
