using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    class TelaCaixa : TelaBase
    {
        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Excluir {tipoEntidade}");
            Console.WriteLine($"4 - Visualizar {tipoEntidade}s");
            Console.WriteLine($"5 - Visualizar todas as Revistas das caixas");

            Console.WriteLine();

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            if (operacaoEscolhida == '5')
            {
                ListarTodasRevistas();
                return 'S';
            }

            return operacaoEscolhida;
        }




        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Caixas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20} | {3, -30} |",
                "Id", "Etiqueta", "Cor", "Prazo Maximo para Emprestimo"
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



        public void CadastrarCaixaNovidade()
        {
            Caixa caixa = new Caixa("Novidades", "Azul", 3);

            repositorio.Cadastrar(caixa);
        }



        public void ListarTodasRevistas()
        {
            ListarRevistas();

            void ListarRevistas()
            {
                VisualizarRegistros(false);

                Console.Write("Digite o ID da caixa que deseja ver as Revistas: ");
                int idCaixa = int.Parse(Console.ReadLine());

                Caixa caixaSelecionada = (Caixa)repositorio.SelecionarPorId(idCaixa);


                Console.WriteLine(
                    "{0, -5} | {1, -10} | {2, -10} | {3, -10} |",
                    "Id", "Titulo", "Edição nº", "Ano"
                );

                foreach (Revista revista in caixaSelecionada.Revistas)
                {
                    if (revista == null)
                        continue;

                    Console.WriteLine(
                    "{0, -5} | {1, -10} | {2, -10} | {3, -10} |",

                        revista.Id, revista.Nome, revista.Edicao, revista.Ano
                    );
                }

                Console.ReadLine();
            }
        }
    }
}
