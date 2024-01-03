using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using System.Reflection;

namespace _146247148111.TVApp.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(string libraryName)
        {
            Type? typeToCreate = null;

            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);

        }

        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }

        public IEnumerable<ITV> GetTVs()
        {
            return dao.GetAllTV();
        }

        public IProducer CreateNewProducer(int ID, string Name, string Country)
        {
            return dao.CreateNewProducer(ID, Name, Country);   
        }

        public ITV CreateNewTV(int ID, string Name, int ProducerId, ScreenType Screen, int ScreenSize)
        {
            return dao.CreateNewTV(ID, Name, ProducerId, Screen, ScreenSize);
        }

        public bool DeleteProducerById(int producerId)
        {
            return dao.DeleteProducerById(producerId);
        }

        public bool DeleteTVById(int TVId)
        {
            return dao.DeleteTVById(TVId);
        }

        public IProducer UpdateProducer(int ID, string Name, string Country)
        {
            return dao.UpdateProducer(ID, Name, Country);
        }

        public ITV UpdateTV(int ID, string Name, int ProducerId, ScreenType Screen, int ScreenSize)
        {
            return dao.UpdateTV(ID, Name, ProducerId, Screen, ScreenSize);
        }

        public IEnumerable<ITV> SearchTVsByKeyword(string keyword)
        {
            return dao.SearchTVsByKeyword(keyword);
        }

        public IEnumerable<ITV> FilterByProducer(string producer)
        {
            return dao.FilterByProducer(producer);
        }
        public IEnumerable<ITV> FilterByScreenSize(double minSize = 0, double maxSize = 100)
        {
            return dao.FilterByScreenSize(minSize, maxSize);
        }

        public IEnumerable<ITV> FilterByScreenType(ScreenType screenType)
        {
            return dao.FilterByScreenType(screenType);
        }
    }
}