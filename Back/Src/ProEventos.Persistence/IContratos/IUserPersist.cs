using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    // Interface implementa a outra interface semelhante nesse caso,  IGeralPersistence que é GENERICA

    public interface IUserPersist : IGeralPersistence
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserName(string userName);

    }
}
