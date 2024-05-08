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
            
            if (telaAmigo.repositorio.ExistemItensCadastrados() || telaRevista.repositorio.ExistemItensCadastrados()) RepositorioVazio(ref repetir);
            else
            {
                Emprestimo entidade = (Emprestimo)ObterRegistro(repositorio.CadastrandoID());
                RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
            }
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            throw new NotImplementedException();
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            int idSelecionado = 0; 
            TimeSpan diasParaDevolver = new TimeSpan(0, 0, 0, 0);

            EntidadeBase amigoSelecionado = new Amigo("-", "-", "-", "-");
            EntidadeBase revistaSelecionada = new Revista("-", "-", "-", null);
            EntidadeBase novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now);


            TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
            RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado), 
                ref novoEmprestimo, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);


            TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, "-", "-");
            RecebeAtributo(() => new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now),
                () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado), 
                ref novoEmprestimo, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);


            Revista revista = (Revista)revistaSelecionada;
            diasParaDevolver = new TimeSpan(revista.Caixa.DiasDeEmprestimo, 0, 0, 0);

            TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, DateTime.Now.ToString("d"), DateTime.Now.Add(diasParaDevolver).ToString("d"));

            return new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now, DateTime.Now.Add(diasParaDevolver));
        }
        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando caixa...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -5}", "Id", "Amigo", "Revista", "Data empréstimo", "Data devolução");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3], texto[4]);
        }

    }
}
