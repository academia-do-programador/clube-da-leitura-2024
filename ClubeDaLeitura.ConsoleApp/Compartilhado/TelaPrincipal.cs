using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    internal class TelaPrincipal 
    {
        TelaCaixa telaCaixa;
        TelaRevista telaRevista;
        TelaAmigo telaAmigo;
        TelaMulta telaMulta;
        TelaEmprestimo telaEmprestimo;
        TelaReserva telaReserva;

        public TelaPrincipal()
        {
            telaCaixa = new(new RepositorioCaixa(), "caixa");
            telaRevista = new(new RepositorioRevista(), telaCaixa, "revista");
            telaAmigo = new(new RepositorioAmigo(), "amigo");
            telaMulta = new(new RepositorioMulta(), telaAmigo, "multa");
            telaEmprestimo = new(new RepositorioEmprestimo(), telaAmigo, telaRevista, telaMulta, "empréstimo");
            telaReserva = new(new RepositorioReserva(), telaAmigo, telaRevista, telaEmprestimo, "reserva");
        }

        public void MenuPrincipal(ref bool sair)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("|               Clube do Livro                |");
            Console.WriteLine("-----------------------------------------------\n");

            Console.WriteLine("1 - Gerir amigos");
            Console.WriteLine("2 - Gerir revistas");
            Console.WriteLine("3 - Gerir caixas");
            Console.WriteLine("4 - Gerir empréstimos");
            Console.WriteLine("5 - Gerir reservas");
            Console.WriteLine("6 - Gerir multas");
            Console.WriteLine("S - Sair");

            string opcaoEscolhida = telaAmigo.RecebeString("\nEscolha uma das opções: ");
            
            ITelaCRUD tela = null;

            switch (opcaoEscolhida)
            {
                case "1": tela = telaAmigo; break;
                case "2": tela = telaRevista; break;
                case "3": tela = telaCaixa; break;
                case "4": tela = telaEmprestimo; break;
                case "5": tela = telaReserva; break;
                case "6": tela = telaMulta; break;
                case "S": sair = true; break;
                default: telaAmigo.OpcaoInvalida(); break;
            }
            tela?.ApresentarMenu(ref sair);
        }
    }
}
