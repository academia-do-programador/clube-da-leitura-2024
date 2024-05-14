using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

            TelaAmigo telaAmigo = new TelaAmigo();
            telaAmigo.tipoEntidade = "Amigo";
            telaAmigo.repositorio = repositorioAmigo;

            telaAmigo.CadastrarEntidadeTeste();

            //RepositorioCaixa repositorioCaixa = new RepositorioCaixa();

            //TelaCaixa telaCaixa = new TelaCaixa();
            //telaCaixa.tipoEntidade = "Caixa";
            //telaCaixa.repositorio = repositorioCaixa;

            //telaCaixa.CadastrarEntidadeTeste();

            //RepositorioRevista repositorioRevista = new RepositorioRevista();

            //TelaRevista telaRevista = new TelaRevista();
            //telaRevista.tipoEntidade = "Revista";
            //telaRevista.repositorio = repositorioRevista;
            //telaRevista.repositorioCaixa = repositorioCaixa;

            //telaRevista.CadastrarEntidadeTeste();

            //RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

            //TelaEmprestimo telaEmprestimo = new TelaEmprestimo();
            //telaEmprestimo.tipoEntidade = "Empréstimo";
            //telaEmprestimo.repositorio = repositorioEmprestimo;

            //telaEmprestimo.repositorioAmigo = repositorioAmigo;
            //telaEmprestimo.repositorioRevista = repositorioRevista;

            //telaEmprestimo.telaAmigo = telaAmigo;
            //telaEmprestimo.telaRevista = telaRevista;

            //RepositorioReserva repositorioReserva = new RepositorioReserva();

            //TelaReserva telaReserva = new TelaReserva();
            //telaReserva.tipoEntidade = "Reserva";
            //telaReserva.repositorio = repositorioReserva;
            //telaReserva.repositorioAmigo = repositorioAmigo;
            //telaReserva.repositorioRevista = repositorioRevista;

            //telaReserva.telaAmigo = telaAmigo;
            //telaReserva.telaRevista = telaRevista;
            //telaReserva.telaEmprestimo = telaEmprestimo;

            while (true)
            {
                char opcaoTelaPrincipalEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoTelaPrincipalEscolhida == 'S' || opcaoTelaPrincipalEscolhida == 's')
                    break;

                TelaBase tela = null;

                if (opcaoTelaPrincipalEscolhida == '1')
                    tela = telaAmigo;

                //else if (opcaoTelaPrincipalEscolhida == '2')
                //    tela = telaCaixa;

                //else if (opcaoTelaPrincipalEscolhida == '3')
                //    tela = telaRevista;

                //else if (opcaoTelaPrincipalEscolhida == '4')
                //    tela = telaEmprestimo;

                //else if (opcaoTelaPrincipalEscolhida == '5')
                //    tela = telaReserva;

                if (tela == null)
                    continue;

                char operacaoSubmenuEscolhida = tela.ApresentarMenu();

                if (operacaoSubmenuEscolhida == 'S' || operacaoSubmenuEscolhida == 's')
                    continue;

                //if (tela.tipoEntidade == "Empréstimo")
                //{
                //    if (operacaoSubmenuEscolhida == '1')
                //        telaEmprestimo.Registrar();

                //    else if (operacaoSubmenuEscolhida == '2')
                //        telaEmprestimo.Concluir();

                //    else if (operacaoSubmenuEscolhida == '3')
                //        telaEmprestimo.VisualizarRegistros(true);
                //}

                //else if (tela.tipoEntidade == "Reserva")
                //{
                //    if (operacaoSubmenuEscolhida == '1')
                //        telaReserva.Registrar();

                //    else if (operacaoSubmenuEscolhida == '2')
                //        telaReserva.AbrirEmprestimo();

                //    else if (operacaoSubmenuEscolhida == '3')
                //        telaReserva.VisualizarRegistros(true);
                //}

                else if (operacaoSubmenuEscolhida == '1')
                    tela.Registrar();

                else if (operacaoSubmenuEscolhida == '2')
                    tela.Editar();

                else if (operacaoSubmenuEscolhida == '3')
                    tela.Excluir();

                else if (operacaoSubmenuEscolhida == '4')
                    tela.VisualizarRegistros(true);

                else if (operacaoSubmenuEscolhida == '5' && tela.tipoEntidade == "Amigo")
                    telaAmigo.PagarMulta();
            }

        }
    }
}