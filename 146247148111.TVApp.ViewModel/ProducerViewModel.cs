using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.ViewModel
{
    public partial class ProducerViewModel : ObservableValidator, IProducer
    {
        [ObservableProperty]
        private int iD;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Name must me longer than 1 character")]
        private string name;

        [ObservableProperty]
        private string country;

        public ProducerViewModel(IProducer producer) : this()
        {
            ID = producer.ID;
            name = producer.Name;
            country = producer.Country;
        }

        public ProducerViewModel() { }
    }
}
