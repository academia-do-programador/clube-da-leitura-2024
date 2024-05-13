using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo : TelaBase
    {
        public TelaRevista telaRevista = null;
        public RepositorioRevista repositorioRevista = null;

        public TelaAmigo telaAmigo = null;
        public RepositorioAmigo repositorioAmigo = null;

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar Novo Empréstimo");
            Console.WriteLine($"2 - Devolução de Empréstimo");
            Console.WriteLine($"3 - Visualizar Empréstimos");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }

        public override void Registrar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando {tipoEntidade}...");

            Console.WriteLine();

            Emprestimo entidade = (Emprestimo)ObterRegistro();

            ArrayList erros = entidade.Validar();

            if (erros.Count > 0)
            {
                ApresentarErros(erros);
                return;
            }

            entidade.Iniciar();

            base.InserirRegistro(entidade);
        }

        public void Concluir()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Conclusão de {tipoEntidade}...");

            Console.WriteLine();

            VisualizarEmprestimosEmAberto();

            Console.Write("\nDigite o ID do empréstimo que deseja concluir: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            Emprestimo emprestimo = (Emprestimo)repositorio.SelecionarPorId(idEmprestimo);

            emprestimo.Concluir();

            ExibirMensagem($"O empréstimo foi concluído com sucesso!", ConsoleColor.Green);
        }

        public void RegistrarEmprestimoDeReserva(Reserva reserva)
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando {tipoEntidade}...");

            Console.WriteLine();

            Emprestimo entidade = new Emprestimo(reserva.Amigo, reserva.Revista);

            entidade.Iniciar();

            base.InserirRegistro(entidade);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            ApresentarCabecalho();

            Console.WriteLine("Visualizando Empréstimos...");

            Console.WriteLine();

            Console.WriteLine("1 - Visualizar Empréstimos do Mês");
            Console.WriteLine("2 - Visualizar Empréstimos Em Aberto do Dia");
            Console.WriteLine("3 - Visualizar Todos os Empréstimos");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            ArrayList emprestimos;

            if (opcaoEscolhida == '1')
                emprestimos = ((RepositorioEmprestimo)repositorio).SelecionarEmprestimosDoMes();

            else if (opcaoEscolhida == '2')
                emprestimos = ((RepositorioEmprestimo)repositorio).SelecionarEmprestimosDoMes();

            else
                emprestimos = repositorio.SelecionarTodos();

            VisualizarEmprestimos(emprestimos);

            Console.ReadLine();
        }

        private void VisualizarEmprestimos(ArrayList emprestimos)
        {
            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -10} | {4, -20} | {5, -20}",
                "Id", "Revista", "Amigo", "Data", "Data de Devolução", "Status"
            );

            foreach (Emprestimo e in emprestimos)
            {
                if (e == null)
                    continue;

                string statusEmprestimo = e.Concluido ? "Concluído" : "Em Aberto";

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -15} | {3, -10} | {4, -20} | {5, -20}",
                    e.Id, e.Revista.Titulo, e.Amigo.Nome, e.Data.ToShortDateString(), e.DataDevolucao.ToShortDateString(), statusEmprestimo
                );
            }

            Console.WriteLine();
        }

        private void VisualizarEmprestimosEmAberto()
        {
            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -10} | {4, -20} | {5, -20}",
                "Id", "Revista", "Amigo", "Data", "Data de Devolução", "Status"
            );

            ArrayList registros = ((RepositorioEmprestimo)repositorio).SelecionarEmprestimosEmAberto();

            foreach (Emprestimo e in registros)
            {
                if (e == null)
                    continue;

                string statusEmprestimo = e.Concluido ? "Concluído" : "Em Aberto";

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -15} | {3, -10} | {4, -20} | {5, -20}",
                    e.Id, e.Revista.Titulo, e.Amigo.Nome, e.Data.ToShortDateString(), e.DataDevolucao.ToShortDateString(), statusEmprestimo
                );
            }

            Console.ReadLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            telaRevista.VisualizarRegistros(false);

            Console.Write("Digite o ID da revista que deseja requisitar: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

            telaAmigo.VisualizarRegistros(false);

            Console.Write("Digite o ID do amigo requisitante: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

            return new Emprestimo(amigoSelecionado, revistaSelecionada);
        }
    }
}
