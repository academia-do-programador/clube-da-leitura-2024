using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                char opcaoTelaEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoTelaEscolhida == 'S' || opcaoTelaEscolhida == 's')
                    break;

                TelaBase tela = null;

                if (tela == null)
                    continue;

                char operacaoEscolhida = tela.ApresentarMenu();

                if (operacaoEscolhida == 'S' || operacaoEscolhida == 's')
                    continue;

                if (operacaoEscolhida == '1')
                    tela.Registrar();

                else if (operacaoEscolhida == '2')
                    tela.Editar();

                else if (operacaoEscolhida == '3')
                    tela.Excluir();

                else if (operacaoEscolhida == '4')
                    tela.VisualizarRegistros(true);
            }
        }
    }
}
