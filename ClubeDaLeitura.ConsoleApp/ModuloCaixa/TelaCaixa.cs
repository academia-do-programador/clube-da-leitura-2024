using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class TelaCaixa : TelaBase
    {
        public TelaCaixa(RepositorioBase repositorio, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.tipoEntidade = tipoEntidade;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (!repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando Caixas...");

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -15} | {3, -15}",
                "Id", "Etiqueta", "Cor", "Tempo de empréstimo");

            foreach (Caixa caixa in repositorio.SelecionarTodos())
                Console.WriteLine("{0, -10} | {1, -15} | {2, -15} | {3, -15}",
                    caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.DiasDeEmprestimo);
            
            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            string etiqueta = "-", cor = "-", diasDeEmprestimo = "-";
            EntidadeBase novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo);

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref etiqueta,
                () => TabelaDeCadastro(id, "{0, -5} | ", etiqueta, cor, diasDeEmprestimo));

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref cor,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", etiqueta, cor, diasDeEmprestimo));

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref diasDeEmprestimo,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", etiqueta, cor, diasDeEmprestimo));

            return novoRegistro;
        }
        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando caixa...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15}", "Id", "Etiqueta", "Cor", "Tempo de Empréstimo");
            Console.Write(texto[0], id, texto[1], texto[2]);
        }
    }   
}
