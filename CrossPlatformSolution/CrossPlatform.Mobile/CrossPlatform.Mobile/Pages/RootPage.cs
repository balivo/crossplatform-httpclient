using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CrossPlatform.Mobile.Pages
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            this.Master = new MenuPage();
            this.Detail = new NavigationPage(new ProdutosPage());
        }
    }
}
