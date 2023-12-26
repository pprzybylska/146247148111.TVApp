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
    }
}