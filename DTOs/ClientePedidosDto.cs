namespace Lab8_BryanCama.DTOs
{
    public class ClientePedidosDto
    {
        public string NombreCliente { get; set; } = string.Empty;
        public List<string> Productos { get; set; } = new();
    }
}