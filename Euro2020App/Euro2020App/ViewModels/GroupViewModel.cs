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
    public class GroupViewModel : BaseViewModel
    {
        public ObservableCollection<EuroGroup> Items { get; private set; }

        private EuroGroup _selectedItem;

        public EuroGroup SelectedItem
        {
            get => _selectedItem;
            set { SetProperty(ref _selectedItem, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }

        public INavigation Navigation { get; private set; }

        private string _service;

        private async Task LoadData()
        {
            IsBusy = true;

            var data = await WebApiService.GetData<EuroGroup>(_service);

            foreach (var item in data)
            {
                Items.Add(item);
            }

            IsBusy = false;
        }

        public GroupViewModel(INavigation navigation)
        {
            Items = new ObservableCollection<EuroGroup>();
            Navigation = navigation;
            _service = "EuroGroups";
            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailsCommand = new Command<Type>(async (page) => await GoToDetails(page));
        }

        protected async Task GoToDetails(Type page)
        {
            if (SelectedItem != null)
            {
                var newPage = (Page)Activator.CreateInstance(page);
                var viewModel = new GroupDetailViewModel(SelectedItem, Navigation);
                newPage.BindingContext = viewModel;
                await Navigation.PushAsync(newPage);
            }
        }
    }
}
