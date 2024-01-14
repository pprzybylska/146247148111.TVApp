using _146247148111.TVApp.BLC;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _146247148111.TVApp.ViewModel
{
    public partial class TVCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TVViewModelV2> tvs;

        // DAOLibraryName
        private BLC.BLC iColl = BLC.BLC.GetInstance("C:\\Users\\neuba\\Documents\\PW\\146247148111.TVApp\\146247148111.TVApp.ViewModel\\bin\\Debug\\net8.0-windows10.0.19041.0\\146247148111.TVApp.DAOMock.dll");

        public TVCollectionViewModel()
        {
            tvs = new ObservableCollection<TVViewModelV2>();

            foreach (var tv in iColl.GetTVs())
            {
                tvs.Add(new TVViewModelV2(tv));
            }
        }
    }
}
