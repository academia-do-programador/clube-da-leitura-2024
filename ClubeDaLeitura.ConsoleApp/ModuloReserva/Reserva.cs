using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva
    {
        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public DateTime DataAbertura { get; set; }

        public bool Expirada { get; set; }

        public Reserva(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;

            DataAbertura = DateTime.Now;
            Expirada = false;
        }
    }
}