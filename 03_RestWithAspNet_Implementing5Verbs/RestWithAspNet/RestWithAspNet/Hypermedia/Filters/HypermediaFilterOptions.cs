using RestWithAspNet.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspNet.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
