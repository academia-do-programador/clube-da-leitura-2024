using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

            TelaAmigo telaAmigo = new TelaAmigo();
            telaAmigo.tipoEntidade = "Amigo";
            telaAmigo.repositorio = repositorioAmigo;

            telaAmigo.CadastrarEntidadeTeste();

            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();

            TelaCaixa telaCaixa = new TelaCaixa();
            telaCaixa.tipoEntidade = "Caixa";
            telaCaixa.repositorio = repositorioCaixa;

            telaCaixa.CadastrarEntidadeTeste();

            RepositorioRevista repositorioRevista = new RepositorioRevista();

            TelaRevista telaRevista = new TelaRevista();
            telaRevista.tipoEntidade = "Revista";
            telaRevista.repositorio = repositorioRevista;
            telaRevista.repositorioCaixa = repositorioCaixa;

            telaRevista.CadastrarEntidadeTeste();

            #region Empréstimos
            //RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

            //TelaEmprestimo telaEmprestimo = new TelaEmprestimo();
            //telaEmprestimo.tipoEntidade = "Empréstimo";
            //telaEmprestimo.repositorio = repositorioEmprestimo;
            //telaEmprestimo.repositorioAmigo = repositorioAmigo;
            //telaEmprestimo.repositorioRevista = repositorioRevista;

            //telaEmprestimo.telaAmigo = telaAmigo;
            //telaEmprestimo.telaRevista = telaRevista;
            #endregion

            #region Reservas
            //RepositorioReserva repositorioReserva = new RepositorioReserva();

            //TelaReserva telaReserva = new TelaReserva();
            //telaReserva.tipoEntidade = "Reserva";
            //telaReserva.repositorio = repositorioReserva;
            //telaReserva.repositorioAmigo = repositorioAmigo;
            //telaReserva.repositorioRevista = repositorioRevista;

            //telaReserva.telaAmigo = telaAmigo;
            //telaReserva.telaRevista = telaRevista;
            //telaReserva.telaEmprestimo = telaEmprestimo;
            #endregion

            while (true)
            {
                char opcaoTelaEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoTelaEscolhida == 'S' || opcaoTelaEscolhida == 's')
                    break;

                TelaBase tela = null;

                if (opcaoTelaEscolhida == '1')
                    tela = telaAmigo;

                else if (opcaoTelaEscolhida == '2')
                    tela = telaCaixa;

                else if (opcaoTelaEscolhida == '3')
                    tela = telaRevista;

                //else if (opcaoTelaEscolhida == '4')
                //    tela = telaEmprestimo;

                //else if (opcaoTelaEscolhida == '5')
                //    tela = telaReserva;

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