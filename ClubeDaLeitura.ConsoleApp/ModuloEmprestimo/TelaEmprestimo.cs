using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;

using System.Collections;


namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    class TelaEmprestimo : TelaBase
    {
        public TelaAmigo telaAmigo = null;
        public RepositorioAmigo repositorioAmigo = null;

        public TelaRevista telaRevista = null;
        public RepositorioRevista repositorioRevista = null;

        public TelaMulta telaMulta = null;
        public RepositorioMulta repositorioMulta = null;
        
        public override char ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"        Gestão de {tipoEntidade}s        ");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
            Console.WriteLine($"2 - Editar {tipoEntidade}");
            Console.WriteLine($"3 - Excluir {tipoEntidade}");
            Console.WriteLine($"4 - Visualizar {tipoEntidade}s");
            Console.WriteLine($"5 - Visualizar Emprestimos do Mês");

            Console.WriteLine();

            Console.WriteLine("S - Voltar");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine());

            if(operacaoEscolhida == '5')
            {
                ListaEmprestimosMes();
                return 'S';
            }

            return operacaoEscolhida;
        }


        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
            {
                ApresentarCabecalho();

                Console.WriteLine("Visualizando Emprestimos...");
            }

            Console.WriteLine();

            Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",
                "Id", "Nome do Amigo", "Data Do Emprestimo", "Status"
            );

            ArrayList emprestimosCadastrados = repositorio.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                if (emprestimo == null)
                    continue;

                Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",

                    emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.DataEmprestimo, emprestimo.Status
                );
            }

            Console.ReadLine();
            Console.WriteLine();
        }



        protected override EntidadeBase ObterRegistro()
        {
            telaAmigo.VisualizarRegistros(false);

            Console.Write("Digite o ID do Amigo requisitante: ");
            int idAmigo = int.Parse(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);


            telaRevista.VisualizarRegistros(false);

            Console.Write("Digite o ID da Revista Requisitada: ");
            int idRevista = int.Parse(Console.ReadLine());

            Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);


            Console.Write("Digite a data do emprestimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());



            Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, dataEmprestimo);


            bool statusAmigo = MultaEmAberto(novoEmprestimo.Amigo);
            
            if(statusAmigo)
            {
                novoEmprestimo.VerificaMultasEmAberto(statusAmigo);
            }

            VerificaValidadeDevolucao(novoEmprestimo);

            return novoEmprestimo;
        }


        private void VerificaValidadeDevolucao(Emprestimo emprestimo)
        {
            if(emprestimo.Status == "Atrasado")
            {
                telaMulta.RegistrarMulta(emprestimo.Amigo, 10.00m);
                telaMulta.Registrar();
            }
        }


        private bool MultaEmAberto(Amigo amigoSelecionado)
        {
            ArrayList multasCadastradas = repositorioMulta.SelecionarTodos();

            foreach (Multa multa in multasCadastradas)
            {
                if (multa == null)
                    continue;
                
                if(amigoSelecionado.Id == multa.Amigo.Id)
                    return true;
            }

            return false;
        }


        public void CadastrarEntidadeTeste()
        {
            Amigo amigoTeste = (Amigo)repositorioAmigo.SelecionarTodos()[0];

            Revista revistaTeste = (Revista)repositorioRevista.SelecionarTodos()[0];


            DateTime dataTeste = DateTime.Now;

            Emprestimo emprestimo = new Emprestimo(amigoTeste, revistaTeste, dataTeste);

            repositorio.Cadastrar(emprestimo);
        }


        public void ListaEmprestimosMes()
        {
            Console.WriteLine("Visualizando os Emprestimos do Mês...");
            Console.WriteLine();

            Console.WriteLine(
                "| 1 - {0}   |\n" +
                "| 2 - {1} |\n" +
                "| 3 - {2}     |\n" +
                "| 4 - {3}     |\n" +
                "| 5 - {4}      |\n" +
                "| 6 - {5}     |\n" +
                "| 7 - {6}     |\n" +
                "| 8 - {7}    |\n" +
                "| 9 - {8}  |\n" +
                "| 10 - {9}  |\n" +
                "| 11 - {10} |\n" +
                "| 12 - {11} |",
                "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
                );

            Console.Write("Digite o Número do Mês que Deseja Visualizar: ");
            int mes = Convert.ToInt32(Console.ReadLine());

            string nomeMes = AdicionaNomeMes(mes);

            ArrayList emprestimosMes = new ArrayList();



            ArrayList emprestimosCadastrados = repositorio.SelecionarTodos();

            foreach (Emprestimo emprestimo in emprestimosCadastrados)
            {
                if (emprestimo == null)
                    continue;


                if (emprestimo.DataEmprestimo.Month == mes)
                {
                    emprestimosMes.Add(emprestimo);
                }
            }


            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"| Emprestimos do Mês de {nomeMes} |");
            Console.WriteLine("-----------------------------------");
            foreach (Emprestimo emprestimo in emprestimosMes)
            {
                if (emprestimo == null)
                    continue;


                Console.WriteLine(
                "| {0, -10} | {1, -20} | {2, -20} | {3, -10} |",

                    emprestimo.Id, emprestimo.Amigo.Nome, emprestimo.DataEmprestimo, emprestimo.Status
                );
            }

            Console.ReadLine();
        }



        private string AdicionaNomeMes(int numeroMes)
        {
            string nomeMes = "";

            switch (numeroMes)
            {
                case 1:
                    nomeMes = "Janeiro";
                    break;

                case 2:
                    nomeMes = "Fevereiro";
                    break;

                case 3:
                    nomeMes = "Março";
                    break;

                case 4:
                    nomeMes = "Abril";
                    break;

                case 5:
                    nomeMes = "Maio";
                    break;

                case 6:
                    nomeMes = "Junho";
                    break;

                case 7:
                    nomeMes = "Julho";
                    break;

                case 8:
                    nomeMes = "Agosto";
                    break;

                case 9:
                    nomeMes = "Setembro";
                    break;

                case 10:
                    nomeMes = "Outubro";
                    break;

                case 11:
                    nomeMes = "Novembro";
                    break;

                case 12:
                    nomeMes = "Dezembro";
                    break;
            }

            return nomeMes;
        }
    }
}