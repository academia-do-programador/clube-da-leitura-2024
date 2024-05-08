using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        public Revista[] Revistas { get; set; }

        public string Etiqueta { get; set; }
        public string CorDaCaixa { get; set; }
        public string DiasdeEmprestimo { get; set; }
        public string Revista { get; set; }



        public Caixa(string etiqueta, string corCaixa, string diasdeEmprestimo, string revista)
        {
            Etiqueta = etiqueta;
            CorDaCaixa = corCaixa;
            DiasdeEmprestimo = diasdeEmprestimo;
            Revista = revista;
        }



         public override void Validar()
         {
            throw new NotImplementedException();

        }

        public override void AtualizarRegistro(EntidadeBase novoegistro)
        {
            throw new NotImplementedException();
        }
    }
}

