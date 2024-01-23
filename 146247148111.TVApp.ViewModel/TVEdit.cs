using _146247148111.TVApp.Interfaces;
using _146247148111.TVApp.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace _146247148111.TVApp.ViewModel
{
    public partial class TVEdit : ObservableObject
    {
        [ObservableProperty]
        private TVViewModelV2 tvEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute =nameof(CanCreateNewTV))]
        private void CreateNewTV()
        {
            TvEdit = new TVViewModelV2();
            IsEditing = true;
        }

        private bool CanCreateNewTV()
        {
            return !IsEditing;
        }
    }
}
