using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using _146247.TVApp.DAOSQL;


namespace _146247148111.TVApp.DAOSQL
{
    public class DAOSQL: IDAO
    {
        public IProducer CreateNewProducer(int ID, string Name, string Country)
        {
            var context = new Context();
            var producer = new Producer { ID=ID, Name=Name, Country=Country };
            context.producers.Add(producer);
            context.SaveChanges();
            return producer;
        }

        public ITV CreateNewTV(int ID, string Name, IProducer TV_Producer, ScreenType Screen, int ScreenSize)
        {
            var context = new Context();
            var tv = new TV { ID=ID, Name=Name, Producer=TV_Producer, Screen=Screen, ScreenSize=ScreenSize };
            context.TVs.Add(tv);
            context.SaveChanges();
            return tv;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            var context = new Context();
            return context.producers;
        }

        public IEnumerable<ITV> GetAllTV()
        {
            var context = new Context();
            return context.TVs;
        }
    }
}
