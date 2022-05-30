using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    public interface IGeralPersistence
    {
        //GERAL Metodos genericos que são executados de acordo com o dominio

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entityArray) where T : class;
        Task<bool> SaveChangesAsync() ;

    }
}
