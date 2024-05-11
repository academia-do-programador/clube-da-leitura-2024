using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    class TelaRevista : TelaBase
    {
        public TelaCaixa telaCaixa = null;
        public RepositorioCaixa repositorioCaixa = null;


        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Revista Cadastradas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -5} | {1, -10} | {2, -10} | {3, -10} | {4, -10}",
                "Id", "Titulo", "Edição nº", "Ano", "Caixa Armazenada" 
                );

            ArrayList revistasCadastradas = repositorio.SelecionarTodos();

            foreach (Revista revista in revistasCadastradas)
            {
                if (revista == null)
                    continue;

                Console.WriteLine(
                    "{0, -5} | {1, -10} | {2, -10} | {3, -10} | {4, -10}",

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
            int idCaixa = int.Parse(Console.ReadLine());

            Caixa caixasSelecionada = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

            Revista novaRevista = new Revista(nome, edicao, ano, caixasSelecionada);

            AdicionaRevista(caixasSelecionada, novaRevista);
            
            return novaRevista;
        }



        public void CadastrarEntidadeTeste()
        {
            Caixa caixateste = (Caixa) repositorioCaixa.SelecionarTodos()[0];
            

            Revista revista = new Revista ("Batman", "1251", "1970", caixateste );

            repositorio.Cadastrar(revista); 
        }



        public void AdicionaRevista(Caixa caixaSelecionada, Revista revistaSelecionada)
        {
            repositorioCaixa.AdicionaRevista(caixaSelecionada.Id, caixaSelecionada, revistaSelecionada);
        }


    }
}
