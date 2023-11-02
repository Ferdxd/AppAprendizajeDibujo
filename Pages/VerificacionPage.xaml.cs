using AppDibujoArtistico.BaseDatos;
using AppDibujoArtistico.Correo;

namespace AppDibujoArtistico.Pages;

public partial class VerificacionPage : ContentPage
{
    public string tk;
    public string correo;
	public VerificacionPage()
	{
		InitializeComponent();

       correo = RegistroPage.Correo;
        ClsCorreo clscorreo = new ClsCorreo();
        tk=clscorreo.enviarCorreo(correo.ToString()).ToString();
    }

    private async void btnVerificacion_Clicked(object sender, EventArgs e)
    {
        Cls_tb_usuarios cls_Tb_Usuarios = new Cls_tb_usuarios();
        string token_Entrada = txtVerificacion.Text.ToString();
        int id = 0;

        try
        {
            id = cls_Tb_Usuarios.encontrarAlumnoMail(correo);
            if (cls_Tb_Usuarios.verificacionToken(id, tk, token_Entrada))
            {
                
                await DisplayAlert("Verificacion Completada", "Tu cuenta se encuentra activa para poder usar la app ingresa tus datos en el inicio de sesion", "Ok");
            }
            else
            {
                await DisplayAlert("Verificacion fallida", "Al parecer el token que has ingresado es incorrecto","Ok");
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error", ""+ex, "Ok");
        }
       
        
       
    }
}