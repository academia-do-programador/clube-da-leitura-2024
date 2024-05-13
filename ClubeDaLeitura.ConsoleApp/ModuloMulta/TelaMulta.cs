using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    class TelaMulta : TelaBase
    {
        public Multa novaMulta = null;

        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Visualizar {tipoEntidade}s");
            Console.WriteLine($"2 - Quitar {tipoEntidade}");


            Console.WriteLine();

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            if (operacaoEscolhida == '1')
            {
                VisualizarRegistros(true);
                return 'S';
            }

            else if (operacaoEscolhida == '2')
            {
                QuitarMulta();
                return 'S';
            }

            else if (operacaoEscolhida == '2')
            {
                return 'S';
            }

            return operacaoEscolhida;
        }


        public void RegistrarMulta(Amigo amigoMultado, decimal valor)
        {
            novaMulta = new Multa(amigoMultado, valor);
        }


        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Multas...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "| {0, -5} | {1, -20} | {2, -10} | {3, -15} |",
                "Id", "Amigo Multado", "Valor", "Status Atual"
                );

            ArrayList multasCadastradas = repositorio.SelecionarTodos();

            foreach (Multa multa in multasCadastradas)
            {
                if (multa == null)
                    continue;

                Console.WriteLine(
                 "| {0, -5} | {1, -20} | {2, -10} | {3, -15} |",

                    multa.Id, multa.Amigo.Nome, multa.Valor, multa.Status
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        public void QuitarMulta()
        {
            VisualizarRegistros(false);

            Console.WriteLine("Digite o ID da multa que deseja Quitar: ");
            int idMulta = Convert.ToInt32(Console.ReadLine());

            repositorio.Excluir(idMulta);
        }


        protected override EntidadeBase ObterRegistro()
        {
            Multa multa = novaMulta;

            return multa;
        }
    }
}
