using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.ViewModel
{
    public partial class TVViewModelV2 : ObservableValidator, ITV
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage ="ID musi być nadane")]
        private int iD;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int producerId;

        [ObservableProperty]
        private IProducer? producer;

        [ObservableProperty]
        [DefaultValue(30)]
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

            ErrorsChanged += CVM_ErrorsChanged;
            PropertyChanged += CVM_PropertyChanged;
        }

        public TVViewModelV2() { }

        ~TVViewModelV2 () 
        { 
            ErrorsChanged -= CVM_ErrorsChanged;
            PropertyChanged -= CVM_PropertyChanged;
        }

        private void TvViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<string> AllScreens { get; } = Enum.GetNames(typeof(ScreenType));

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get
            {
                //pobieramy wszystkie błędy
                var geterr = GetErrors(null);

                //przeszukujemy słownik żeby znaleźć właściwości, dla których już błędów nie ma..
                foreach (var kv in errors.ToList())
                {
                    if (geterr.All(r => r.MemberNames.All(a => a != kv.Key)))
                    {
                        errors.Remove(kv.Key);

                    }
                }

                //wszystkie pozostałe błędy grupujemy
                var q = from ValidationResult e in geterr
                        from member in e.MemberNames
                        group e by member into gr
                        select gr;


                //tworzymy odpowiednie kolekcje w słowniku
                foreach (var prop in q)
                {
                    var messeges = prop.Select(r => r.ErrorMessage).ToList();
                    if (errors.ContainsKey(prop.Key))
                    {
                        errors.Remove(prop.Key);
                    }
                    errors.Add(prop.Key, messeges);
                    //RaiseErrorChanged(prop.Key);
                }
                //OnPropertyChanged(nameof(Errors));
                return errors;
            }
        }

        private void CVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void CVM_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }

        public IEnumerable<string> this[string propertyName]
        {
            get { return from ValidationResult e in GetErrors(propertyName) select e.ErrorMessage; }

        }

    }
}
