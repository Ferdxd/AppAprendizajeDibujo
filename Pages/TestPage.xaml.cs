using AppDibujoArtistico.BaseDatos;
using AppDibujoArtistico.Correo;

namespace AppDibujoArtistico.Pages;

public partial class TestPage : ContentPage
{
    //public List<string> preguntas= new List<string>();
    //Dictionary<string, List<string>> preguntasyrespuestas= new Dictionary<string, List<string>>();
    Dictionary<string, List<Tuple<string, bool>>> preguntasYRespuestas;
    bool respuesta1;
    bool respuesta2;
    bool respuesta3;
    int currentIndex = 0;
    int nota = 0;
    int notaP = 0;
    int notaI = 0;
    int notaA = 0;
    string correo;
    int id_alumno;
    Cls_Evaluaciones evaluaciones = new Cls_Evaluaciones();
    public TestPage()
	{
		InitializeComponent();
        
        try
        {
            
            preguntasYRespuestas = evaluaciones.cargarPreguntasYRespuestas(LoginPage.examen);
            lblExamen.Text = LoginPage.examen.ToString();
            mostrarPreguntasyRespuestas();
        }catch(Exception ex) {
            int error4 = 0;
        }
    }
    public void mostrarPreguntasyRespuestas()
    {
       
      
        var pregunta = preguntasYRespuestas.Keys.ElementAt(currentIndex);
        lblPregunta.Text = pregunta.ToString();

        var respuestas = preguntasYRespuestas[pregunta];
        btnOpcionA.Text = respuestas[0].Item1;
        btnOpcionB.Text = respuestas[1].Item1;
        btnOpcionC.Text = respuestas[2].Item1;
        respuesta1 = respuestas[0].Item2;
        respuesta2 = respuestas[1].Item2;
        respuesta3 = respuestas[2].Item2;
    }
    public async void seleccion_Respuesta(bool resp)
    {
        currentIndex++;
        mostrarPreguntasyRespuestas();
        
        ClsCorreoPDF certificado= new ClsCorreoPDF();
        // Color colorPersonalizado = Color.FromHex("#FF0000");

        if (resp)
        {
            // colorPersonalizado = Color.FromHex("81F37A");
            //btnOpcionA.BackgroundColor = colorPersonalizado;

            if (LoginPage.examen == "Test_Ubicacion")
            {
                nota += 1;
            }
            if (LoginPage.examen == "Principiante")
            {
                notaP += 1;
            }
            if (LoginPage.examen == "Intermedio")
            {
                notaI += 1;
            }
            if (LoginPage.examen == "Avanzado")
            {
                notaA += 1;
            }
        }
        else
        {

            //btnOpcionA.BackgroundColor = colorPersonalizado;
        }
        if (currentIndex >= 9 && LoginPage.examen == "Test_Ubicacion")
        {
            await DisplayAlert("Tu nota final es:", "" + nota, "Ok");
            currentIndex = 0;
            try
            {
                evaluaciones.insertarNotaUbi(LoginPage.id_alumno, nota);
            }
            catch
            {
                evaluaciones.actualizarNotaExamen(LoginPage.id_alumno,nota,"tb_puntuaciones_ubicacion");
            }
            finally
            {
                await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
            }
            
            
        }
        if (currentIndex >= 9 && LoginPage.examen == "Principiante")
        {
            await DisplayAlert("Tu nota final es:", "" + notaP +" \nSi tu punteo es menor a 8 necesitas realizar nuevamente el test, caso contrario puedes acceder al siguiente nivel", "Ok");
            currentIndex = 0;
            try
            {
                evaluaciones.insertarNotaExamen(LoginPage.id_alumno, notaP, "tb_puntuaciones_test1");
            }
            catch
            {
                if (notaP < 8)
                {
                    evaluaciones.actualizarNotaExamen(LoginPage.id_alumno, notaP, "tb_puntuaciones_test1");
                }
                else
                {
                    await DisplayAlert("Curso Aprobado", "Usted ya aprobo el curso por lo cual ya cuenta con una nota por lo tanto no se le dara validez a los resultados que acaba de obtener", "Ok");
                }
               
            }
            finally
            {
                await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
            }
            
            
        }
        if (currentIndex >= 9 && LoginPage.examen == "Intermedio")
        {
            await DisplayAlert("Tu nota final es:", "" + notaI + " \nSi tu punteo es menor a 8 necesitas realizar nuevamente el test, caso contrario puedes acceder al siguiente nivel\"", "Ok");
            currentIndex = 0;
            try
            {
                evaluaciones.insertarNotaExamen(LoginPage.id_alumno, notaI, "tb_puntuaciones_test2");
            }
            catch
            {
                if(notaI < 8)
                {
                    evaluaciones.actualizarNotaExamen(LoginPage.id_alumno, notaI, "tb_puntuaciones_test2");
                }
                else
                {
                    await DisplayAlert("Curso Aprobado", "Usted ya aprobo el curso por lo cual ya cuenta con una nota por lo tanto no se le dara validez a los resultados que acaba de obtener", "Ok");
                }
                
            }
            finally
            {
                await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
            }
            
           
        }
        if (currentIndex >= 9 && LoginPage.examen == "Avanzado")
        {
            if (notaA>=8)
            {
                await DisplayAlert("Tu nota final es:", "" + notaA + "\nFelicidades por completar el ultimo nivel, debido a tu logro se te otorgo un certificado puedes ir a tu correo a verificarlo", "Ok");
                certificado.enviarPDF(LoginPage.Correo, LoginPage.nombre_Completo);
            }
            else
            {
                await DisplayAlert("Tu nota final es:", "" + notaA + "\nTu nota es muy baja, repasa nuevamente y evaluate cuando te sientas preparado", "Ok");
                
            }
         
            currentIndex = 0;
            try
            {
                evaluaciones.insertarNotaExamen(LoginPage.id_alumno, notaA, "tb_puntuaciones_test3");
            }
            catch
            {
                
                if(notaA<8)
                {
                    evaluaciones.actualizarNotaExamen(LoginPage.id_alumno, notaA, "tb_puntuaciones_test3");

                }
                else
                {
                    await DisplayAlert("Curso Aprobado", "Usted ya aprobo el curso por lo cual ya cuenta con una nota por lo tanto no se le dara validez a los resultados que acaba de obtener", "Ok");
                }
                
            }
            
            await Navigation.PushModalAsync(new Pages.ConocimientosPrototipo());
        }

    }
    
    private async void btnOpcionA_Clicked(object sender, EventArgs e)
    {

        seleccion_Respuesta(respuesta1);
    }



    private async void btnOpcionB_Clicked(object sender, EventArgs e)
    {
        seleccion_Respuesta(respuesta2);
    }

    private async void btnOpcionC_Clicked(object sender, EventArgs e)
    {
        seleccion_Respuesta(respuesta3);
    }
}