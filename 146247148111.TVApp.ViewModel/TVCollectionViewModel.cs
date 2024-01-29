using _146247148111.TVApp.BLC;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Globalization;
using _146247148111.TVApp.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Graphics;
using System.Reflection;

namespace _146247148111.TVApp.ViewModel
{
    public partial class TVCollectionViewModel : ObservableObject
    {
        // DAOLibraryName
        private BLC.BLC iColl;
        //= BLC.BLC.GetInstance("C:\\Users\\neuba\\Documents\\PW\\146247148111.TVApp\\146247148111.TVApp.ViewModel\\bin\\Debug\\net8.0-windows10.0.19041.0\\146247148111.TVApp.DAOMock.dll");


        [ObservableProperty]
        private ObservableCollection<TVViewModelV2> tvs;

        [ObservableProperty]
        public ObservableCollection<string> types;

        [ObservableProperty]
        public ObservableCollection<ProducerViewModel> producers;

        [ObservableProperty]
        private string searchString = "";

        [ObservableProperty]
        private ProducerViewModel filterProducer;

        [ObservableProperty]
        Double filterScreenSizeMax = 100;

        [ObservableProperty]
        Double filterScreenSizeMin = 0;

        [ObservableProperty]
        ScreenType filterScreenType;

        [RelayCommand]
        private void Search()
        {
            tvs.Clear();
            
            IEnumerable<ITV> newTV = iColl.GetTVs();
            if (SearchString != "")
            {
                newTV = iColl.SearchTVsByKeyword(SearchString);
            }
            if (newTV.Count() > 0)
            {
                foreach (var newTv in newTV)
                {
                    tvs.Add(new TVViewModelV2(newTv));
                }
            }
        }

        [RelayCommand]
        private void FilterProducerFunction()
        {
            tvs.Clear();

            IEnumerable<ITV> newTV = iColl.GetTVs();
            if (FilterProducer != null && FilterProducer.Name != "")
            {
                newTV = iColl.FilterByProducer(FilterProducer.Name);
            }
            if (newTV.Count() > 0)
            {
                foreach (var newTv in newTV)
                {
                    tvs.Add(new TVViewModelV2(newTv));
                }
            }
        }

        [RelayCommand]
        private void FilterScreenTypeFunction()
        {
            tvs.Clear();

            IEnumerable<ITV> newTV = iColl.GetTVs();
            if (FilterScreenType != null)
            {
                newTV = iColl.FilterByScreenType(FilterScreenType);
            }

            if (newTV.Count() > 0)
            {
                foreach (var newTv in newTV)
                {
                    tvs.Add(new TVViewModelV2(newTv));
                }
            }
        }

        [RelayCommand]
        private void FilterScreenSizeFunction()
        {
            tvs.Clear();

            IEnumerable<ITV> newTV = iColl.GetTVs();
            
            newTV = iColl.FilterByScreenSize(FilterScreenSizeMin, FilterScreenSizeMax);
            if (newTV.Count() > 0)
            {
                foreach (var newTv in newTV)
                {
                    tvs.Add(new TVViewModelV2(newTv));
                }
            }
        }

        [RelayCommand]
        public void ResetFilters()
        {
            SearchString = "";
            FilterScreenSizeMin = 0;
            FilterScreenSizeMax = 100;
            Search();
        }

        public TVCollectionViewModel()
        {

            iColl = BLC.BLC.GetInstance();

            tvs = new ObservableCollection<TVViewModelV2>();

            foreach (var tv in iColl.GetTVs())
            {
                tvs.Add(new TVViewModelV2(tv));
            }

            Producers = new ObservableCollection<ProducerViewModel>();

            foreach (var producer in iColl.GetProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }

            List<string> types = Enum.GetNames(typeof(ScreenType)).ToList();
            Types = new ObservableCollection<string>(types);

            CancelCommand = new Command(
                execute: () =>
                {
                    TvEdit.PropertyChanged -= OnTvPropertyChanged;
                    if (prod != null)
                    {
                        prod.PropertyChanged -= OnTvPropertyChanged;
                    }
                    TvEdit = null;
                    prod = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return IsEditing;
                });

            DeleteCommand = new Command(
                execute: () =>
                {
                    bool v = iColl.DeleteTVById(TvEdit.ID);
                    Tvs.Remove(TvEdit);
                    TvEdit.PropertyChanged -= OnTvPropertyChanged;
                    TvEdit = null;
                    prod = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return CanDelete();
                });

        }

