using RestWithAspNet.Hypermedia;
using RestWithAspNet.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithAspNet.Data.VO
{
    public class BookVO : ISupportsHypermedia
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();
    }
}
