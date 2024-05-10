using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class TelaReserva : TelaBase
    {
        TelaBase telaAmigo;
        TelaBase telaRevista;
        TelaBase telaEmprestimo;
        public TelaReserva(RepositorioBase repositorio, TelaBase telaAmigo, TelaBase telaRevista, TelaBase telaEmprestimo, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
            this.telaEmprestimo = telaEmprestimo;
            this.tipoEntidade = tipoEntidade;
        }
        public override void ApresentarMenu(ref bool sair)
        {
            bool retornar = true;
            while (retornar)
            {
                ApresentarCabecalhoEntidade("");

                Console.WriteLine("1 - Reservar revista");
                Console.WriteLine("2 - Editar reservas");
                Console.WriteLine("3 - Excluir reserva");
                Console.WriteLine("4 - Visualizar reservas");
                Console.WriteLine("5 - Realizar empréstimo");
                Console.WriteLine("R - Retornar");
                Console.WriteLine("S - Sair");

                string operacaoEscolhida = RecebeString("\nEscolha uma das opções: ");
                retornar = false;

                switch (operacaoEscolhida)
                {
                    case "1": Registrar(); break;
                    case "2": Editar(ref retornar); break;
                    case "3": Excluir(ref retornar); break;
                    case "4": VisualizarRegistros(true); break;
                    case "5": RealizarEmprestimo(); break;
                    case "R": break;
                    case "S": sair = true; break;
                    default: OpcaoInvalida(ref retornar); break;
                }
            }
        }

        public override void Registrar()
        {
            bool repetir = false;
            ApresentarCabecalhoEntidade($"Cadastrando {tipoEntidade}...\n");

            if (NaoHaRevistasDisponiveis()) return;
            if (telaAmigo.repositorio.ExistemItensCadastrados() || telaRevista.repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref repetir); return; }

            Reserva entidade = (Reserva)ObterRegistro(repositorio.CadastrandoID());
            RealizaAcao(() => repositorio.Cadastrar(entidade, telaRevista), "cadastrado");
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando reservas...\n");

            Console.WriteLine(
                "{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "Status", "DataReserva");

            foreach (Reserva reserva in repositorio.SelecionarTodos())
            {
                string statusReserva = "";

                if (reserva.Status)
                    statusReserva = "Aberto";
                else
                    statusReserva = "Expirado";

                string[] parametros = [reserva.Id.ToString(), reserva.Amigo.Nome, reserva.Revista.Titulo, statusReserva.ToString(), reserva.DataReserva.Add(new TimeSpan(2, 0, 0, 0)).ToString("d")];

                AjustaTamanhoDeVisualizacao(parametros);

                Console.Write("{0, -5} | {1, -15} | {2, -15} |",
                    parametros[0], parametros[1], parametros[2]);

                if (reserva.Status)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                    Console.BackgroundColor = ConsoleColor.DarkRed;

                Console.Write(" {0, -15} ", parametros[3]);

                Console.ResetColor();

                Console.WriteLine("| {0, -15} ", parametros[4]);
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }
        public virtual void Excluir(ref bool retornar)
        {
            while (true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"\nExcluindo {tipoEntidade}...\n");
                VisualizarRegistros(false);

                int idRegistroEscolhido = RecebeInt($"\nDigite o ID do {tipoEntidade} que deseja excluir: ");

                if (!repositorio.Existe(idRegistroEscolhido, this)) IdInvalido();
                else
                {
                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido, telaRevista, 0), "excluído");
                    break;
                }
            }
        }
        private void RealizarEmprestimo()
        {
            bool repetir = false;
            if (TodasReservasExpiradas()) return;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref repetir); return; }

            int idReservaSelecionada;
            Reserva reservaSelecionada;

            do
            {
                ApresentarCabecalhoEntidade($"Realizando empréstimo a partir de reserva...\n");

                VisualizarRegistros(false);

                idReservaSelecionada = RecebeInt("\nInsira o ID da reserva com a qual deseja prosseguir: ");

                reservaSelecionada = (Reserva)repositorio.SelecionarPorId(idReservaSelecionada);
                if (!reservaSelecionada.Status)
                {
                    ExibirMensagem("A reserva selecionada está expirada, por favor, selecione uma reserva em aberto.", ConsoleColor.DarkYellow);
                    Console.ReadKey(true);
                }

            } while (!IdEhValido(idReservaSelecionada) || !reservaSelecionada.Status);

            Revista revista = (Revista)reservaSelecionada.Revista;
            Caixa caixa = (Caixa)revista.Caixa;
            TimeSpan diasParaDevolver = new TimeSpan(caixa.DiasDeEmprestimo, 0, 0, 0);

            Emprestimo novoEmprestimo = new Emprestimo(reservaSelecionada.Amigo, reservaSelecionada.Revista, DateTime.Now, DateTime.Now.Add(diasParaDevolver));

            telaEmprestimo.repositorio.Cadastrar(novoEmprestimo);

            repositorio.Excluir(idReservaSelecionada);

            ExibirMensagem("Emprestimo de item indiponivel concluído!", ConsoleColor.Green);
            Console.ReadKey(true);
        }


        protected override EntidadeBase ObterRegistro(int id)
        {
            int idSelecionado = 0;
            DateTime dataSelecionada = DateTime.Now;
            EntidadeBase amigoSelecionado = new Amigo("-", "-", "-", "-");
            EntidadeBase revistaSelecionada = new Revista("-", "-", "-", null);

            EntidadeBase novaReserva = new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);

            }
            while (!IdEhValido(idSelecionado, telaAmigo, ref amigoSelecionado,
                    () => amigoSelecionado = new Amigo("-", "-", "-", "-"))) ;

            do
            {
                TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);
            }
            while (!IdEhValido(idSelecionado, telaRevista, ref revistaSelecionada, 
                    () => revistaSelecionada = new Revista("-", "-", "-", null)) || revistaEstaReservada(idSelecionado));


            RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                ref novaReserva, ref dataSelecionada,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d")));

            return new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, DateTime.Now < dataSelecionada.Add(new TimeSpan(2, 0, 0, 0))); 
        }
        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando Reservas...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15}",
                "Id", "Amigo", "Reserva", "Data de Reserva");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3]);
        }

        private bool revistaEstaReservada(int idSelecionado)
        {
            foreach (Reserva reserva in repositorio.SelecionarTodos())
            {
                if (reserva.Revista.Id == idSelecionado)
                {
                    ExibirMensagem("Essa revista já está reservada, selecione uma revista válida", ConsoleColor.DarkYellow);
                    Console.ReadKey(true);
                    return true;
                }
            }
            return false;
        }
        private bool NaoHaRevistasDisponiveis()
        {
            if (repositorio.SelecionarTodos().Count == 0) return false;

            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
                foreach (Reserva reserva in repositorio.SelecionarTodos())
                    if (reserva.Revista != revista) return false;

            ExibirMensagem("Todos as revistas estão reservadas :(", ConsoleColor.Red);
            Console.ReadKey(true);
            return true;
        }
        private bool TodasReservasExpiradas()
        {
            foreach (Reserva reserva in repositorio.SelecionarTodos())
                if (reserva.Status) 
                    return false;


            ExibirMensagem("Todas as reservas estão expiradas, emprestimos só serão feitos a partir de reservas em aberto.", ConsoleColor.Red);
            Console.ReadKey(true);
            return true;
        }
    }
}
