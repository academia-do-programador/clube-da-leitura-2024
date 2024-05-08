using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    class TelaAmigo : TelaBase
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
                "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                "Id", "Nome", "Nome Responsável", "Telefone", "Endereço"
            );

            ArrayList amigosCadastrados = repositorio.SelecionarTodos();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
                    amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }


        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Nome do Responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o Endereço: ");
            string endereco = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, endereco);
        }



        public void CadastrarEntidadeTeste()
        {
            Amigo amigo = new Amigo("João", "Rech", "09009090", "Teste 1");

            repositorio.Cadastrar(amigo);
        }
    }
}
