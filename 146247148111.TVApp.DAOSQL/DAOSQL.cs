using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using _146247.TVApp.DAOSQL;
using Microsoft.EntityFrameworkCore;



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

        public ITV CreateNewTV(int ID, string Name, int ProducerId, ScreenType Screen, int ScreenSize)
        {
            var context = new Context();
            var existingProducer = context.producers.FirstOrDefault(p => p.ID == ProducerId);

            if (existingProducer == null)
            {
                Console.WriteLine("Producent o podanym ID nie istnieje.");
                return null;
            }

            var tv = new TV { ID=ID, Name=Name, ProducerId=ProducerId, Producer=existingProducer, Screen=Screen, ScreenSize=ScreenSize };

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
            return context.TVs.Include(t => t.Producer).ToList();
        }

        public bool DeleteProducerById(int producerId)
        {
            var context = new Context();
            var producerToDelete = context.producers.FirstOrDefault(p => p.ID == producerId);

            if (producerToDelete != null)
            {
                context.producers.Remove(producerToDelete);
                context.SaveChanges();
                return true; 
            }

            Console.WriteLine("Producent o podanym ID nie istnieje.");
            return false; 
        }

        public bool DeleteTVById(int TVId)
        {
            var context = new Context();
            var TVToDelete = context.TVs.FirstOrDefault(p => p.ID == TVId);

            if (TVToDelete != null)
            {
                context.TVs.Remove(TVToDelete);
                context.SaveChanges();
                return true;
            }

            Console.WriteLine("TV o podanym ID nie istnieje.");
            return false;
        }
    }
}
