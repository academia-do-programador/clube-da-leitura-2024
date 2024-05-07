
namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    internal class TelaPrincipal : TelaBase
    {

        public void MenuPrincipal(ref bool sair)
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|            Clube do Livro             |");
            Console.WriteLine("----------------------------------------\n");

            Console.WriteLine("1 - Cadastro de pacientes");
            Console.WriteLine("2 - Cadastro de medicamentos");
            Console.WriteLine("3 - Cadastro de requisições de Saída");
            Console.WriteLine("4 - Cadastro de funcionário");
            Console.WriteLine("5 - Cadastro de fornecedor");
            Console.WriteLine("6 - Cadastro de requisições de entrada");
            Console.WriteLine("S - Sair");

        //    string opcaoEscolhida = RecebeString("\nEscolha uma das opções: ");

        //    switch (opcaoEscolhida)
        //    {
        //        case "1": .ApresentarMenu(ref sair); break;
        //        case "2": .ApresentarMenu(ref sair); break;
        //        case "3": .ApresentarMenu(ref sair); break;
        //        case "4": .ApresentarMenu(ref sair); break;
        //        case "5": .ApresentarMenu(ref sair); break;
        //        case "6": .ApresentarMenu(ref sair); break;
        //        case "S": sair = true; break;
        //        default: OpcaoInvalida(); break;
        //    }
        //}
        public override void VisualizarRegistros(bool exibirTitulo) => throw new NotImplementedException();
        protected override EntidadeBase ObterRegistro() => throw new NotImplementedException();
    }
}
