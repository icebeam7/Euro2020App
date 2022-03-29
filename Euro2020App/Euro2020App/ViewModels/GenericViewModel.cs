using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Essentials;

using Euro2020App.Models;
using Euro2020App.Services;

namespace Euro2020App.ViewModels
{
    public class GenericViewModel<T> : BaseViewModel
    {
        public ObservableCollection<T> Items { get; private set; }

        private T _selectedItem;

        public T SelectedItem
        {
            get => _selectedItem;
            set { SetProperty(ref _selectedItem, value); }
        }

        private string _service;

        private async Task LoadData()
        {
            var data = await WebApiService.GetData<T>(_service);
            Items = new ObservableCollection<T>(data);
        }

        public GenericViewModel(string service)
        {
            _service = service;
            Task.Run(async () => await LoadData());
        }
    }
}
