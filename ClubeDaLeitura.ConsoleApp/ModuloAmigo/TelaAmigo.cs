using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    internal class TelaAmigo : TelaBase
    {
        TelaBase telaMulta;
        public TelaAmigo(RepositorioBase repositorio, TelaBase telaMulta, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaMulta = telaMulta;
            this.tipoEntidade = tipoEntidade;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando amigos...\n");

            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -5}",
                "Id", "Nome", "Responsável", "Telefone", "Endereço");

            foreach (Amigo amigo in repositorio.SelecionarTodos())
            {
                string[] parametros = [amigo.Id.ToString(), amigo.Nome, amigo.NomeResponsavel, amigo.Telefone, amigo.Endereco];

                AjustaTamanhoDeVisualizacao(parametros);

                Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -5}",
                    parametros[0], parametros[1], parametros[2], parametros[3], parametros[4]);
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }
        protected override EntidadeBase ObterRegistro(int id)
        {
            string nome = "-", nomeResp = "-", telefono = "-", endereco = "-";
            EntidadeBase novoRegistro = new Amigo(nome, nomeResp, telefono, endereco);

            do
            {
                RecebeAtributo(() => novoRegistro = new Amigo(nome, nomeResp, telefono, endereco), ref novoRegistro, ref nome,
                    () => TabelaDeCadastro(id, "{0, -5} | ", nome, nomeResp, telefono));
                ItemJaCadastrado(nome);
            } 
            while (repositorio.ItemRepetido(nome));

            RecebeAtributo(() => novoRegistro = new Amigo(nome, nomeResp, telefono, endereco), ref novoRegistro, ref nomeResp,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", nome, nomeResp, telefono));

            RecebeAtributo(() => novoRegistro = new Amigo(nome, nomeResp, telefono, endereco), ref novoRegistro, ref telefono,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", nome, nomeResp, telefono));

            RecebeAtributo(() => novoRegistro = new Amigo(nome, nomeResp, telefono, endereco), ref novoRegistro, ref endereco,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | {3, -15} | ", nome, nomeResp, telefono));

            return novoRegistro;
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando amigo...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -5}", "Id", "Nome", "Responsável", "Telefone", "Endereço");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3]);
        }
    }
}
