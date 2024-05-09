using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using Microsoft.Win32;
namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        TelaBase telaAmigo, telaRevista, telaMulta;
        public TelaEmprestimo(RepositorioBase repositorio, TelaBase telaAmigo, TelaBase telaRevista, TelaBase telaMulta, string tipoEntidade)
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

            if(TodosOsAmigosTemMulta()) return;
            else
            {
                if (telaAmigo.repositorio.ExistemItensCadastrados() || telaRevista.repositorio.ExistemItensCadastrados()) RepositorioVazio(ref repetir);
                else
                {
                    Emprestimo entidade = (Emprestimo)ObterRegistro(repositorio.CadastrandoID());
                    RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
                }
            }

        }

        private bool TodosOsAmigosTemMulta()
        {
            foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                if (!amigo.multa) return false;

            Console.WriteLine("Todos os amigos tem multa :(");
            Console.ReadKey(true);
            return true;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando revistas...\n");

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
                    DateTime devolucao = RecebeData("Informe a data da devolução: ");

                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido, devolucao, telaAmigo, telaMulta), "devolvido");
                    break;
                }
            }
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            int idSelecionado = 0;
            TimeSpan diasParaDevolver = new TimeSpan(0, 0, 0, 0);

            EntidadeBase amigoSelecionado = new Amigo("-", "-", "-", "-");
            EntidadeBase revistaSelecionada = new Revista("-", "-", "-", null);
            EntidadeBase novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
                RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                    () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado),
                    ref novoEmprestimo, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);
            }
            while (ValidaMultas(amigoSelecionado));
                
            TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
            RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado),
                ref novoEmprestimo, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);

            Revista revista = (Revista)revistaSelecionada;
            Caixa caixa = (Caixa)revista.Caixa;
            diasParaDevolver = new TimeSpan(caixa.DiasDeEmprestimo, 0, 0, 0);

            TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", amigoSelecionado.Nome, revistaSelecionada.Titulo, DateTime.Now.ToString("d"), DateTime.Now.Add(diasParaDevolver).ToString("d"));
            Console.WriteLine();

            return new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now.Add(diasParaDevolver));
        }

        private bool ValidaMultas(EntidadeBase amigoSelecionado)
        {
            foreach (Amigo amigo in telaMulta.repositorio.SelecionarTodos())
                if (amigoSelecionado == amigo)
                {
                    ExibirMensagem("Este amigo possui multas em aberto. Não é possível emprestar ", ConsoleColor.Red);
                    Console.ReadKey(true);
                    return true;
                }
            return false;
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando caixa...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", "Id", "Amigo", "Revista", "Data de empréstimo", "Data de devolução");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3], texto[4]);
        }
    }
}
