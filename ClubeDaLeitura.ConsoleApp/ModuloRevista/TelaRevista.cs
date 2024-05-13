using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class TelaRevista : TelaBase
    {
        public TelaCaixa telaCaixa = null;
        public RepositorioCaixa repositorioCaixa = null;

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Revistas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -10} | {4, -20}",
                "Id", "Titulo", "Edição", "Ano", "Caixa"
            );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -10} | {4, -20}",
                    revista.Id, revista.Titulo, revista.NumeroEdicao, revista.Ano, revista.Caixa.Etiqueta
                );
            }

            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o título da revista: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o número da edição: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o ano de lançamento: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            telaCaixa.VisualizarRegistros(false);

            Console.Write("Digite o ID da caixa em que deseja guardar a revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(titulo, numeroEdicao, ano, caixaSelecionada);
        }
    }
}
