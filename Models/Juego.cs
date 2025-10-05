public static class Juego
{
    public static string username;

    public  static int PuntajeActual;

    public  static int CantidadPreguntasCorrectas;

    public  static int ContadorNroPreguntaActual;

    public  static Preguntas PreguntaActual;

    public  static List<Preguntas> ListaPreguntas;

    public  static List<Respuestas> ListaRespuestas;



    public static void IniciarJuego()
    {
        username = "";
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null;
        ListaRespuestas = null;

    }
    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> Categorias = BD.ObtenerCategorias();
        return Categorias;
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        List<Dificultad> Dificultades = BD.ObtenerDificultades();
        return Dificultades;
    }

    public static void CargarPartida(string username, int dificultad, int categoria)
    {
        List<Preguntas> preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        IniciarJuego();
        Juego.username = username;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = preguntas[0];
        ListaPreguntas = preguntas;
        ListaRespuestas = BD.ObtenerRespuestas(PreguntaActual.IDPregunta);
    }

    public static Preguntas ObtenerProximaPregunta()
    {
        if (ContadorNroPreguntaActual < ListaPreguntas.Count())
        {
            PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            ContadorNroPreguntaActual++;
            return PreguntaActual;
        }
        else
            return null;
    }

    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
    {
        List<Respuestas> RespuestasXIdPregunta = BD.ObtenerRespuestas(idPregunta);
        return RespuestasXIdPregunta;
    }
    public static bool VerificarRespuesta(int idRespuesta)
    {
        bool correcto = false;

        foreach (Respuestas r in ListaRespuestas)
        {
            if (r.IDRespuesta == idRespuesta && r.IDPregunta == PreguntaActual.IDPregunta)
            {
                if (r.Correcta)
                {
                    PuntajeActual += 20;
                    CantidadPreguntasCorrectas++;
                    correcto = true;
                }
            }
        }

        return correcto;
    }
}

