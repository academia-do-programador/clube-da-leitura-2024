
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    internal class TelaPrincipal : TelaBase
    {
        static TelaEmprestimo telaEmprestimo = new(new RepositorioEmprestimo(), telaAmigo, telaRevista, "empréstimo");
        static TelaRevista telaRevista = new(new RepositorioRevista(), telaCaixa, "revista");
        static TelaReserva telaReserva = new(new RepositorioReserva(), telaAmigo, "reserva");
        static TelaCaixa telaCaixa = new(new RepositorioCaixa(), telaRevista, "caixa");
        static TelaAmigo telaAmigo = new(new RepositorioAmigo(), telaMulta, "amigo");
        static TelaMulta telaMulta = new(new RepositorioMulta());
        
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
            Console.WriteLine("S - Sair");

            string opcaoEscolhida = RecebeString("\nEscolha uma das opções: ");

            TelaBase tela = null;            
            switch (opcaoEscolhida)
            {
                case "1": tela = telaAmigo; break;
                case "2": tela = telaRevista; break;
                case "3": tela = telaCaixa; break;
                case "4": tela = telaEmprestimo; break;
                case "5": tela = telaReserva; break;
                case "S": sair = true; break;
                default: OpcaoInvalida(); break;
            }
            tela?.ApresentarMenu(ref sair);
        }

        public override void VisualizarRegistros(bool exibirTitulo) => throw new NotImplementedException();
        protected override EntidadeBase ObterRegistro() => throw new NotImplementedException();
    }
}
