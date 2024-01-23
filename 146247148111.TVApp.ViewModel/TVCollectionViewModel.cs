using _146247148111.TVApp.BLC;
using _146247148111.TVApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

            IsEditing = false;
            TvEdit = null;

            CancelCommand = new Command(
                execute: () =>
                {
                    TvEdit.PropertyChanged -= OnTvPropertyChanged;
                    TvEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () => IsEditing
                );


            RefreshCanExecute();
        }

        [ObservableProperty]
        private TVViewModelV2 tvEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewTV))]
        private void CreateNewTV()
        {
            TvEdit = new TVViewModelV2();
            TvEdit.PropertyChanged += OnTvPropertyChanged;
            IsEditing = true;
        }

        private bool CanCreateNewTV()
        {
            return !IsEditing;
        }

        [RelayCommand(CanExecute =nameof(CanEditTvBeSaved))]
        private void SaveTv()
        {
            Tvs.Add(TvEdit);
            TvEdit.PropertyChanged -= OnTvPropertyChanged;
            TvEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditTvBeSaved()
        {
            return TvEdit != null &&
                TvEdit.Name != null &&
                TvEdit.Name.Length > 2;
        }

        void OnTvPropertyChanged(object sender,PropertyChangedEventArgs args)
        {
            SaveTvCommand.NotifyCanExecuteChanged();
        }

        private void RefreshCanExecute()
        {
            CreateNewTVCommand.NotifyCanExecuteChanged();
            SaveTvCommand.NotifyCanExecuteChanged();

            (CancelCommand as Command).ChangeCanExecute();
        }

        public ICommand CancelCommand { get; set; }
    }
}
