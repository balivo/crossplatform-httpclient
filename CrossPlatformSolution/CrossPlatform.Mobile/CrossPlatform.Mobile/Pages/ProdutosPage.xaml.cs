using CrossPlatform.Mobile.Data.Dtos;
using CrossPlatform.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CrossPlatform.Mobile.Pages
{
    public partial class ProdutosPage : ContentPage
    {
        public ProdutosPage()
        {
            this.InitializeComponent();

            this.BindingContext = new ProdutosViewModel(this.Navigation);
        }

        //ItemTapped="Handle_ItemTapped"
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e) => ((ListView)sender).SelectedItem = null;

        //ItemSelected="Handle_ItemSelected"
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var _selected = ((ListView)sender).SelectedItem as ProdutoDto;

                if (_selected == null)
                    return;

                await ((ProdutosViewModel)this.BindingContext).Editar(_selected);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected override void OnAppearing()
        {
            ((ProdutosViewModel)this.BindingContext).RefreshCommand.Execute(null);

            base.OnAppearing();
        }
    }
}
