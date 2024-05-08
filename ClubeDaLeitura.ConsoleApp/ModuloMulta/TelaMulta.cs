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
        public TelaMulta(RepositorioBase repositorio) => this.repositorio = repositorio;

        protected override EntidadeBase ObterRegistro(int id)
        {
            throw new NotImplementedException();
        }
        public override void VisualizarRegistros(bool exibirTitulo) => throw new NotImplementedException();
    }
}
