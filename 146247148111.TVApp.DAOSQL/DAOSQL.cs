﻿using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using _146247.TVApp.DAOSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace _146247148111.TVApp.DAOSQL
{
    public class DAOSQL : IDAO
    {
        public IProducer CreateNewProducer(string Name, string Country)
        {
            var context = new Context();

            var lowestAvailableProducerId = 1;
            while (context.producers.Any(tv => tv.ID == lowestAvailableProducerId))
            {
                lowestAvailableProducerId++;
            }

            var producer = new Producer { ID=lowestAvailableProducerId, Name=Name, Country=Country };
            context.producers.Add(producer);
            context.SaveChanges();
            return producer;
        }

        public ITV CreateNewTV(string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            var context = new Context();

            var lowestAvailableTvId = 1;
            while (context.TVs.Any(tv => tv.ID == lowestAvailableTvId))
            {
                lowestAvailableTvId++;
            }

            Console.WriteLine(ProducerName);
            var existingProducer = context.producers.FirstOrDefault(p => p.Name.Equals(ProducerName));

            if (existingProducer == null)
            {
                Console.WriteLine("Producent o podanym imieniu nie istnieje.");
                return null;
            }

            var tv = new TV {ID=lowestAvailableTvId, Name=Name, ProducerId=existingProducer.ID, Producer=existingProducer, Screen=Screen, ScreenSize=ScreenSize };

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
            IEnumerable<TV> tvsToDelete = context.TVs.Where(tv => tv.ProducerId == producerId);

            if (tvsToDelete.Any())
            {
                context.TVs.RemoveRange(tvsToDelete);
                context.SaveChanges();
            }

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

        public IProducer UpdateProducer(int ID, string Name, string Country)
        {
            var context = new Context();
            var producerToUpdate = context.producers.FirstOrDefault(p => p.ID == ID);
            if (producerToUpdate != null)
            {
                producerToUpdate.Name = Name;
                producerToUpdate.Country = Country;
                context.Update(producerToUpdate);
                context.SaveChanges();
                return producerToUpdate;
            }
            Console.WriteLine("TV o podanym ID nie istnieje.");
            return null;
        }

        public ITV UpdateTV(int ID, string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            var context = new Context();
            var TVToUpdate = context.TVs.FirstOrDefault(p => p.ID == ID);


            if (TVToUpdate != null)
            {
                var existingProducer = context.producers.FirstOrDefault(p => p.Name.Equals(ProducerName));
                TVToUpdate.Name = Name;
                TVToUpdate.ProducerId = existingProducer.ID;
                TVToUpdate.Producer = existingProducer;
                TVToUpdate.Screen = Screen;
                TVToUpdate.ScreenSize = ScreenSize;
                context.SaveChanges();
                return TVToUpdate;
            }
            Console.WriteLine("TV o podanym ID nie istnieje.");
            return null;
        }
        public IEnumerable<ITV> SearchTVsByKeyword(string keyword)
    {
            var context = new Context();

            if (keyword != null)
            {
                IEnumerable<ITV> matchingTVs = context.TVs.Include(t => t.Producer).ToList()
                    .Where(tv =>
                        tv.Name.ToLower().Contains(keyword.ToLower()) ||
                        tv.Producer.Name.ToLower().Contains(keyword.ToLower()) ||
                        tv.Screen.ToString().ToLower().Contains(keyword.ToLower()) ||
                        tv.ScreenSize.ToString().ToLower().Contains(keyword.ToLower()))
                    .ToList();

                return matchingTVs;
            }

            return GetAllTV();
        }

        public IEnumerable<ITV> FilterByProducer(string producer)
        {
            var context = new Context();
            if (producer  != null)
            {
                var filteredTVs = context.TVs.Include(t => t.Producer).ToList()
                    .Where(tv => tv.Producer.Name.Equals(producer, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return filteredTVs;
            }

            return GetAllTV();
        }

        public IEnumerable<ITV> FilterByScreenSize(double? minSize, double? maxSize)
        {
            if (minSize == null)
            {
                minSize = 0;
            }

            if (maxSize == null || maxSize == 0)
            {
                maxSize = 200;
            }

            var context = new Context();

            var filteredTVs = context.TVs.Include(t => t.Producer).ToList()
                .Where(tv => tv.ScreenSize >= minSize && tv.ScreenSize <= maxSize)
                .ToList();

            return filteredTVs;

        }

        public IEnumerable<ITV> FilterByScreenType(ScreenType screenType)
        {
            var context = new Context();

            if (screenType != null)
            {
                var filteredTVs = context.TVs.Include(t => t.Producer).ToList()
                    .Where(tv => tv.Screen == screenType)
                    .ToList();
                return filteredTVs;
            }
            return GetAllTV();

        }

    }
}
