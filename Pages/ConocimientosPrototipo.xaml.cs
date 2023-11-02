using AppDibujoArtistico.BaseDatos;

namespace AppDibujoArtistico.Pages;

public partial class ConocimientosPrototipo : ContentPage
{
    Cls_Evaluaciones evaluaciones=new Cls_Evaluaciones();
	public ConocimientosPrototipo()
	{
		InitializeComponent();
	}

    private async void btnPrincipiante_Clicked(object sender, EventArgs e)
    {
        
        if ((LoginPage.nota>=0 && LoginPage.nota<=5))
        {
            LoginPage.examen = "Principiante";
            await Navigation.PushModalAsync(new Pages.ContenidoPage());
        }
        
    }

    private async void btnIntermedio_Clicked(object sender, EventArgs e)
    {
        if ((LoginPage.nota>= 6 && LoginPage.nota<=8) || (evaluaciones.seleccionarNotaExamen(LoginPage.id_alumno, "tb_puntuaciones_test1") >= 8))
        {
            LoginPage.examen = "Intermedio";
            await Navigation.PushModalAsync(new Pages.ContenidoPage());
        }
        else
        {
            await DisplayAlert("Nivel no desbloqueado", "Para acceder a este modulo tienes que aprobar el nivel Principiante", "Ok");
        }
    }

    private async void btnAvanzado_Clicked(object sender, EventArgs e)
    {
        if((LoginPage.nota>= 9 && LoginPage.nota <= 10) || (evaluaciones.seleccionarNotaExamen(LoginPage.id_alumno, "tb_puntuaciones_test2") >= 8))
        {
            LoginPage.examen = "Avanzado";
            await Navigation.PushModalAsync(new Pages.ContenidoPage());
        }
        else
        {
            await DisplayAlert("Nivel no desbloqueado", "Para acceder a este modulo tienes que aprobar el nivel intermedio","Ok");
        }
    }
}