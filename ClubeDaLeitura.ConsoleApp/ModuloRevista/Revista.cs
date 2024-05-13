using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class Revista
    {
        public string Titulo { get; set; }

        public int NumeroEdicao { get; set; }

        public int Ano { get; set; }

        public Caixa Caixa { get; set; }

        public Revista(string titulo, int numeroEdicao, int ano, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            Ano = ano;
            Caixa = caixa;
        }
    }
}