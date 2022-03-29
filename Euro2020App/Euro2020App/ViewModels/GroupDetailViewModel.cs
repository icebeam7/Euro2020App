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
    public class GroupDetailViewModel : BaseViewModel
    {
        private EuroGroup _currentItem;

        public EuroGroup CurrentItem
        {
            get => _currentItem;
            set { SetProperty(ref _currentItem, value); }
        }

        public ObservableCollection<EuroTeam> Items { get; private set; }

        private EuroTeam _selectedItem;

        public EuroTeam SelectedItem
        {
            get => _selectedItem;
            set { SetProperty(ref _selectedItem, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand PrintCommand { private set; get; }

        public INavigation Navigation { get; private set; }

        private string _service;

        private async Task LoadData()
        {
            IsBusy = true;

            var data = await WebApiService.GetData<EuroTeam>(_service);

            foreach (var item in data.Where(x => x.EuroGroupId == CurrentItem.Id))
            {
                Items.Add(item);
            }

            IsBusy = false;
        }

        private async Task Print()
        {
            await PrintService.CreatePdf(CurrentItem, Items.ToList());
        }

        public GroupDetailViewModel(EuroGroup item, INavigation navigation)
        {
            CurrentItem = item;
            Items = new ObservableCollection<EuroTeam>();
            Navigation = navigation;
            _service = "EuroTeams";

            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailsCommand = new Command<Type>(async (page) => await GoToDetails(page));
            PrintCommand = new Command(async () => await Print());


            Task.Run(async () => await LoadData());
        }

        protected async Task GoToDetails(Type page)
        {
            if (SelectedItem != null)
            {
                var newPage = (Page)Activator.CreateInstance(page);
                var viewModel = new TeamDetailViewModel(SelectedItem);
                newPage.BindingContext = viewModel;
                await Navigation.PushAsync(newPage);
            }
        }
    }
}
