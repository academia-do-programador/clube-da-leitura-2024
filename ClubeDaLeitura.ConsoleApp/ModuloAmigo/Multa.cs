namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Multa
    {
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public bool EstaPaga { get; set; }

        public Multa(decimal valor)
        {
            Valor = valor;

            Data = DateTime.Now;
            EstaPaga = false;
        }

        public void Pagar()
        {
            EstaPaga = true;
        }
    }
}