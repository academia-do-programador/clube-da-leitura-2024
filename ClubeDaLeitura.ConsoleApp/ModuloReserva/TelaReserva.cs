using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class TelaReserva : TelaBase
    {
        TelaBase telaAmigo;
        TelaBase telaRevista;
        public TelaReserva(RepositorioBase repositorio, TelaBase telaAmigo, TelaBase telaRevista, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
            this.tipoEntidade = tipoEntidade;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            bool retornar = false;
            if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando revistas...\n");

            Console.WriteLine(
                "{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "Status", "Validade");

            foreach (Reserva reserva in repositorio.SelecionarTodos())
            {
                string statusReserva = "";

                if (reserva.Status)
                    statusReserva = "Aberto";
                else
                    statusReserva = "Expirado";

                string[] parametros = [reserva.Id.ToString(), reserva.Amigo.Nome, reserva.Revista.Titulo, statusReserva, reserva.Validade.ToString()];

                AjustaTamanhoDeVisualizacao(parametros);

                Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                    parametros[0], parametros[1], parametros[2], parametros[3], parametros[4]);

                Console.ResetColor();
            }

            if (exibirTitulo) RecebeString("\n'Enter' para continuar ");
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            int idSelecionado = 0;
            DateTime dataSelecionada = DateTime.Now;
            EntidadeBase amigoSelecionado = new Amigo("-", "-", "-", "-");
            EntidadeBase revistaSelecionada = new Revista("-", "-", "-", null);

            EntidadeBase novaReserva = new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref amigoSelecionado, telaAmigo, "amigo", ref idSelecionado);
            }
            while (false);

            do
            {
                TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d"));
                RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                    () => revistaSelecionada = (Revista)telaRevista.repositorio.SelecionarPorId(idSelecionado),
                    ref novaReserva, ref revistaSelecionada, telaRevista, "revista", ref idSelecionado);
            }
            while (false);

            RecebeAtributo(() => new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, true),
                ref novaReserva, ref dataSelecionada,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", amigoSelecionado.Nome, revistaSelecionada.Titulo, dataSelecionada.ToString("d")));

            return new Reserva(amigoSelecionado, revistaSelecionada, dataSelecionada, DateTime.Now < dataSelecionada.Add(new TimeSpan(2, 0, 0, 0))); 
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
    }
}
