using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class TelaCaixa : TelaBase
    {
        TelaBase telaRevista;
        public TelaCaixa(RepositorioBase repositorio, TelaBase telaRevista, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaRevista = telaRevista;
            this.tipoEntidade = tipoEntidade;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (!repositorio.ExistemItensCadastrados()) { RepositorioVazio(); return; };
            if (exibirTitulo) ApresentarCabecalhoEntidade("Visualizando Caixas...");

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Etiqueta", "Cor", "Tempo de empréstimo");
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            throw new NotImplementedException();
        }
    }
}
