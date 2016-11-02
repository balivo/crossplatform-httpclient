using CrossPlatform.Backend.Clients;
using CrossPlatform.Backend.Clients.Produtos;
using CrossPlatform.Backend.Clients.Data.Dtos;
using CrossPlatform.Mobile.Pages;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossPlatform.Mobile.ViewModels
{
    sealed class ProdutosViewModel : BaseViewModel
    {
        private readonly INavigation _Navigation;

        public ProdutosViewModel(INavigation pNavigation)
        {
            this._Navigation = pNavigation;
        }

        public ObservableCollection<ProdutoDto> Produtos { get; private set; } = new ObservableCollection<ProdutoDto>();

        private Command _RefreshCommand;

        public Command RefreshCommand => this._RefreshCommand ?? (this._RefreshCommand = new Command(
            async () => await RefreshCommandExecute(),
            () => RefreshCommandCanExecute()));

        private bool RefreshCommandCanExecute()
        {
            return this.IsNotBusy;
        }

        internal async Task Editar(ProdutoDto _selected)
        {
            await this._Navigation.PushAsync(new ProdutoPage(_selected.Id));
        }

        private async Task RefreshCommandExecute()
        {
            try
            {
                if (this.IsBusy)
                    return;

                this.IsBusy = true;
                this.RefreshCommand.ChangeCanExecute();

                var _result = await ProdutosClient.Get();

                this.Produtos.Clear();

                foreach (var item in _result)
                {
                    this.Produtos.Add(item);
                }
            }
            catch (InvalidOperationException ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message, "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ah não!", ex.Message, "Ok");
            }
            finally
            {
                this.IsBusy = false;
                this.RefreshCommand.ChangeCanExecute();
            }
        }

        private Command _NovoCommand;
        public Command NovoCommand => this._NovoCommand ?? (this._NovoCommand = new Command(
            async () => await NovoCommandExecute()));

        private async Task NovoCommandExecute()
        {
            await this._Navigation.PushAsync(new ProdutoPage());
        }

        private Command _AutenticarHttpCommand;
        public Command AutenticarHttpCommand => this._AutenticarHttpCommand ?? (this._AutenticarHttpCommand = new Command(async () => await AutenticarHttpCommandExecute()));

        private async Task AutenticarHttpCommandExecute()
        {
            try
            {
                if (this.IsBusy)
                    return;

                this.IsBusy = true;
                this.RefreshCommand.ChangeCanExecute();
                this.AutenticarHttpCommand.ChangeCanExecute();

                await CrossPlatformHttpClientService.Current.Autenticar();
            }
            catch (InvalidOperationException ex)
            {
                await App.Current.MainPage.DisplayAlert("Oops", ex.Message, "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ah não!", ex.Message, "Ok");
            }
            finally
            {
                this.IsBusy = false;
                this.RefreshCommand.ChangeCanExecute();
                this.AutenticarHttpCommand.ChangeCanExecute();
            }
        }
    }
}