        [ObservableProperty]
        private TVViewModelV2 tvEdit;
        private ProducerViewModel prod;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isUpdateing;

        [RelayCommand(CanExecute = nameof(CanCreateNewTV))]
        private void CreateNewTV()
        {
            producers.Clear();
            foreach (var producer in iColl.GetProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }

            TvEdit = new TVViewModelV2();
            prod = new ProducerViewModel();
            prod.PropertyChanged += OnTvPropertyChanged;
            TvEdit.PropertyChanged += OnTvPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private bool CanCreateNewTV()
        {
            return !IsEditing;
        }

        private bool CanDelete()
        {
            return TvEdit != null; ;
        }

        [RelayCommand(CanExecute = nameof(CanCreateNewTV))]
        private void UpdateTv()
        {
            if (TvEdit != null) 
            { 
                TvEdit.PropertyChanged += OnTvPropertyChanged;
                IsUpdateing = true;
                IsEditing = true;
                RefreshCanExecute();
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditTvBeSaved2))]
        private void SaveUpdate()
        {
            iColl.UpdateTV(TvEdit.ID, TvEdit.Name, TvEdit.Producer.Name, TvEdit.Screen, TvEdit.ScreenSize);
            RefreshList();
            TvEdit.PropertyChanged -= OnTvPropertyChanged;
            TvEdit = null;
            IsEditing = false;
            IsUpdateing = false;
            RefreshCanExecute();
        }

        private void RefreshList()
        {
            tvs.Clear();
            producers.Clear();

            foreach (var tmp_tv in iColl.GetTVs())
            {
                tvs.Add(new TVViewModelV2(tmp_tv));
            }

            foreach (var producer in iColl.GetProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }
        }

        [RelayCommand(CanExecute =nameof(CanEditTvBeSaved))]
        private void SaveTv()
        {
            iColl.CreateNewTV(TvEdit.Name, TvEdit.Producer.Name.ToString(), TvEdit.Screen, TvEdit.ScreenSize);
            RefreshList();
            TvEdit.PropertyChanged -= OnTvPropertyChanged;
            TvEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditTvBeSaved()
        {
            return TvEdit != null &&
                TvEdit.Name != null &&
                TvEdit.Name.Length > 1 &&
                TvEdit.ProducerId != null &&
                TvEdit.ScreenSize > 20 &&
                !isUpdateing;
        }

        private bool CanEditTvBeSaved2()
        {
            return TvEdit != null &&
                TvEdit.Name != null &&
                TvEdit.Name.Length > 1 &&
                TvEdit.ProducerId != null &&
                TvEdit.ScreenSize > 20 &&
                isUpdateing;
        }

        void OnTvPropertyChanged(object sender,PropertyChangedEventArgs args)
        {
            SaveTvCommand.NotifyCanExecuteChanged();
            UpdateTvCommand.NotifyCanExecuteChanged();
            SaveUpdateCommand.NotifyCanExecuteChanged();
        }

        private void RefreshCanExecute()
        {
            CreateNewTVCommand.NotifyCanExecuteChanged();
            SaveTvCommand.NotifyCanExecuteChanged();

            UpdateTvCommand.NotifyCanExecuteChanged();
            SaveUpdateCommand.NotifyCanExecuteChanged();

            (CancelCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
        }

        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public IConfiguration Configuration { get; }
    }

}
