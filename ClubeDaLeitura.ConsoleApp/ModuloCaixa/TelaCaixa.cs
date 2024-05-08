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
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando Caixas...\n");

            Console.WriteLine(
                "{0, -5} | {1, -15} | {2, -15} | {3, -20}",
                "Id", "Etiqueta", "Cor", "Tempo de empréstimo");

            foreach (Caixa caixa in repositorio.SelecionarTodos())
            {
                string[] parametros = [caixa.Id.ToString(), caixa.Etiqueta, caixa.Cor, caixa.DiasDeEmprestimo];

                AjustaTamanhoDeVisualizacao(parametros);
                CorDaCaixa(caixa.Cor);

                Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -20}",
                    parametros[0], parametros[1], parametros[2], parametros[3]);

                Console.ResetColor();
            }
           
            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }
        protected override EntidadeBase ObterRegistro(int id)
        {
            string etiqueta = "-", cor = "-", diasDeEmprestimo = "-";
            EntidadeBase novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo);

            do
            {
                RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref etiqueta,
                    () => TabelaDeCadastro(id, "{0, -5} | ", etiqueta, cor, diasDeEmprestimo));
                ItemJaCadastrado(etiqueta);
            }
            while (repositorio.ItemRepetido(etiqueta));

            do
            {
                RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref cor,
                    () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", etiqueta, cor, diasDeEmprestimo));
                CorNaoExiste(cor);
            }
            while (!CorDaCaixa(cor)); Console.ResetColor();

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref diasDeEmprestimo,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", etiqueta, cor, diasDeEmprestimo));

            return novoRegistro;
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando caixa...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15}", "Id", "Etiqueta", "Cor", "Tempo de Empréstimo");
            
            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2]);
        }
        private void CorNaoExiste(string cor)
        {
            if (!CorDaCaixa(cor))
            {
                ExibirMensagem("\nEsta cor não existe. Tente novamente ", ConsoleColor.Red);
                Console.ReadKey(true);
            }
        }
    }
}
