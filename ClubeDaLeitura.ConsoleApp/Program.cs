using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal tela = new();
            bool sair = false;
            do tela.MenuPrincipal(ref sair);
            while (!sair);

        }
    }
}
