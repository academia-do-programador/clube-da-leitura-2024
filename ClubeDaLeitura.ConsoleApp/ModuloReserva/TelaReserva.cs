using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using Microsoft.Win32;
namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class TelaReserva : TelaBase <Reserva>, ITelaCRUD
    {
        TelaAmigo telaAmigo;
        TelaRevista telaRevista;
        TelaEmprestimo telaEmprestimo;
        public TelaReserva(RepositorioReserva repositorio, TelaAmigo telaAmigo, TelaRevista telaRevista, TelaEmprestimo telaEmprestimo, string tipoEntidade)
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

            if (telaAmigo.repositorio.ExistemItensCadastrados() || telaRevista.repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref repetir); return; }
            if (NaoHaRevistasDisponiveis()) return;
            if (NaoHaAmigosDisponiveis()) return;

            Reserva entidade = ObterRegistro(repositorio.CadastrandoID());
            RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
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
        public override void Excluir(ref bool retornar)
        {
            while (true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"\nExcluindo {tipoEntidade}...\n");
                VisualizarRegistros(false);

                int idRegistroEscolhido = RecebeInt($"\nDigite o ID do {tipoEntidade} que deseja excluir: ");

                if (!repositorio.Existe(idRegistroEscolhido)) IdInvalido();
                else
                {
                    LiberaRevista(idRegistroEscolhido);
                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido), "excluído");
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


        protected override Reserva ObterRegistro(int id)
        {
            int idSelecionado = 0;
            DateTime dataSelecionada = DateTime.Now;
            Amigo amigoSelecionado = new Amigo("-", "-", "-", "-");
            Revista revistaSelecionada = new Revista("-", "-", "-", null);

            Reserva novaReserva = new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);

            }
            while (!IdEhValido(idSelecionado, telaAmigo, ref amigoSelecionado, 
            () => amigoSelecionado = new Amigo("-", "-", "-", "-")) || AmigoTemMulta(amigoSelecionado));

            do
            {
                TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);
            }
            while (!IdEhValido(idSelecionado, telaRevista, ref revistaSelecionada,
                    () => revistaSelecionada = new Revista("-", "-", "-", null)) || RevistaIndisponivel(revistaSelecionada));


            RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                ref novaReserva, ref dataSelecionada,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d")));

            novaReserva = new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, DateTime.Now < dataSelecionada.Add(new TimeSpan(2, 0, 0, 0)));

            BloqueiaRevista(novaReserva);

            return novaReserva;
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

        #region Auxiliares
        private bool AmigoTemMulta(Amigo amigo)
        {
            if (amigo.multa)
            {
                ExibirMensagem("Este amigo possui multas em aberto. Não é possível emprestar ", ConsoleColor.Red);
                Console.ReadKey(true);
            }
            return amigo.multa;
        }
        private bool RevistaIndisponivel(Revista revista)
        {
            if (revista.indiponivel)
            {
                ExibirMensagem("Essa revista já está reservada ou emprestada, selecione uma revista válida", ConsoleColor.DarkYellow);
                Console.ReadKey(true);
            }
            return revista.indiponivel;
        }
        private bool NaoHaRevistasDisponiveis()
        {
            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
                 if (!revista.indiponivel) return false;

            ExibirMensagem("Não há revistas disponíveis :(", ConsoleColor.Red);
            Console.ReadKey(true);
            return true;
        }
        private bool NaoHaAmigosDisponiveis()
        {
            foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                if (!amigo.multa) return false;

            ExibirMensagem("Todos os amigos tem multa :(", ConsoleColor.Red);
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
        private void BloqueiaRevista(Reserva novaReserva)
        {
            if (novaReserva.Status)
                foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
                    if (revista == novaReserva.Revista)
                    {
                        revista.indiponivel = true;
                        telaRevista.repositorio.Editar(revista.Id, revista);
                    }
        }
        private void LiberaRevista(int idRegistroEscolhido)
        {
            Reserva reserva = repositorio.SelecionarPorId(idRegistroEscolhido);

            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
            {
                if (revista == reserva.Revista) revista.indiponivel = false;
                telaRevista.repositorio.Editar(revista.Id, revista);
            }
        }
        #endregion
    }
}
