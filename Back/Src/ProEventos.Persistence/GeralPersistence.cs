using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{


    // Classe que implenta a interface IProEventosPersistence que tem os metodos Genericospara EVENTOS


    public class GeralPersistence : IGeralPersistence
    {
        private readonly ProEventosContext _context;

        // faz a injeção de Dependencia do contexto.


        public GeralPersistence(ProEventosContext context)
        {
            _context = context;
        }
        public void Add<T> (T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T> (T entity) where T : class
        {
            _context.Update (entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        // Remove varios elementos
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        // Retorna true se ocorrer alguma alteração executada pelos metodos.

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }



    }
}
