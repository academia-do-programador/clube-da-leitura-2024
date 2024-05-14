using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private RepositorioAmigo repositorioAmigo;
        private TelaAmigo telaAmigo;

        private RepositorioCaixa repositorioCaixa;
        private TelaCaixa telaCaixa;

        //private RepositorioRevista repositorioRevista;
        //private TelaRevista telaRevista;

        //private RepositorioEmprestimo repositorioEmprestimo;
        //private TelaEmprestimo telaEmprestimo;

        //private RepositorioReserva repositorioReserva;
        //private TelaReserva telaReserva;

        public TelaPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();

            telaAmigo = new TelaAmigo();
            telaAmigo.tipoEntidade = "Amigo";
            telaAmigo.repositorio = repositorioAmigo;

            telaAmigo.CadastrarEntidadeTeste();

            repositorioCaixa = new RepositorioCaixa();

            telaCaixa = new TelaCaixa();
            telaCaixa.tipoEntidade = "Caixa";
            telaCaixa.repositorio = repositorioCaixa;

            telaCaixa.CadastrarEntidadeTeste();

            //repositorioRevista = new RepositorioRevista();

            //telaRevista = new TelaRevista();
            //telaRevista.tipoEntidade = "Revista";
            //telaRevista.repositorio = repositorioRevista;
            //telaRevista.repositorioCaixa = repositorioCaixa;

            //telaRevista.CadastrarEntidadeTeste();

            //repositorioEmprestimo = new RepositorioEmprestimo();

            //telaEmprestimo = new TelaEmprestimo();
            //telaEmprestimo.tipoEntidade = "Empréstimo";
            //telaEmprestimo.repositorio = repositorioEmprestimo;

            //telaEmprestimo.repositorioAmigo = repositorioAmigo;
            //telaEmprestimo.repositorioRevista = repositorioRevista;

            //telaEmprestimo.telaAmigo = telaAmigo;
            //telaEmprestimo.telaRevista = telaRevista;

            //repositorioReserva = new RepositorioReserva();

            //telaReserva = new TelaReserva();
            //telaReserva.tipoEntidade = "Reserva";
            //telaReserva.repositorio = repositorioReserva;
            //telaReserva.repositorioAmigo = repositorioAmigo;
            //telaReserva.repositorioRevista = repositorioRevista;

            //telaReserva.telaAmigo = telaAmigo;
            //telaReserva.telaRevista = telaRevista;
            //telaReserva.telaEmprestimo = telaEmprestimo;
        }

        public ITelaCadastravel ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|           Clube da Leitura           |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Cadastro de Amigos");
            Console.WriteLine("2 - Cadastro de Caixas");
            Console.WriteLine("3 - Cadastro de Revistas");
            Console.WriteLine("4 - Cadastro de Empréstimos");
            Console.WriteLine("5 - Reserva de Revista");

            Console.WriteLine("S - Sair");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");

            char opcaoEscolhida = Console.ReadLine()[0];

            ITelaCadastravel tela = null;

            if (opcaoEscolhida == '1')
                tela = telaAmigo;

            else if (opcaoEscolhida == '2')
                tela = telaCaixa;

            //else if (opcaoEscolhida == '3')
            //    tela = telaRevista;

            //else if (opcaoEscolhida == '4')
            //    tela = telaEmprestimo;

            //else if (opcaoEscolhida == '5')
            //    tela = telaReserva;

            return tela;
        }
    }
}

