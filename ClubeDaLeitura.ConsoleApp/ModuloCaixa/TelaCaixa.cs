using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCaixa : TelaBase
    {
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Caixas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -10}",
                "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
            );

            ArrayList caixasCadastradas = repositorio.SelecionarTodos();

            foreach (Caixa caixa in caixasCadastradas)
            {
                if (caixa == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -10}",
                    caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.TempoEmprestimo
                );
            }

            Console.WriteLine();
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite o tempo de empréstimo máximo: ");
            int tempoEmprestimo = Convert.ToInt32(Console.ReadLine());

            return new Caixa(etiqueta, cor, tempoEmprestimo);
        }
    }
}
