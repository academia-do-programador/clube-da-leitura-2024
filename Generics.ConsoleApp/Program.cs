namespace Generics.ConsoleApp
{
    class EntidadeBase
    {
        public int Id { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] strings = { "1", "2", "3" };

            int[] ints = { 1, 2, 3 };

            bool[] bools = { true, false };

            EntidadeBase newEntidade = new EntidadeBase();
            newEntidade.Id = 1;

            ExibirValores<EntidadeBase>(new EntidadeBase[] { newEntidade });

            Console.ReadLine();
        }

        // <T> é um placeholder do tipo que estamos passando

        public static void ExibirValores<Tipo>(Tipo[] objetos) where Tipo : EntidadeBase
        {
            foreach (Tipo objeto in objetos)
            {
                Console.WriteLine(objeto.Id);
            }
        }
    }
}
