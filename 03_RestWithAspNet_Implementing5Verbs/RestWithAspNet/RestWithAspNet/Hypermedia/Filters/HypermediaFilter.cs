using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Hypermedia.Filters
{
    public class HypermediaFilter : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _hypermidiaFilterOptions;

        public HypermediaFilter(HypermediaFilterOptions hypermidiaFilterOptions)
        {
            _hypermidiaFilterOptions = hypermidiaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryErichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryErichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult objectResult)
            {
                var enricher = _hypermidiaFilterOptions.ContentResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));

                if(enricher != null)
                {
                    Task.FromResult(enricher.Enrich(context));
                }
            }
        }
    }
}
