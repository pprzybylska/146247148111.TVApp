using _146247.TVApp.DAOMock;
using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;

namespace _146247148111.TVApp.DAOMock
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers { get; set; }
        private List<ITV> TVs { get; set; }

        public DAOMock()
        {
            producers = new List<IProducer>();
            TVs = new List<ITV>();

            producers.Add(new Producer() { ID = 1, Name = "Samsung" });
            producers.Add(new Producer() { ID = 2, Name = "Sony" });
            producers.Add(new Producer() { ID = 3, Name = "Panasonic" });

            TVs.Add(new TV() { ID = 1, Name = "QE65Q77C", ProducerId = 1, Producer = producers[0], Screen = Core.ScreenType.QLED, ScreenSize = 65 });
            TVs.Add(new TV() { ID = 2, Name = "QE65S90C", ProducerId = 1, Producer = producers[0], Screen = Core.ScreenType.OLED, ScreenSize = 65 });
            TVs.Add(new TV() { ID = 3, Name = "KD-65X75WL", ProducerId = 2, Producer = producers[1], Screen = Core.ScreenType.LED, ScreenSize = 65 });
            TVs.Add(new TV() { ID = 4, Name = "KD-55X85LAEP", ProducerId = 2, Producer = producers[1], Screen = Core.ScreenType.LED, ScreenSize = 55 });
            TVs.Add(new TV() { ID = 5, Name = "TX-55LX650E", ProducerId = 3, Producer = producers[2], Screen = Core.ScreenType.LED, ScreenSize = 55 });
            TVs.Add(new TV() { ID = 6, Name = "TX-50LX650E", ProducerId = 3, Producer = producers[2], Screen = Core.ScreenType.LED, ScreenSize = 50 });
        }

        public IProducer CreateNewProducer(string Name, string Country)
        {
            var lowestAvailableProducerId = 1;
            while (producers.Any(tv => tv.ID == lowestAvailableProducerId))
            {
                lowestAvailableProducerId++;
            }

            var producer = new Producer { ID=lowestAvailableProducerId, Name=Name, Country=Country };
            producers.Add(producer);
            return producer;
        }

        public ITV CreateNewTV(string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            var lowestAvailableTvId = 1;
            while (TVs.Any(tv => tv.ID == lowestAvailableTvId))
            {
                lowestAvailableTvId++;
            }

            var producerIndex = producers.FindIndex(producers => producers.Name.Equals(ProducerName));
            var tv = new TV { ID=lowestAvailableTvId, Name=Name, ProducerId=producers[producerIndex].ID, Producer = producers[producerIndex], Screen=Screen, ScreenSize=ScreenSize };
            TVs.Add(tv);
            Console.WriteLine(tv.ID + " " + tv.ScreenSize + " " + tv.Screen + " " + tv.ProducerId);

            Console.WriteLine(TVs.Count);
            return tv;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public IEnumerable<ITV> GetAllTV()
        {
            return TVs;
        }

        public bool DeleteProducerById(int producerId)
        {
            int indexToRemove = producers.FindIndex(producers => producers.ID == producerId);

            TVs.RemoveAll(tv => tv.ProducerId == producerId);
            if (indexToRemove != -1)
            {
                producers.RemoveAt(indexToRemove);
                return true;
            }
            return false;
        }

        public bool DeleteTVById(int TVId)
        {
            int indexToRemove = TVs.FindIndex(tv => tv.ID == TVId);

            if (indexToRemove != -1)
            {
                TVs.RemoveAt(indexToRemove);
                return true;
            }
            return false;
        }

        public IProducer UpdateProducer(int ID, string Name, string Country)
        {
            int indexToUpdate = producers.FindIndex(producers => producers.ID == ID);
            if (indexToUpdate != -1)
            {
                var producer = producers[indexToUpdate];
                producer.Name = Name;
                producer.Country = Country;
                return producer;
            }

            return null;
        }

        public ITV UpdateTV(int ID, string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            int indexToUpdate = TVs.FindIndex(TVs => TVs.ID == ID);
            if (indexToUpdate != -1)
            {
                var producerIndex = producers.FindIndex(producers => producers.Name.Equals(ProducerName));
                var tv = TVs[indexToUpdate];
                tv.Name = Name;
                tv.ProducerId = producers[producerIndex].ID;
                tv.Producer = producers[producerIndex];
                tv.Screen = Screen;
                tv.ScreenSize = ScreenSize;
                return tv;
            }
            return null;
        }

        public IEnumerable<ITV> SearchTVsByKeyword(string keyword)
        {

            if (keyword != null)
            {
                IEnumerable<ITV> matchingTVs = TVs.Where(tv =>
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
            List<ITV> filteredTVs = TVs
                .Where(tv => tv.Producer.Name.Equals(producer, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return filteredTVs;
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

            List<ITV> filteredTVs = TVs
                .Where(tv => tv.ScreenSize >= minSize && ((TV)tv).ScreenSize <= maxSize)
                .ToList();

            return filteredTVs;
        }

        public IEnumerable<ITV> FilterByScreenType(ScreenType screenType)
        {
            List<ITV> filteredTVs = TVs
                .Where(tv => tv.Screen == screenType)
                .ToList();

            return filteredTVs;
        }

    }
}
