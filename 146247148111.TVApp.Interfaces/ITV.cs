using _146247148111.TVApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.Interfaces
{
    public interface ITV
    {
        int ID { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        int ScreenSize { get; set; }
        ScreenType Screen { get; set; }
    }
}
