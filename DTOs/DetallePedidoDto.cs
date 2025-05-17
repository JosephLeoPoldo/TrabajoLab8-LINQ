namespace Lab8_BryanCama.DTOs
{
    public class DetallePedidoDto
    {
        public int OrderId { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}