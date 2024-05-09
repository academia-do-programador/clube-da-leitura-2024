using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
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
            ApresentarCabecalhoEntidade($"Cadastrando {tipoEntidade}...\n");
            if (telaCaixa.repositorio.ExistemItensCadastrados())
            {
                ExibirMensagem("Ainda não existem caixas cadastradas.\nPara cadastrar uma revista, é necessário existir uma caixa.", ConsoleColor.Red);
                Console.ReadKey(true);
            }
            else
            {
                Revista entidade = (Revista)ObterRegistro(repositorio.CadastrandoID());
                RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
            }
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando revistas...\n");

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
            EntidadeBase caixaSelecionada = new Caixa("-", "-", 0);
            EntidadeBase novoRegistro = new Revista(titulo, edicao, ano, caixaSelecionada);

            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixaSelecionada), ref novoRegistro, ref titulo,
                () => TabelaDeCadastro(id, "{0, -5} | ", titulo, edicao, ano, caixaSelecionada.Etiqueta));
            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixaSelecionada), ref novoRegistro, ref edicao,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", titulo, edicao, ano, caixaSelecionada.Etiqueta));
            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixaSelecionada), ref novoRegistro, ref ano,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", titulo, edicao, ano, caixaSelecionada.Etiqueta));

            TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -15} ", titulo, edicao, ano, caixaSelecionada.Etiqueta);
            RecebeAtributo(() => novoRegistro = new Revista(titulo, edicao, ano, caixaSelecionada),
                () => caixaSelecionada = (Caixa)telaCaixa.repositorio.SelecionarPorId(idCaixa), ref novoRegistro, ref caixaSelecionada, telaCaixa, "caixa", ref idCaixa);

            return novoRegistro;
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando revistas...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Titulo", "Edição", "Ano", "Caixa");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3]);
        }
    }
}
