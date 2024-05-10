using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    class TelaRevista : TelaBase
    {
        public Revista revistas;
        public TelaCaixa telaCaixa = null;
        public RepositorioCaixa repositorioCaixa = null; 
        private IEnumerable<Revista> revista;

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Revista Cadastradas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -30} | {4, -30}",
                "Id", "Titulo:", "Edição nº:", "Ano da Revista:", " Cor da Caixa Armazenada:" );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -30} |{4, -30} ",

                    revista.Id, revista.Nome, revista.Edicao, revista.Ano, revista.Caixa.Cor
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o Nome da Revista: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Numero da Edição da Revista: ");
            string edicao = Console.ReadLine();

            Console.Write("Digite o ano de Lançamento da Revista: ");
            string ano = Console.ReadLine();

            telaCaixa.VisualizarRegistros(false);

            Console.WriteLine("Digite a caixa onde está Armazenada a Revista: ");
            int IdCaixa = int.Parse(Console.ReadLine());

            Caixa caixaselecionada = (Caixa)repositorioCaixa.SelecionarPorId(IdCaixa);
            
            
            return new Revista ( nome, edicao, ano, caixaselecionada );

            
        }



        public void CadastrarEntidadeTeste()
        {
            Caixa caixateste = (Caixa) repositorioCaixa.SelecionarTodos()[0];

            Revista revista = new Revista ("nome", "edição", "anoLancamento", caixateste );

            repositorio.Cadastrar(revista);
            
        
        }
    }
}
