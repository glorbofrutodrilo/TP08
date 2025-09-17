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

    public  List<Categoria> ObtenerCategoria()
    {
        List<Categoria>Categorias= BD.ObtenerCategorias();
        return Categorias;
    }
    public List<Dificultad> ObtenerDificultad(){
        List<Dificultad>Dificultades = BD.ObtenerDificultades();
        return Dificultades;
    }
   
}
