using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

using System.Collections;
using System.Linq.Expressions;


namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    class TelaReserva : TelaBase
    {
        public TelaReserva telaReserva = new TelaReserva();
        public RepositorioReserva repositorioReserva = new RepositorioReserva(); public Reserva reservaValida;
        public Reserva validadeReserva;

        
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Reservas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",
                "Id", "Reserva Válida", "Status", "Validade Reserva" 
            );
            
            ArrayList reservaCadastradas = repositorio.SelecionarTodos();

            foreach (Reserva reserva in reservaCadastradas)
            {
                if (reserva == null)
                    continue;

                Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",

                    reserva.Id, reserva.ReservaValida, reserva.Status, reserva.ValidadeReserva
                    ) ;
            }

            Console.ReadLine();
            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            telaReserva.VisualizarRegistros(false);

            Console.Write("Digite o ID da Reserva: ");
            int idReserva = int.Parse(Console.ReadLine());

            Reserva reservaCadastrada = (Reserva)repositorioReserva.SelecionarPorId(idReserva);


            Console.Write("Digite a data do emprestimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());
            
            return new Reserva (reservaCadastrada, dataEmprestimo);
        }
        
        public void CadastrarEntidadeTeste()
        {
            Reserva reservaTeste = (Reserva)repositorioReserva.SelecionarTodos()[0];
            DateTime dataTeste = DateTime.Now;

            Reserva reserva = new Reserva(reservaTeste, dataTeste);

             repositorio.Cadastrar (reserva);
        }
    }
}