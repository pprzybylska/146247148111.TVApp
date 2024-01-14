using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.ViewModel
{
    public partial class ProducerViewModel : ObservableObject, IProducer
    {
        [ObservableProperty]
        private int iD;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string country;

        public ProducerViewModel(IProducer producer) 
        {
            ID = producer.ID;
            name = producer.Name;
            country = producer.Country;
        }
    }
}
