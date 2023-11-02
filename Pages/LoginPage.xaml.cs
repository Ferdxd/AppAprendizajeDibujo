using AppDibujoArtistico.Archivos;
using AppDibujoArtistico.BaseDatos;
using AppDibujoArtistico.Correo;

namespace AppDibujoArtistico.Pages;

public partial class LoginPage : ContentPage
{
    public static string Correo { get; private set; }
    public static int nota { get; private set; }
    public static int id_alumno { get; private set; }

    public static string nombre_Completo { get; private set; }

    public static string examen { get; set; }
	public LoginPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        ClsManipulacionImagenes manipulacionImagenes = new ClsManipulacionImagenes();
        Cls_tb_nivel1 cls_Tb_Nivel1=new Cls_tb_nivel1();
        //Listar imagenes y cargarlas a la base de datos
       // List<string> listaImagenes = manipulacionImagenes.ListarImagenesEnCarpeta(@"D:\Dowloads Chrome\Principiante\Avanzado");
        //byte[] imagenuwu = cls_Tb_Nivel1.ConvertirImagenABlob(imagen);
        //cls_Tb_Nivel1.insertarImagen(imagenuwu);
       /* foreach (string imagen in listaImagenes)
        {
            byte[] imagenuwu = cls_Tb_Nivel1.ConvertirImagenABlob(imagen);
            cls_Tb_Nivel1.InsertarImagen(2, imagenuwu, null);
        }*/
       // byte[] imagenuwu = cls_Tb_Nivel1.ConvertirImagenABlob(@"D:\Dowloads Chrome\Principiante\Cabezas\29.png");
       // cls_Tb_Nivel1.InsertarImagen(1,imagenuwu,null);
        //listaImagenes.Add("Hola");
        
        // D:\Dowloads Chrome\Principiante\Cabezas

    
        Cls_tb_usuarios cls_Tb_Usuarios = new Cls_tb_usuarios();
        List<string> datos= new List<string>();
        Cls_Evaluaciones cls_Evaluaciones = new Cls_Evaluaciones();
        ClsConexion conexion= new ClsConexion();
        /* try
         {
             conexion.abrirConexion();
             await DisplayAlert("conexion obtenida", "", "Ok");
         }
         catch(Exception ex)
         {
             await DisplayAlert("error conexion no obtenida", ""+ex, "Ok");
         }
         finally { conexion.cerrarConexion(); }
        */
        if (!string.IsNullOrWhiteSpace(txtCorreoElectronico.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            try
            {
                Correo = txtCorreoElectronico.Text.ToString();

                id_alumno = cls_Tb_Usuarios.encontrarAlumnoMail(Correo);
                if (id_alumno!=0)
                {
                    
                    nota = cls_Evaluaciones.seleccionarNotaUbi(id_alumno);
                    datos = cls_Tb_Usuarios.Obtener_Fila(Correo);
                    nombre_Completo = datos[0].ToString() + " " + datos[1].ToString();
                    examen = "Test_Ubicacion";
                   
              

                        if ((txtCorreoElectronico.Text.ToString() == datos[2].ToString()) & (txtPassword.Text.ToString() == datos[3].ToString()))
                        {
                            await DisplayAlert("Bienvenido", datos[0] + " " + datos[1], "Ok");
                            if (nota > 0)
                            {
                                await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
                            }
                            else
                            {
                                await Navigation.PushModalAsync(new Pages.MenuInicialPage());
                            }



                        }
                        else
                        {
                            await DisplayAlert("Error", "Contraseña incorrecta", "Ok");
                        }
                    
                }
                else
                {
                    await DisplayAlert("Error", "El correo no esta registrado, regresa y registrate para poder acceder", "Ok");
                }







            }
            catch (Exception ex)
            {
                await DisplayAlert("Error: ", "Se ha producido un error \n" + ex, "Ok");
            }
        }
        else
        {
            await DisplayAlert("Error", "Es obligatorio llenar todos los campos", "Ok");
        }


        
        

    }

    private async void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Pages.RegistroPage());
    }
}