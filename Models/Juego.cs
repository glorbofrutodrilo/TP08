public class Juego
{
    public static string username;

    public static int PuntajeActual;

    public static int CantidadPreguntasCorrectas;

    public static int ContadorNroPreguntaActual;

    public static Preguntas PreguntaActual;

    public static List<Preguntas> ListaPreguntas;

    public static List<Respuestas> ListaRespuestas;



    public void IniciarJuego()
    {
        username = "";
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null;
        ListaRespuestas = null;

    }
    public List<Categoria> ObtenerCategorias()
    {
        List<Categoria> Categorias = BD.ObtenerCategorias();
        return Categorias;
    }

    public List<Dificultad> ObtenerDificultades()
    {
        Lista<Dificultad> Dificultades = BD.ObtenerDificultades();
        return Dificultades;
    }

    public void CargarPartida(string username, int dificultad, int categoria)
    {
        List<Preguntas> preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        IniciarJuego();
        this.username = username;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = preguntas;
        ListaRespuestas = null;
    }

    public Preguntas ObtenerProximaPregunta()
    {
        if (ContadorNroPreguntaActual <= ListaPreguntas.Count())
        {
            return ListaPreguntas[ContadorNroPreguntaActual];
            ContadorNroPreguntaActual++;
        }
        else
            return null;
    }

    public List<Preguntas> ObtenerProximasRespuestas(int idPregunta)
    {
        List<Respuestas>RespuestasXIdPregunta = BD.ObtenerRespuestas(idPregunta);
        return RespuestasXIdPregunta;
    }
}

