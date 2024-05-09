using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            throw new NotImplementedException();
        }
    }
}
