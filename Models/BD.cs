using Microsoft.Data.SqlClient;
using Dapper;

public class BD{
    private static string _connectionString = @"Server=LocalHost;Database=TP08;Integrated Security=True;TrustServerCertificate=True;";

    public static List<Categoria> ObtenerCategorias()
{
    List<Categoria> categorias = new List<Categoria>();
    using(SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Categoria";
        categorias = connection.Query<Categoria>(query).ToList();
    }
    return categorias;
}

public static List<Dificultad> ObtenerDificultades()
{
    List<Dificultad> dificultades = new List<Dificultad>();
    using(SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Dificultad";
        dificultades = connection.Query<Dificultad>(query).ToList();
    }
    return dificultades;
}
public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
{
    List<Preguntas> preguntas = new List<Preguntas>();
    using(SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        string query = @"
            SELECT * FROM Preguntas
            WHERE (@TDificultad = -1 OR IDDificultad = @TDificultad)
              AND (@TCategoria = -1 OR IDCategoria = @TCategoria)";
        preguntas = connection.Query<Preguntas>(query, new { TDificultad = dificultad, TCategoria = categoria }).ToList();
    }
    return preguntas;
}

public static List<Respuestas> ObtenerRespuestas(int idPregunta)
{
    List<Respuestas> respuestas = new List<Respuestas>();
    using(SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Respuestas WHERE IDPregunta = @TIdPregunta";
        respuestas = connection.Query<Respuestas>(query, new { TIdPregunta = idPregunta }).ToList();
    }
    return respuestas;
}
}