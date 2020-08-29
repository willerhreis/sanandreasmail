using SanAndreasMail.Persistence.Contexts;

namespace SanAndreasMail.Persistence.Respositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
