using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataDevolucao { get; set; }

        public bool Concluido { get; set; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;

            Data = DateTime.Now;
            DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo);
            Concluido = false;
        }
    }
}