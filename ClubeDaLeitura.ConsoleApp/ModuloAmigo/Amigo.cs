using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public string Nome { get; set; }

        public string NomeResponsavel { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public ArrayList HistoricoMultas { get; set; } = new ArrayList();

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