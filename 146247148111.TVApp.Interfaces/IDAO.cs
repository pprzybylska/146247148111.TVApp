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
        ITV CreateNewTV(int ID, string Name, IProducer TV_Producer, ScreenType Screen, int ScreenSize);
    }
}
