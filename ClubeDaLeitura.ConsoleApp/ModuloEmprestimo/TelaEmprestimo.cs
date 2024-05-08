using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        TelaBase telaAmigo, telaRevista;
        public TelaEmprestimo(RepositorioBase repositorio, TelaBase telaAmigo, TelaBase telaRevista, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
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
