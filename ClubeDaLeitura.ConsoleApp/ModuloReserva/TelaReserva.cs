using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class TelaReserva : TelaBase, ITelaCadastravel
    {
        public TelaRevista telaRevista = null;
        public RepositorioRevista repositorioRevista = null;

        public TelaAmigo telaAmigo = null;
        public RepositorioAmigo repositorioAmigo = null;

        public TelaEmprestimo telaEmprestimo = null;

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("           Gestão de Reservas           ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Cadastrar Nova Reserva");
            Console.WriteLine("2 - Empréstimo à partir de Reserva");
            Console.WriteLine("3 - Visualizar Reservas");

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            return operacaoEscolhida;
        }

        public override void Registrar()
        {
            ApresentarCabecalho();

            Console.WriteLine($"Cadastrando nova reserva...");

            Console.WriteLine();

            Reserva reserva = (Reserva)ObterRegistro();

            ArrayList erros = reserva.Validar();

            if (erros.Count > 0)
            {
                ApresentarErros(erros);
                return;
            }

            if (reserva.Revista.EstaEmprestada)
            {
                ExibirMensagem("Não é possível emprestar uma revista já emprestada.", ConsoleColor.Red);
                return;
            }

            if (reserva.Amigo.TemMulta)
            {
                ExibirMensagem("Não é possível emprestar para um amigo com multa pendente.", ConsoleColor.Red);
                return;
            }

            reserva.Iniciar();

            base.InserirRegistro(reserva);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            ApresentarCabecalho();

            Console.WriteLine("Visualizando Reservas...");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                "Id", "Revista", "Amigo", "Data de Abertura", "Status"
            );

            foreach (Reserva r in repositorio.SelecionarTodos())
            {
                if (r == null)
                    continue;

                string statusReserva = r.Expirada ? "Expirada" : "Em Aberto";

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                    r.Id, r.Revista.Titulo, r.Amigo.Nome, r.DataAbertura.ToShortDateString(), statusReserva
                );
            }

            Console.ReadLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            telaRevista.VisualizarRegistros(false);

            Console.Write("Digite o ID da revista que deseja reservar: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

            telaAmigo.VisualizarRegistros(false);

            Console.Write("Digite o ID do amigo requisitante: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

            return new Reserva(amigoSelecionado, revistaSelecionada);
        }

        public void AbrirEmprestimo()
        {
            ApresentarCabecalho();

            Console.WriteLine("Abrindo empréstimo à partir de reserva...");

            Console.WriteLine();

            VisualizarReservasEmAberto();

            Console.Write("\nDigite o ID da reserva que deseja converter em empréstimo: ");
            int idReserva = Convert.ToInt32(Console.ReadLine());

            Reserva reserva = (Reserva)repositorio.SelecionarPorId(idReserva);

            reserva.Concluir();

            telaEmprestimo.RegistrarEmprestimoDeReserva(reserva);
        }

        public void VisualizarReservasEmAberto()
        {
            Console.WriteLine();

            Console.WriteLine("Visualizando Reservas Em Aberto...");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                "Id", "Revista", "Amigo", "Data de Abertura", "Status"
            );

            ArrayList registros = ((RepositorioReserva)repositorio).SelecionarReservasEmAberto();

            foreach (Reserva r in registros)
            {
                if (r == null)
                    continue;

                string statusEmprestimo = r.Expirada ? "Expirada" : "Em Aberto";

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                    r.Id, r.Revista.Titulo, r.Amigo.Nome, r.DataAbertura.ToShortDateString(), statusEmprestimo
                );
            }

            Console.ReadLine();
        }
    }
}
