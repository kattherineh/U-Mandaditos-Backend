namespace Aplication.DTOs.General
{
    /// <summary>
    /// Clase genérica para estructurar las respuestas.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
