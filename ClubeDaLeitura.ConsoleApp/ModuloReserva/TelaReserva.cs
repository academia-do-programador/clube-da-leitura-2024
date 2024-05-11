using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

using System.Collections;


namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    class TelaReserva : TelaBase
    {
        public TelaAmigo telaAmigo = null;
        public TelaRevista telaRevista = null;
        public TelaEmprestimo telaEmprestimo = null;

        public RepositorioAmigo repositorioAmigo = null;
        public RepositorioRevista repositorioRevista = null;


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
            Console.WriteLine($"5 - Realizar Emprestimo");

            Console.WriteLine();

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            if (operacaoEscolhida == '5')
            {
                RealizarEmprestimo();
                return 'S';
            }

            return operacaoEscolhida;
        }



        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Reservas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -10} |",
                "Id", "Nome do Amigo", "Nome da Revista", "Data da Reserva", "Status" 
            );
            
            ArrayList reservaCadastradas = repositorio.SelecionarTodos();

            foreach (Reserva reserva in reservaCadastradas)
            {
                if (reserva == null)
                    continue;

                Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -20} | {4, -10} |",
                 reserva.Id, reserva.Amigo.Nome, reserva.Revista.Nome, reserva.DataReserva, reserva.Status
                 ) ;
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        protected override EntidadeBase ObterRegistro()
        {
            telaAmigo.VisualizarRegistros(false);

            Console.Write("Digite o ID do Amigo solicitante: ");
            int idAmigo = int.Parse(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);



            telaRevista.VisualizarRegistros(false);

            Console.Write("Digite o ID da Revista Solicitada: ");
            int idRevista = int.Parse(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);



            Console.Write("Digite a data da reserva: ");
            DateTime dataReserva = Convert.ToDateTime(Console.ReadLine());
            
            return new Reserva (amigoSelecionado, revistaSelecionada, dataReserva);
        }
        


        public void CadastrarEntidadeTeste()
        {
            Amigo amigoTeste = (Amigo)repositorioAmigo.SelecionarTodos()[0];

            Revista revistaTeste = (Revista)repositorioRevista.SelecionarTodos()[0];

            DateTime dataTeste = DateTime.Now;

            Reserva reserva = new Reserva(amigoTeste, revistaTeste, dataTeste);

             repositorio.Cadastrar (reserva);
        }


        private void RealizarEmprestimo()
        {
            telaEmprestimo.Registrar();
        }
    }
}