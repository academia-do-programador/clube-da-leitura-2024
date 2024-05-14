using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCaixa : TelaBase<Caixa>, ITelaCadastravel
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

            List<Caixa> caixasCadastradas = repositorio.SelecionarTodos();

            foreach (Caixa caixa in caixasCadastradas)
            {
                if (caixa == null)
                    continue;

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -20} | {3, -10}",
                    caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.TempoEmprestimo
                );
            }

            Console.ReadLine();
        }

        protected override Caixa ObterRegistro()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite o tempo de empréstimo máximo: ");
            int tempoEmprestimo = Convert.ToInt32(Console.ReadLine());

            return new Caixa(etiqueta, cor, tempoEmprestimo);
        }

        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = new Caixa("Novidades", "Azul", 3);

            repositorio.Cadastrar(caixa);
        }
    }
}