using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace _146247148111.TVApp.BLC
{
    public class BLC
    {
        private static BLC instance;
        private static readonly object lockObject = new object();
        private IDAO dao;

        private BLC(IDAO daoInstance)
        {
            dao = daoInstance;
        }

        public static BLC GetInstance(string libraryName)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        Type typeToCreate = null;

                        Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.IsAssignableTo(typeof(IDAO)))
                            {
                                typeToCreate = type;
                                break;
                            }
                        }

                        IDAO daoInstance = (IDAO)Activator.CreateInstance(typeToCreate, null);
                        instance = new BLC(daoInstance);
                    }
                }
            }

            return instance;
        }

        public static BLC GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        Type typeToCreate = null;

                        string dirName = AppDomain.CurrentDomain.BaseDirectory;
                        dirName = Path.GetFullPath(dirName).Remove(dirName.IndexOf("\\bin"));

                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(dirName)
                            .AddJsonFile("appsettings.json")
                            .Build();

                        Assembly assembly = Assembly.UnsafeLoadFrom(configuration["DAOLibraryName"]);

                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.IsAssignableTo(typeof(IDAO)))
                            {
                                typeToCreate = type;
                                break;
                            }
                        }

                        IDAO daoInstance = (IDAO)Activator.CreateInstance(typeToCreate, null);
                        instance = new BLC(daoInstance);
                    }
                }
            }

            return instance;
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }

        public IEnumerable<ITV> GetTVs()
        {
            return dao.GetAllTV();
        }

        public IProducer CreateNewProducer(string Name, string Country)
        {
            return dao.CreateNewProducer(Name, Country);   
        }

        public ITV CreateNewTV(string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            return dao.CreateNewTV(Name, ProducerName, Screen, ScreenSize);
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

        public ITV UpdateTV(int ID, string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            return dao.UpdateTV(ID, Name, ProducerName, Screen, ScreenSize);
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