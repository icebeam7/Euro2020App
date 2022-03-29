using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Euro2020App.Models;
using Euro2020App.ViewModels;

namespace Euro2020App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupView : ContentPage
    {
        GroupViewModel vm;

        public GroupView()
        {
            InitializeComponent();

            vm = new GroupViewModel(this.Navigation);
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.Items.Count == 0)
                await Task.Run(() => vm.LoadDataCommand.Execute(null));
            else
                vm.SelectedItem = null;
        }
    }
}