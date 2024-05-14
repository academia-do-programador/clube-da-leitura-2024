using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using Microsoft.Win32;
namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase <Emprestimo>, ITelaCRUD
    {
        TelaAmigo telaAmigo;
        TelaRevista telaRevista;
        TelaMulta telaMulta;
        public TelaEmprestimo(RepositorioEmprestimo repositorio, TelaAmigo telaAmigo, TelaRevista telaRevista, TelaMulta telaMulta, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
            this.telaMulta = telaMulta;
            this.tipoEntidade = tipoEntidade;
        }

        public override void ApresentarMenu(ref bool sair)
        {
            bool retornar = true;
            while (retornar)
            {
                ApresentarCabecalhoEntidade("");

                Console.WriteLine($"1 - Emprestar");
                Console.WriteLine($"2 - Editar empréstimo");
                Console.WriteLine($"3 - Devolver");
                Console.WriteLine($"4 - Visualizar empréstimos");
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

            if (NaoHaAmigosDisponiveis()) return;
            if (NaoHaRevistasDisponiveis()) return;
            if (telaAmigo.repositorio.ExistemItensCadastrados() || telaRevista.repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref repetir); return; }
            
            Emprestimo entidade = (Emprestimo)ObterRegistro(repositorio.CadastrandoID());
            RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
        }
        protected override Emprestimo ObterRegistro(int id)
        {
            int idSelecionado = 0;
            TimeSpan diasParaDevolver = new TimeSpan(0, 0, 0, 0);

            Amigo amigoSelecionado = new Amigo("-", "-", "-", "-");
            Revista revistaSelecionada = new Revista("-", "-", "-", null);
            Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
                RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                    () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado),
                    ref novoEmprestimo, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);
            }
            while (!IdEhValido(idSelecionado, telaAmigo, ref amigoSelecionado,
                    () => amigoSelecionado = new Amigo("-", "-", "-", "-")) || AmigoTemMulta(amigoSelecionado));

            do
            {
                TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
                RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                    () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado),
                    ref novoEmprestimo, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);
            }
            while (!IdEhValido(idSelecionado, telaRevista, ref revistaSelecionada,
                    () => revistaSelecionada = new Revista("-", "-", "-", null)) || RevistaIndisponivel(revistaSelecionada));

            BloqueiaRevista(idSelecionado, revistaSelecionada);

            diasParaDevolver = new TimeSpan(revistaSelecionada.Caixa.DiasDeEmprestimo, 0, 0, 0);

            TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", amigoSelecionado.Nome, revistaSelecionada.Titulo, DateTime.Now.ToString("d"), DateTime.Now.Add(diasParaDevolver).ToString("d"));
            Console.WriteLine();

            return new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now.Add(diasParaDevolver));
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando empréstimos em aberto...\n");

            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", 
                "Id", "Amigo", "Revista", "Data de empréstimo", "Data de devolução");

            foreach (Emprestimo emprestimo in repositorio.SelecionarTodos())
            {
                string[] parametros = [emprestimo.Id.ToString(), emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, emprestimo.DataEmprestimo.ToString("d"), emprestimo.DataDevolucao.ToString("d")];

                AjustaTamanhoDeVisualizacao(parametros);

                Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}",
                    parametros[0], parametros[1], parametros[2], parametros[3], parametros[4]);
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }
        public override void Excluir(ref bool retornar)
        {
            while (true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"Devolvendo a revista emprestada...\n");
                VisualizarRegistros(false);

                int idRegistroEscolhido = RecebeInt($"\nDigite o ID do {tipoEntidade} que deseja devolver: ");

                if (!repositorio.Existe(idRegistroEscolhido)) IdInvalido();
                else
                {
                    DateTime devolucao = RecebeData("\nInforme a data da devolução: ");
                    Emprestimo emprestimo = repositorio.SelecionarPorId(idRegistroEscolhido);

                    GeraMulta(idRegistroEscolhido, devolucao, emprestimo);
                    LiberaRevista(emprestimo);

                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido), "devolvido");
                    break;
                }
            }
        }


        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando caixa...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", "Id", "Amigo", "Revista", "Data de empréstimo", "Data de devolução");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3], texto[4]);
        }

        #region Barra o cadastro
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
        #endregion

        #region Altera outras classes
        private void BloqueiaRevista(int idSelecionado, Revista revistaSelecionada)
        {
            foreach (Revista revistas in telaRevista.repositorio.SelecionarTodos())
                if (revistas == revistaSelecionada)
                {
                    revistas.indiponivel = true;
                    telaRevista.repositorio.Editar(idSelecionado, revistas);
                }
        }
        private void LiberaRevista(Emprestimo emprestimo)
        {
            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
            {
                if (revista == emprestimo.Revista) revista.indiponivel = false;
                telaRevista.repositorio.Editar(revista.Id, revista);
            }
        }
        private void GeraMulta(int idRegistroEscolhido, DateTime devolucao, Emprestimo emprestimo)
        {
            if (devolucao > emprestimo.DataDevolucao)
                foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                    if (amigo == emprestimo.Amigo)
                    {
                        repositorio.Editar(idRegistroEscolhido, emprestimo);
                        amigo.multa = true;

                        string[] tempo = (devolucao - emprestimo.DataDevolucao).ToString().Split('.');
                        if (tempo[0].Length > 3) tempo[0] = "1";

                        Multa multa = new(emprestimo.Amigo.Nome, emprestimo.Revista.Titulo, Convert.ToInt32(tempo[0]));

                        telaMulta.repositorio.Cadastrar(multa);
                        telaAmigo.repositorio.Editar(amigo.Id, amigo);
                    }
        }
        #endregion
    }
}
