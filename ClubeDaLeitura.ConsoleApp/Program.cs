using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                char opcaoPrincipalEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoPrincipalEscolhida == 'S' || opcaoPrincipalEscolhida == 's')
                    break;

                TelaBase tela = null;

                //if (opcaoPrincipalEscolhida == '1')
                //    tela = telaPaciente;

                //else if (opcaoPrincipalEscolhida == '2')
                //    tela = telaFornecedor;

                //else if (opcaoPrincipalEscolhida == '3')
                //    tela = telaMedicamento;

                //else if (opcaoPrincipalEscolhida == '4')
                //    tela = telaFuncionario;

                //else if (opcaoPrincipalEscolhida == '5')
                //    tela = telaRequisicaoEntrada;

                //else if (opcaoPrincipalEscolhida == '6')
                //    tela = telaRequisicaoSaida;

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
