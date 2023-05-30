using RestWithAspNet.Hypermedia;
using RestWithAspNet.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithAspNet.Data.VO
{
    public class PersonVO : ISupportsHypermedia
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();
    }

}


