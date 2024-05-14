using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;

namespace ClubeDaLeitura.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                ITelaCadastravel tela = telaPrincipal.ApresentarMenuPrincipal();

                if (tela == null)
                    break;

                char operacaoSubmenuEscolhida = tela.ApresentarMenu();

                if (operacaoSubmenuEscolhida == 'S' || operacaoSubmenuEscolhida == 's')
                    continue;

                // asserção -> cast
                if (tela is TelaEmprestimo telaEmprestimo)
                    GerenciarEmprestimos(operacaoSubmenuEscolhida, telaEmprestimo);

                else if (tela is TelaReserva telaReserva)
                    GerenciarReservas(operacaoSubmenuEscolhida, telaReserva);

                else if (operacaoSubmenuEscolhida == '1')
                    tela.Registrar();

                else if (operacaoSubmenuEscolhida == '2')
                    tela.Editar();

                else if (operacaoSubmenuEscolhida == '3')
                    tela.Excluir();

                else if (operacaoSubmenuEscolhida == '4')
                    tela.VisualizarRegistros(true);

                else if (operacaoSubmenuEscolhida == '5' && tela is TelaAmigo telaAmigo)
                    telaAmigo.PagarMulta();
            }
        }

        static void GerenciarReservas(char operacaoSubmenuEscolhida, TelaReserva telaReserva)
        {
            if (operacaoSubmenuEscolhida == '1')
                telaReserva.Registrar();

            else if (operacaoSubmenuEscolhida == '2')
                telaReserva.AbrirEmprestimo();

            else if (operacaoSubmenuEscolhida == '3')
                telaReserva.VisualizarRegistros(true);
        }

        static void GerenciarEmprestimos(char operacaoSubmenuEscolhida, TelaEmprestimo telaEmprestimo)
        {
            if (operacaoSubmenuEscolhida == '1')
                telaEmprestimo.Registrar();

            else if (operacaoSubmenuEscolhida == '2')
                telaEmprestimo.Concluir();

            else if (operacaoSubmenuEscolhida == '3')
                telaEmprestimo.VisualizarRegistros(true);
        }
    }
}