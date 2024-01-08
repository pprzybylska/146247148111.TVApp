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
        IProducer CreateNewProducer(string Name, string Country);
        ITV CreateNewTV(string Name, string ProducerName, ScreenType Screen, int ScreenSize);
        bool DeleteProducerById(int producerId);
        bool DeleteTVById(int TVId);
        IProducer UpdateProducer(int ID, string Name, string Country);
        ITV UpdateTV(int ID, string Name, string ProducerName, ScreenType Screen, int ScreenSize);
        IEnumerable<ITV> SearchTVsByKeyword(string keyword);
        public IEnumerable<ITV> FilterByProducer(string producer);
        public IEnumerable<ITV> FilterByScreenSize(double? minSize, double? maxSize);
        public IEnumerable<ITV> FilterByScreenType(ScreenType screenType);
    }
}
