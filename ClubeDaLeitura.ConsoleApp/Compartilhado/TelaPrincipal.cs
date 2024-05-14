namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    public static class TelaPrincipal
    {
        public static char ApresentarMenuPrincipal()
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

            return opcaoEscolhida;
        }
    }
}
