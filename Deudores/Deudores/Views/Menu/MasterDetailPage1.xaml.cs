using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Deudores.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            switch (page.Title)
            {
                case "Deudores":
                    Detail = new NavigationPage(new MasterDetailPage1Detail());
                    IsPresented = false;
                    break;

                case "Contactos":
                    Detail = new NavigationPage(new DeudoresListPage());
                    IsPresented = false;
                    break;

                case "Deuda total":
                    Detail = new NavigationPage(new DeudaTotalPage());
                    IsPresented = false;
                    break;

                default:
                    break;
            }

            MasterPage.ListView.SelectedItem = null;
        }
    }
}