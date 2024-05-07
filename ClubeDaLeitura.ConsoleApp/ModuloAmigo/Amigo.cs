using ClubeDaLeitura.ConsoleApp.ModuloMulta;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public string Nome { get; set; }

        public string NomeResponsavel
        {
            get => default;
            set
            {
            }
        }

        public string Telefone
        {
            get => default;
            set
            {
            }
        }

        public string Endereco
        {
            get => default;
            set
            {
            }
        }

        public Multa[] HistoricoMultas
        {
            get => default;
            set
            {
            }
        }

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
            Endereco = endereco;
        }


        public void Validar()
        {

        }
    }
}