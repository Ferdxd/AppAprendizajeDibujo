using AppDibujoArtistico.BaseDatos;
using AppDibujoArtistico.Correo;
using MySql.Data.MySqlClient;

namespace AppDibujoArtistico.Pages;

public partial class RegistroPage : ContentPage
{
	public RegistroPage()
	{
		InitializeComponent();
	}

	public static string Correo { get; set; }
	
	

    private async void btnRegistrarse_Clicked(object sender, EventArgs e)
    {

		string nombre = txtNombre.Text.ToString();
		string apellido = txtApellido.Text.ToString();
		string correo= txtRegistrarCorreo.Text.ToString();
		string contraseña = txtRegistrarContraseña.Text.ToString();
        Correo = correo;

        Cls_tb_usuarios cls_Tb_Usuarios = new Cls_tb_usuarios();

		if (!string.IsNullOrWhiteSpace(nombre)&& !string.IsNullOrWhiteSpace(apellido) && !string.IsNullOrWhiteSpace(correo)&& !string.IsNullOrWhiteSpace(contraseña))
		{
            try
            {
				cls_Tb_Usuarios.crearUsuario(nombre, apellido, correo, contraseña, "Sin Verificar");
				
				await DisplayAlert("Primer paso Completado","a continuacion se te enviara un token a tu correo que deberas ingresar en donde se te pida","Ok");
				await Navigation.PushModalAsync(new Pages.VerificacionPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error: " + ex.ToString(), "Ocurrio un error al crear el usuario", "Ok");
			
            }
		}
		else
		{
			await DisplayAlert("Campos Vacíos", "Todos los campos son obligatorios", "Ok");
		}
	
	
		
    }

    //ClsCorreo clscorreo = new ClsCorreo();
    //tk=clscorreo.enviarCorreo(correo).ToString();
    //id=cls_Tb_Usuarios.encontrarAlumnoMail(correo);
    //cls_Tb_Usuarios.verificacionToken(id);
}