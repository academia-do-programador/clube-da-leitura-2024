using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    internal class TelaMulta : TelaBase
    {
        TelaAmigo telaAmigo { get; set; }
        public TelaMulta(RepositorioMulta repositorio, TelaAmigo telaAmigo)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
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
