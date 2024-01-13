using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.MAUI.ViewModels
{
    public partial class TVViewModel : ObservableObject, ITV
    {
        [ObservableProperty]
        private int iD;



        //public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ProducerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProducer Producer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ScreenSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ScreenType Screen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
