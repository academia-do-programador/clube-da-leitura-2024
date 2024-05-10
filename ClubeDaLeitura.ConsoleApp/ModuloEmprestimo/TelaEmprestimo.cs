using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;


namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    class TelaEmprestimo : TelaBase
    {
        public TelaAmigo telaAmigo = null;
        public RepositorioAmigo repositorioAmigo = null;

        public TelaRevista telaRevista = null;
        public RepositorioRevista repositorioRevista = null;


        public virtual char ApresentarMenu()
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
            Console.WriteLine($"5 - Visualizar Emprestimos do Mês");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }


        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Emprestimos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",
                "Id", "Nome do Amigo", "Data Do Emprestimo", "Status"
            );

            ArrayList emprestimosCadastrados = repositorio.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                if (emprestimo == null)
                    continue;

                Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",

                    emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.DataEmprestimo, emprestimo.Status
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        protected override EntidadeBase ObterRegistro()
        {
            telaAmigo.VisualizarRegistros(false);

            Console.Write("Digite o ID do Amigo requisitante: ");
            int idAmigo = int.Parse(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);


            telaRevista.VisualizarRegistros(false);

            Console.Write("Digite o ID da Revista Requisitada: ");
            int idRevista = int.Parse(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);


            Console.Write("Digite a data do emprestimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());




            return new Emprestimo(amigoSelecionado,revistaSelecionada, dataEmprestimo);
        }



        public void CadastrarEntidadeTeste()
        {
            Amigo amigoTeste = (Amigo)repositorioAmigo.SelecionarTodos()[0];
            
            Revista revistaTeste = (Revista)repositorioRevista.SelecionarTodos()[0];
            
            
            DateTime dataTeste = DateTime.Now;

            Emprestimo emprestimo = new Emprestimo(amigoTeste, revistaTeste, dataTeste);

            repositorio.Cadastrar(emprestimo);
        }


        public override void OperacaoAdicionada()
        {
            Console.WriteLine("Visualizando os Emprestimos do Mês...");
            Console.WriteLine();

        }
    }
}
