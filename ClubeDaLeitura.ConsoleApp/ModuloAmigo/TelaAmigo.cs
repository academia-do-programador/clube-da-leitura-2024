using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        protected override EntidadeBase ObterRegistro()
        {
            throw new NotImplementedException();
        }
    }
}
