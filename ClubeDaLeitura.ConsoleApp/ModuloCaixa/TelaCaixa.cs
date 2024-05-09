using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    class TelaCaixa : TelaBase
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
                "{0, -10} | {1, -20} | {2, -20} | {3, -30}",
                "Id", "Etiqueta", "Cor", "Quantidade De Dias para Emprestimo"
            );

            ArrayList caixasCadastradas = repositorio.SelecionarTodos();

            foreach (Caixa caixa in caixasCadastradas)
            {
                if (caixa == null)
                    continue;

                Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -30}",

                    caixa.Id, caixa.Etiqueta, caixa.Cor, caixa.QuantidadeDiasEmprestado
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa desejada: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a Quantidade Limite de Dias para Emprestimos: ");
            int tempoEscohido = int.Parse(Console.ReadLine());


            return new Caixa(etiqueta, cor, tempoEscohido);
        }



        public void CadastrarEntidadeTeste()
        {
            Caixa caixa = new Caixa("Ação", "Azul", 5);

            repositorio.Cadastrar(caixa);
        }
    }
}
