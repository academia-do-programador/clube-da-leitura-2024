using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;


namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    class TelaEmprestimo : TelaBase
    {
        public TelaAmigo telaAmigo = null;
        public RepositorioAmigo repositorioAmigo = null;


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


            //Console.Write("Digite o ID da Revista Requisitada: ");
            //int idRevista = int.Parse(Console.ReadLine());


            Console.Write("Digite a data do emprestimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());




            return new Emprestimo(amigoSelecionado, dataEmprestimo);
        }



        public void CadastrarEntidadeTeste()
        {
            Amigo amigoTeste = (Amigo)repositorioAmigo.SelecionarTodos()[0];
            DateTime dataTeste = DateTime.Now;

            Emprestimo emprestimo = new Emprestimo(amigoTeste, dataTeste);

            repositorio.Cadastrar(emprestimo);
        }
    }
}
