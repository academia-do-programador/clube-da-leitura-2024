using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class TelaReserva : TelaBase
    {
        TelaBase telaAmigo;
        public TelaReserva(RepositorioBase repositorio, TelaBase telaAmigo, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
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

                Console.Write("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                    parametros[0], parametros[1], parametros[2], parametros[3], parametros[4]);

                Console.ResetColor();
            }
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            throw new NotImplementedException();
        }

        protected override void TabelaDeCadastro(int id, params string[] texto)
        {
            Console.Clear();
            ApresentarCabecalhoEntidade($"Cadastrando Reservas...\n");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Reserva", "Status", "Validade");

            AjustaTamanhoDeVisualizacao(texto);

            Console.Write(texto[0], id, texto[1], texto[2], texto[3]);
        }
    }
}
