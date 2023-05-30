using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestWithAspNet.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext resultExecutingContext);
        Task Enrich(ResultExecutingContext resultExecutingContext);
    }
}
