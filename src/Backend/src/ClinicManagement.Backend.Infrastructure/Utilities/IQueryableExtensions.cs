namespace ClinicManagement.Backend.Infrastructure.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable,int pagina,int recordsPorPagina)
        {
            return queryable
                .Skip((pagina - 1) * recordsPorPagina)
                .Take(recordsPorPagina);
                
        }
    }
}
