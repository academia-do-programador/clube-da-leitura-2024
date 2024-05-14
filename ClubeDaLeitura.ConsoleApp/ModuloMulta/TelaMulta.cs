using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    public class TelaMulta : TelaBase<Multa>, ITelaCRUD
    {
        TelaAmigo telaAmigo { get; set; }
        public TelaMulta(RepositorioMulta repositorio, TelaAmigo telaAmigo, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.tipoEntidade = tipoEntidade;
        }

        public override void ApresentarMenu(ref bool sair)
        {
            bool retornar = true;
            while (retornar)
            {
                ApresentarCabecalhoEntidade("");

                Console.WriteLine($"1 - Visualizar multas em aberto");
                Console.WriteLine($"2 - Quitar multas");
                Console.WriteLine("R - Retornar");
                Console.WriteLine("S - Sair");

                string operacaoEscolhida = RecebeString("\nEscolha uma das opções: ");
                retornar = false;

                switch (operacaoEscolhida)
                {
                    case "1": VisualizarRegistros(true); break;
                    case "2": Excluir(ref retornar); break;
                    case "R": break;
                    case "S": sair = true; break;
                    default: OpcaoInvalida(ref retornar); break;
                }
            }
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando multas em aberto...\n");

            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -5}",
                "Id", "Amigo", "Revista", "Dias de atraso", "Valor (R$)");

            foreach (Multa multa in repositorio.SelecionarTodos())
            {
                string[] parametros = [multa.Amigo, multa.Revista];
                AjustaTamanhoDeVisualizacao(parametros);

                Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -5}",
                    multa.Id, parametros[0], parametros[1], multa.DiasDeAtraso, multa.DiasDeAtraso * 5);
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }
        public override void Excluir(ref bool retornar)
        {
            while (true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"Quitando multas...\n");
                VisualizarRegistros(false);

                int idRegistroEscolhido = RecebeInt($"\nDigite o ID da multa que deseja quitar: ");

                if (!repositorio.Existe(idRegistroEscolhido)) IdInvalido();
                else
                {
                    foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                    {
                        if (amigo.Nome == repositorio.SelecionarPorId(idRegistroEscolhido).Amigo) amigo.multa = false;
                        telaAmigo.repositorio.Editar(amigo.Id, amigo);
                    }
                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido), "excluído");
                    break;
                }
            }
        }

        protected override Multa ObterRegistro(int id) => throw new NotImplementedException();
    }
}
