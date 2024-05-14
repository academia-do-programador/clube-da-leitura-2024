using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaAmigo : TelaBase<Amigo>, ITelaCadastravel
    {
        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Excluir {tipoEntidade}");
            Console.WriteLine($"4 - Visualizar {tipoEntidade}s");
            Console.WriteLine($"5 - Pagar Multas");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }

        public void PagarMulta()
        {
            ApresentarCabecalho();

            Console.WriteLine("Pagamento de Multas...");

            VisualizarAmigosComMulta();

            Console.Write("Digite o ID do amigo que deseja pagar as multas: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigo = repositorio.SelecionarPorId(idAmigo);

            Console.WriteLine($"Você deseja pagar o valor total de: R$ {amigo.ValorMulta}?");
            Console.WriteLine("1 - Pagar");
            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcao = Console.ReadLine()[0];

            if (opcao == 'S' || opcao == 's')
                return;

            amigo.PagarMultas();

            ExibirMensagem($"Multas com o valor de R$ {amigo.ValorMulta} pagas com sucesso!", ConsoleColor.Green);
        }

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

            List<Amigo> amigosCadastrados = repositorio.SelecionarTodos();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -25}",
                    amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco
                );
            }

            Console.ReadLine();
        }

        private void VisualizarAmigosComMulta()
        {
            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -25}",
                "Id", "Nome", "Responsável", "Telefone", "Multa Acumulada"
            );

            ArrayList amigosCadastrados = ((RepositorioAmigo)repositorio).SelecionarAmigosComMulta();

            foreach (Amigo amigo in amigosCadastrados)
            {
                if (amigo == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -25} | {5, -15}",
                    amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco, amigo.ValorMulta
                );
            }

            Console.ReadLine();
        }

        protected override Amigo ObterRegistro()
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