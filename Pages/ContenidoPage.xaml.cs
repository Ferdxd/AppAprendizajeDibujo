using AppDibujoArtistico.BaseDatos;

namespace AppDibujoArtistico.Pages;

public partial class ContenidoPage : ContentPage
{


        Dictionary<string, List<Tuple<byte[], string>>> contenidoVisual;
        int indiceImagenActual = 0;
        string nombreEvaluacion = LoginPage.examen; 
    Cls_tb_nivel1 cls_Tb_Nivel = new Cls_tb_nivel1();
        public ContenidoPage()
        {
            InitializeComponent();
    
        
            contenidoVisual = cls_Tb_Nivel.ObtenerContenidoVisualPorEvaluacion(nombreEvaluacion);

            if (contenidoVisual.ContainsKey(nombreEvaluacion) && contenidoVisual[nombreEvaluacion].Count > 0)
            {
                DisplayAlert("Bienvenido al nivel ", ""+LoginPage.examen, "OK");
            }

            MostrarImagenYDescripcion(indiceImagenActual);
    }

        private void MostrarImagenYDescripcion(int indice)
        {
            if (contenidoVisual.ContainsKey(nombreEvaluacion) && indice >= 0 && indice < contenidoVisual[nombreEvaluacion].Count)
            {
                Tuple<byte[], string> contenido = contenidoVisual[nombreEvaluacion][indice];
                MostrarImagenEnEtiqueta(contenido.Item1);
                MostrarDescripcionEnEtiqueta(contenido.Item2);
            }
        }

        private void MostrarDescripcionEnEtiqueta(string descripcion)
        {
            lblDescripcion.Text = descripcion;
        }

        private void MostrarImagenEnEtiqueta(byte[] imagenData)
        {
            try
            {
                if (imagenData == null)
                {
                    DisplayAlert("Error", "", "OK");
                }
                ImageSource imagenSource = ImageSource.FromStream(() => new MemoryStream(imagenData));
                imgPrincipiante.Source = imagenSource;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Error al mostrar la imagen: " + ex.Message, "Aceptar");
            }
        }

        private void btnNextCabezas_Clicked(object sender, EventArgs e)
        {
            indiceImagenActual++;
        try
        {
            MostrarImagenYDescripcion(indiceImagenActual);
        }
        catch
        {
            indiceImagenActual = 0;
        }
           
        }

        private void BtnPreviousCabezas_Clicked(object sender, EventArgs e)
        {
            indiceImagenActual--;
        try
        {
            MostrarImagenYDescripcion(indiceImagenActual);
        }
        catch
        {
            indiceImagenActual = 0;
        }
            
        }

    private async void btnEmpezarEvaluacion_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Pregunta", "¿Te sientes preparado para iniciar la prueba corta de este curso?", "Sí", "No");

        if (answer)
        {
            // El usuario hizo clic en "Sí"
            await Navigation.PushModalAsync(new Pages.TestPage());
        }
        else
        {
            // El usuario hizo clic en "No"
            await DisplayAlert("Has decidido no realizarte el examen", "aprovecha a repasar los detalles de los dibujos y cuando te sientas listo avanza nuevamente hasta aca", "OK");
        }


    }












    /*
  //  List<byte[]> directionsCabezas = new List<byte[]>();
    List<byte[]> imagenes=new List<byte[]>();
    Cls_tb_nivel1 tb_Nivel1 = new Cls_tb_nivel1();
    int indiceImagenActual = 0;
    public Principiante()
	{
		InitializeComponent();
     
        tb_Nivel1.cargarImagenes(imagenes);
        if (imagenes.Count > 0)
        {
            DisplayAlert("Las imagenes se han cargado correctamente", "", "ok");
        }

        MostrarImagenEnEtiqueta(imagenes[indiceImagenActual]);
   
    }

    private void MostrarImagenEnEtiqueta(byte[] imagenData)
    {
        try
        {
            if (imagenData == null)
            {
                DisplayAlert("error", "", "Ok");
            }
            ImageSource imagenSource =ImageSource.FromStream(() => new MemoryStream(imagenData));
            imgPrincipiante.Source = imagenSource;

            
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al mostrar la imagen: " + ex.Message, "Aceptar");
        }
    }

    private async void btnNextCabezas_Clicked(object sender, EventArgs e)
    {
        indiceImagenActual++;
        try
        {
          
            MostrarImagenEnEtiqueta(imagenes[indiceImagenActual]);
            
        }catch
        {
      

       
            bool answer = await DisplayAlert("Pregunta", "¿Te sientes preparado para iniciar la prueba corta de este curso?", "Sí", "No");

            if (answer)
            {
                // El usuario hizo clic en "Sí"
                await Navigation.PushModalAsync(new Pages.TestPage());
            }
            else
            {
                // El usuario hizo clic en "No"
                await DisplayAlert("Has decidido no realizarte el examen", "aprovecha a repasar los detalles de los dibujos y cuando te sientas listo avanza nuevamente hasta aca", "OK");
            }
        

        
        }
     
    }
   

    private void BtnPreviousCabezas_Clicked(object sender, EventArgs e)
    {
        indiceImagenActual--;
        try
        {
            MostrarImagenEnEtiqueta(imagenes[indiceImagenActual]);
        }
        catch (Exception ex)
        {
            indiceImagenActual = 0;
        }
    }*/
}