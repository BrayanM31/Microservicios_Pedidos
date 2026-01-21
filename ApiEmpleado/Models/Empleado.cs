namespace ApiEmpleado.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public required string Cedula { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
    }
}