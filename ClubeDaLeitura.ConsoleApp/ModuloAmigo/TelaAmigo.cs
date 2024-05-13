using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaAmigo : TelaBase
    {
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Amigos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -25}",
                "Id", "Nome", "Responsável", "Telefone", "Endereço"
            );

            ArrayList amigosCadastrados = repositorio.SelecionarTodos();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -25}",
                    amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco
                );
            }

            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o endereço: ");
            string endereço = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, endereço);
        }

        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = new Amigo("Juninho", "Sérgio Borracheiro", "49 98225-5151", "Rua Jânio Quadros");

            repositorio.Cadastrar(amigo);
        }
    }
}
