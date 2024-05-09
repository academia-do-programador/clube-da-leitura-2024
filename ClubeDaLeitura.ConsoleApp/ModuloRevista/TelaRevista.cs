using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    internal class TelaRevista : TelaBase
    {
        TelaBase telaCaixa;
        public TelaRevista(RepositorioBase repositorio, TelaBase telaCaixa, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaCaixa = telaCaixa;
            this.tipoEntidade = tipoEntidade;
        }

        public override void Registrar()
        {
            bool repetir = false;

            ApresentarCabecalhoEntidade($"Cadastrando {tipoEntidade}...\n");
            if (telaCaixa.repositorio.ExistemItensCadastrados())
            {
                RepositorioVazio(ref repetir);
                ExibirMensagem("Nenhuma Caixa cadastrada! Caixas são necessárias para o cadastro de revistas.", ConsoleColor.Red);
                Console.ReadKey(true);
            }
            else            
                base.Registrar();
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando Revista...");

            Console.WriteLine(
                "{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Titulo", "Edição", "Ano", "Caixa");

            foreach (Revista revista in repositorio.SelecionarTodos())
            {
                string[] parametros = [revista.Id.ToString(), revista.Titulo, revista.Edicao, revista.Ano, revista.Caixa.Etiqueta];

                AjustaTamanhoDeVisualizacao(parametros);

                Console.Write("{0, -5} | {1, -15} | {2, -15} | {3, -15} |",
                    parametros[0], parametros[1], parametros[2], parametros[3]);

                telaCaixa.CorDaCaixa(revista.Caixa.Cor);

                Console.WriteLine(" {0, -15}", parametros[4]);

                Console.ResetColor();
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            string titulo = "-", edicao = "-", ano = "-";

            int idCaixa = 0;

            Caixa caixa = new Caixa("-","-",0);

            EntidadeBase caixaSelecionada = new Caixa("-", "-", 0);

            EntidadeBase novoRegistro = new Revista(titulo, edicao, ano, caixa);

            Action caixaa = () => caixaSelecionada = (Caixa)telaCaixa.repositorio.SelecionarPorId(idCaixa);

            Action novaRev = () => novoRegistro = new Revista(titulo, edicao, ano, caixa);

            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixa), ref novoRegistro, ref titulo,
                () => TabelaDeCadastro(id, "{0, -5} | ", titulo, edicao, ano, caixa.Etiqueta));
            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixa), ref novoRegistro, ref edicao,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", titulo, edicao, ano, caixa.Etiqueta));
            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixa), ref novoRegistro, ref ano,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", titulo, edicao, ano, caixa.Etiqueta));

            TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -15} ", titulo, edicao, ano, caixa.Etiqueta);
            RecebeAtributo(novaRev, caixaa, ref novoRegistro, ref caixaSelecionada, telaCaixa, "Caixa", ref idCaixa);

            caixa = (Caixa)telaCaixa.repositorio.SelecionarPorId(idCaixa);

            novoRegistro = new Revista(titulo, edicao, ano, caixa);

            return novoRegistro;
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando Revistas...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Titulo", "Edição", "Ano", "Caixa");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3]);
        }
    }
}
