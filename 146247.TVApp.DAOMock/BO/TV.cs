using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.DAOMock
{
    public class TV : ITV
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public int ScreenSize { get; set; }
        public ScreenType Screen { get; set; }
    }
}
