using _146247.TVApp.DAOMock;
using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.DAOMock
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<ITV> TVs;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new Producer() { ID = 1, Name="Samsung"},
                new Producer() { ID = 2, Name="Sony"},
                new Producer() { ID = 3, Name="Panasonic"},
            };

            TVs = new List<ITV>()
            {
                new TV() { ID = 1, Name = "QE65Q77C", ProducerId = 1, Screen = Core.ScreenType.QLED, ScreenSize=65},
                new TV() { ID = 2, Name = "QE65S90C", ProducerId = 1, Screen = Core.ScreenType.OLED, ScreenSize=65},
                new TV() { ID = 3, Name = "KD-65X75WL", ProducerId = 2, Screen = Core.ScreenType.LED, ScreenSize=65},
                new TV() { ID = 4, Name = "KD-55X85LAEP", ProducerId = 2, Screen = Core.ScreenType.LED, ScreenSize=55},
                new TV() { ID = 5, Name = "TX-55LX650E", ProducerId = 3, Screen = Core.ScreenType.LED, ScreenSize=55},
                new TV() { ID = 6, Name = "TX-50LX650E", ProducerId = 3, Screen = Core.ScreenType.LED, ScreenSize=50},
            };
        }

        public IProducer CreateNewProducer(int ID, string Name, string Country)
        {
            return new Producer { ID=ID, Name=Name, Country=Country };  //TO DO
        }

        public ITV CreateNewTV(int ID, string Name, int ProducerId, ScreenType Screen, int ScreenSize)
        {
            return new TV { ID=ID, Name=Name, ProducerId=ProducerId, Screen=Screen, ScreenSize=ScreenSize };  //TO DO
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

            return false; //TO DO
        }

        public bool DeleteTVById(int TVId)
        {

            return false;  //TO DO
        }
    }
}
