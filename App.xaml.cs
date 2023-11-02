using AppDibujoArtistico.Pages;
using AppDibujoArtistico.Pages.PagesAdmin;
namespace AppDibujoArtistico
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new InsercionTestPage());
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new VerificacionPage());
            //MainPage = new NavigationPage(new MenuInicialPage());
            //MainPage = new NavigationPage(new TestPage());
            // MainPage=new NavigationPage(new TestPage());
            // MainPage = new ConocimientosPage();
           
            
        
        }
    }
}