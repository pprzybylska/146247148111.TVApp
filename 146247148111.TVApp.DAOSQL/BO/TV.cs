using _146247.TVApp.DAOSQL;
using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;

namespace _146247148111.TVApp.DAOSQL
{
    public class TV : ITV
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public IProducer Producer { get; set; }
        public int ScreenSize { get; set; }
        public ScreenType Screen { get; set; }
    }
}
