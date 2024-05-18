namespace Examen.API.utilities
{
    public class Response<T>
    {
        public bool status { get; set; }    

        public T value { get; set; }

        public string? message { get; set; }
    }

    public class ProductRequest
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public IFormFile? Image { get; set; } // Archivo de imagen
    }

}
