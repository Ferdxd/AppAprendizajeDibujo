namespace AppDibujoArtistico.Pages;

public partial class MenuInicialPage : ContentPage
{
	public MenuInicialPage()
	{
		InitializeComponent();
	}

    private async void btnTestInicial_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushModalAsync(new Pages.TestPage());
    }

    private async void btnIgnorarTest_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
    }
}