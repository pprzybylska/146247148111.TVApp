using _146247148111.TVApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ITV> GetAllTV();
        IProducer CreateNewProducer(int ID, string Name, string Country);
        ITV CreateNewTV(int ID, string Name, int ProducerId, ScreenType Screen, int ScreenSize);
        bool DeleteProducerById(int producerId);
        bool DeleteTVById(int TVId);
    }
}
