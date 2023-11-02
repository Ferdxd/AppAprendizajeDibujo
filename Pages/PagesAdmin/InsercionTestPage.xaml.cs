using AppDibujoArtistico.BaseDatos;

namespace AppDibujoArtistico.Pages.PagesAdmin;

public partial class InsercionTestPage : ContentPage
{
	public InsercionTestPage()
	{
		InitializeComponent();
	}

    private void btnBorrarDatos_Clicked(object sender, EventArgs e)
    {
        txtPregunta.Text = "";
        txtRespuesta1.Text = "";
        txtRespuesta2.Text = "";
        txtRespuesta3.Text = "";
        txtCorrecta1.Text = "";
        txtCorrecta2.Text = "";
        txtCorrecta3.Text = "";
        txtIdEvaluacion.Text = "";
    }

    private async void btnGuardarEvaluacion_Clicked(object sender, EventArgs e)
    {
        int idEvaluacion = Int32.Parse(txtIdEvaluacion.Text);
        Cls_InsercionContenidoTest contenidoTest = new Cls_InsercionContenidoTest();
        try
        {
            int id_pregunta = contenidoTest.buscarIdPregunta(txtPregunta.Text.ToString());
            contenidoTest.insertarEvaluaciones_Preguntas(idEvaluacion, id_pregunta);
            await DisplayAlert("Proceso Completado", "Los datos han sido almacenados correctamente", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error:" + ex, "se ha producido un error en la insercion de los datos", "ok");
        }
    }

    private async void btnGuardarPregunta_Clicked(object sender, EventArgs e)
    {
        Cls_InsercionContenidoTest contenidoTest= new Cls_InsercionContenidoTest();
        try
        {
            contenidoTest.insertarPregunta(txtPregunta.Text.ToString());
            await DisplayAlert("Proceso Completado", "Los datos han sido almacenados correctamente", "Ok");
        }catch (Exception ex)
        {
            await DisplayAlert("Error:" + ex, "se ha producido un error en la insercion de los datos", "ok");
        }
        

    }

    private async void btnGuardarRespuestas_Clicked(object sender, EventArgs e)
    {
        Cls_InsercionContenidoTest contenidoTest = new Cls_InsercionContenidoTest();
        int istrue1 = Int32.Parse(txtCorrecta1.Text);
        int istrue2 = Int32.Parse(txtCorrecta2.Text);
        int istrue3 = Int32.Parse(txtCorrecta3.Text);
        int id_pregunta = 0; 
        id_pregunta=contenidoTest.buscarIdPregunta(txtPregunta.Text.ToString());

        
        try
        {
            if (istrue1==1)
            {
                contenidoTest.insertarRespuesta(txtRespuesta1.Text.ToString(), true, id_pregunta);
            }
            else
            {
                contenidoTest.insertarRespuesta(txtRespuesta1.Text.ToString(), false, id_pregunta);
            }
            if (istrue2 == 1)
            {
                contenidoTest.insertarRespuesta(txtRespuesta2.Text.ToString(), true, id_pregunta);
            }
            else
            {
                contenidoTest.insertarRespuesta(txtRespuesta2.Text.ToString(), false, id_pregunta);
            }
            if (istrue3 == 1)
            {
                contenidoTest.insertarRespuesta(txtRespuesta3.Text.ToString(), true, id_pregunta);
            }
            else
            {
                contenidoTest.insertarRespuesta(txtRespuesta3.Text.ToString(), false, id_pregunta);
            }

            await DisplayAlert("Proceso Completado", "Los datos han sido almacenados correctamente", "Ok");

        }
        catch(Exception ex) 
        {
            await DisplayAlert("Error:" + ex, "se ha producido un error en la insercion de los datos", "ok");
        }

        int stop2 = 0;
        
    }
}