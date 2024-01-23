using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.ViewModel
{
    public partial class TVViewModelV2 : ObservableObject, ITV
    {
        [ObservableProperty]
        private int iD;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int producerId;

        [ObservableProperty]
        private IProducer? producer;

        [ObservableProperty]
        private int screenSize;

        [ObservableProperty]
        private ScreenType screen;

        public TVViewModelV2(ITV tv)
        {
            ID = tv.ID;
            Name = tv.Name;
            ProducerId = tv.ProducerId;
            Producer = tv.Producer;
            ScreenSize = tv.ScreenSize;
            Screen = tv.Screen;
        }

        public TVViewModelV2()
        {

        }
    }
}
