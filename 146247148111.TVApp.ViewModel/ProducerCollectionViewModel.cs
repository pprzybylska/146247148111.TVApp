﻿using _146247148111.TVApp.BLC;
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
using System.Collections.Specialized;

namespace _146247148111.TVApp.ViewModel
{
    public partial class ProducerCollectionViewModel: ObservableObject
    {
        //DeleteProducerByIdCommand

        [ObservableProperty]
        private ObservableCollection<ProducerViewModel>? producers;

        [ObservableProperty]
        private bool isEditing;

        // DAOLibraryName
        private BLC.BLC iColl = BLC.BLC.GetInstance();


        public ProducerCollectionViewModel()
        {
            producers = new ObservableCollection<ProducerViewModel>();

            foreach (var produc in iColl.GetProducers()) 
            {
                producers.Add( new ProducerViewModel(produc) );
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    ProducerEdit.PropertyChanged -= OnProducersPropertyChanged;
                    ProducerEdit = null;
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
                    iColl.DeleteProducerById(ProducerEdit.ID);
                    producers.Remove(ProducerEdit);
                    ProducerEdit.PropertyChanged -= OnProducersPropertyChanged;
                    ProducerEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return CanDelete();
                    //return true;
                });
        }

        [ObservableProperty]
        private bool isUpdateing;

        private ProducerViewModel producerEdit;
        public ProducerViewModel ProducerEdit
        {
            get { return producerEdit; }
            set { SetProperty(ref producerEdit, value); }
        }


        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProducerEdit = new ProducerViewModel();
            ProducerEdit.PropertyChanged += OnProducersPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private bool CanCreateNewProducer()
        {
            return !IsEditing;
        }

        private bool CanDelete()
        {
            return ProducerEdit != null;
        }

        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved))]
        private void SaveProducer()
        {
            iColl.CreateNewProducer(ProducerEdit.Name, ProducerEdit.Country);
            RefreshList();
            ProducerEdit.PropertyChanged -= OnProducersPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void UpdateTv()
        {
            if (ProducerEdit != null)
            {
                ProducerEdit.PropertyChanged += OnProducersPropertyChanged;
                IsUpdateing = true;
                IsEditing = true;
                RefreshCanExecute();
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved2))]
        private void SaveUpdate()
        {
            iColl.UpdateProducer(ProducerEdit.ID, ProducerEdit.Name, ProducerEdit.Country);
            RefreshList();
            ProducerEdit.PropertyChanged -= OnProducersPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            IsUpdateing = false;
            RefreshCanExecute();
        }

        private void RefreshList() {
            producers.Clear();

            foreach (var produc in iColl.GetProducers())
            {
                producers.Add(new ProducerViewModel(produc));
            }
        }

        private bool CanEditProducerBeSaved()
        {
            return ProducerEdit != null &&
                ProducerEdit.Name != null &&
                ProducerEdit.Name.Length > 1 &&
                !isUpdateing;
        }

        private bool CanEditProducerBeSaved2()
        {
            return ProducerEdit != null &&
                ProducerEdit.Name != null &&
                ProducerEdit.Name.Length > 1 &&
                isUpdateing;
        }


        void OnProducersPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
            UpdateTvCommand.NotifyCanExecuteChanged();
            SaveUpdateCommand.NotifyCanExecuteChanged();
        }

        private void RefreshCanExecute()
        {
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            SaveProducerCommand.NotifyCanExecuteChanged();

            UpdateTvCommand.NotifyCanExecuteChanged();
            SaveUpdateCommand.NotifyCanExecuteChanged();

            (CancelCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
        }

        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

    }
}
