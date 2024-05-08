
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System;
using System.Collections;
namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class TelaBase
    {
        public string tipoEntidade = "";
        public RepositorioBase repositorio;

        public void ApresentarMenu(ref bool sair)
        {
            bool retornar = true;
            while (retornar)
            {
                ApresentarCabecalhoEntidade("");

                Console.WriteLine($"1 - Cadastrar {tipoEntidade}");
                Console.WriteLine($"2 - Editar {tipoEntidade}");
                Console.WriteLine($"3 - Excluir {tipoEntidade}");
                Console.WriteLine($"4 - Visualizar {tipoEntidade}s");
                Console.WriteLine("R - Retornar");
                Console.WriteLine("S - Sair");

                string operacaoEscolhida = RecebeString("\nEscolha uma das opções: ");
                retornar = false;

                switch (operacaoEscolhida)
                {
                    case "1": Registrar(); break;
                    case "2": Editar(ref retornar); break;
                    case "3": Excluir(ref retornar); break;
                    case "4": VisualizarRegistros(true); break;
                    case "R": break;
                    case "S": sair = true; break;
                    default: OpcaoInvalida(ref retornar); break;
                }
            }
        }
        public virtual void Registrar()
        {
            EntidadeBase entidade = ObterRegistro(repositorio.CadastrandoID());
            RealizaAcao(() => repositorio.Cadastrar(entidade), "cadastrado");
        }
        protected abstract EntidadeBase ObterRegistro(int id);
        public void Editar(ref bool retornar)
        {
            while (true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"\nEditando {tipoEntidade}...\n");
                VisualizarRegistros(false);

                int idEntidadeEscolhida = RecebeInt($"\nDigite o ID do {tipoEntidade} que deseja editar: ");

                if (!repositorio.Existe(idEntidadeEscolhida)) IdInvalido();
                else
                {
                    EntidadeBase entidade = ObterRegistro(idEntidadeEscolhida);
                    RealizaAcao(() => repositorio.Editar(idEntidadeEscolhida, entidade), "editado");
                    break;
                }
            }
        }
        public void Excluir(ref bool retornar)
        {
            while(true)
            {
                retornar = false;
                if (repositorio.ExistemItensCadastrados()) { RepositorioVazio(ref retornar); return; }

                ApresentarCabecalhoEntidade($"\nExcluindo {tipoEntidade}...\n");
                VisualizarRegistros(false);

                int idRegistroEscolhido = RecebeInt($"\nDigite o ID do {tipoEntidade} que deseja excluir: ");

                if (!repositorio.Existe(idRegistroEscolhido)) IdInvalido();
                else
                {
                    RealizaAcao(() => repositorio.Excluir(idRegistroEscolhido), "excluído");
                    break;
                }
            }
        }
        public abstract void VisualizarRegistros(bool exibirTitulo);

        #region Auxiliares
        protected void RealizaAcao(Action acao, string acaoRealizada)
        {
            acao();
            ExibirMensagem($"\nO(a) {tipoEntidade} foi {acaoRealizada}(a) com sucesso!", ConsoleColor.Green);
            Console.ReadKey(true);
        }
        public void ApresentarCabecalhoEntidade(string texto)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"               Gestão de {tipoEntidade}s      ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(texto);
        }
        protected void ApresentarErros(ArrayList erros)
        {
            foreach (string erro in erros) ExibirMensagem(erro, ConsoleColor.Red);
            Console.ReadKey(true);
        }
        protected virtual void TabelaDeCadastro(int id, params string[] texto) { }
        public void AjustaTamanhoDeVisualizacao(params string[] texto)
        {
            for (int i = 1; i < texto.Length; i++)
            {
                if (texto[i].Length > 15)
                {
                    char[] divideTexto = texto[i].ToCharArray();
                    texto[i] = null;
                    for (int j = 0; j < 12; j++) texto[i] += divideTexto[j];
                    texto[i] += "...";
                }
            }
        }
        public void ExibirMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }
        public bool CorDaCaixa(string cor)
        {
            cor = cor.ToLower();

            if (cor == "azul")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "vermelho" || cor == "vermelha")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "verde")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "branco" || cor == "branca")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "amarelo" || cor == "amarela")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "ciano" || cor == "ciana")
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "rosa")
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.Black; return true;
            }
            if (cor == "preto" || cor == "preta") return true;
            return false;
        }

        #region Inputs
        public static string RecebeString(string texto)
        {
            Console.Write(texto);
            return Console.ReadLine().ToUpper();
        }
        public int RecebeInt(string texto)
        {
            Console.Write(texto);
            string quantidade = "", input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) NaoEhNumero(ref input, texto);

            foreach (char c in input.ToCharArray())
            {
                if (c == '-') NaoEhNumero(ref input, texto);
                if (Convert.ToInt32(c) >= 48 && Convert.ToInt32(c) <= 57) quantidade += c;
            }

            if (quantidade.Length != input.Length) NaoEhNumero(ref quantidade, texto);

            return Convert.ToInt32(quantidade);
        }
        public DateTime RecebeData(string texto)
        {
            string data = RecebeString(texto);
            char[] dataValidade = data.ToCharArray();
            if (ValidaTabulacao(dataValidade) || ValidaDias(dataValidade) || ValidaMeses(dataValidade))
            {
                ExibirMensagem("\nData inválida! Tente novamente\n", ConsoleColor.Red);
                data = Convert.ToString(RecebeData(texto));
            }
            return Convert.ToDateTime(data);
        }
        public void RecebeAtributo(Action funcao, ref EntidadeBase novaEntidade, ref string atributo, Action tabelaCadastro)
        {
            ArrayList erros;
            do
            {
                tabelaCadastro();
                atributo = Console.ReadLine();
                funcao();
                erros = novaEntidade.Validar();
                if (erros.Count != 0) ApresentarErros(erros.GetRange(0, 1));
            }
            while (erros.Count != 0);
        }
        public void RecebeAtributo(Action funcao, ref EntidadeBase novaEntidade, ref int atributo, Action tabelaCadastro)
        {
            ArrayList erros;
            do
            {
                tabelaCadastro();
                atributo = RecebeInt("");
                funcao();
                erros = novaEntidade.Validar();
                if (erros.Count != 0) ApresentarErros(erros.GetRange(0, 1));
            }
            while (erros.Count != 0);
        }
        public void RecebeAtributo(Action funcao, ref EntidadeBase novaEntidade, ref DateTime atributo, Action tabelaCadastro)
        {
            ArrayList erros;
            do
            {
                tabelaCadastro();
                atributo = RecebeData("");
                funcao();
                erros = novaEntidade.Validar();
                if (erros.Count != 0) ApresentarErros(erros.GetRange(0, 1));
            }
            while (erros.Count != 0);
        }
        public void RecebeAtributo(Action funcao, Action atributo, ref EntidadeBase novaEntidade, ref EntidadeBase novoAtributo, TelaBase tela, string texto, ref int idEscolhido)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n\n{texto}s...");
            tela.VisualizarRegistros(false);
            Console.ResetColor();
            ArrayList erros;

            do
            {
                idEscolhido = RecebeInt($"\nDigite o ID do {texto}: ");
                atributo();
                funcao();
                erros = novaEntidade.Validar();
                if (erros.Count != 0) ApresentarErros(erros.GetRange(0, 1));
            }
            while (erros.Count != 0);
        }

        #endregion

        #region Validações
        public void OpcaoInvalida(ref bool retornar)
        {
            ExibirMensagem("Opção inválida. Tente novamente ", ConsoleColor.Red);
            retornar = true;
            Console.ReadKey(true);
        }
        public void OpcaoInvalida()
        {
            ExibirMensagem("Opção inválida. Tente novamente ", ConsoleColor.Red);
            Console.ReadKey(true);
        }
        public bool ValidaTabulacao(char[] dataValidade) => dataValidade.Length != 10 || dataValidade[2] != '/' || dataValidade[5] != '/';
        public bool ValidaDias(char[] dataValidade) => (dataValidade[0] != '0' && dataValidade[0] != '1' && dataValidade[0] != '2' && dataValidade[0] != '3') || (dataValidade[0] == '3' && dataValidade[1] != '0');
        public bool ValidaMeses(char[] dataValidade) => (dataValidade[3] != '0' && dataValidade[3] != '1') || (dataValidade[3] == '1' && dataValidade[4] != '0' && dataValidade[4] != '1' && dataValidade[4] != '2');
        public void NaoEhNumero(ref string input, string texto)
        {
            ExibirMensagem("Por favor, insira um número ", ConsoleColor.Red);
            input = Convert.ToString(RecebeInt(texto)); //Para garantir que, ao sair do loop, o método "RecebeInt" não vai puxar a "input" original (nula)
        }
        private void IdInvalido()
        {
            ExibirMensagem($"\nO {tipoEntidade} mencionado não existe! ", ConsoleColor.DarkYellow);
            Console.ReadKey(true);
        }
        protected void RepositorioVazio(ref bool repetir)
        {
            ExibirMensagem($"Ainda não existem itens cadastrados! ", ConsoleColor.Red);
            repetir = true;
            Console.ReadKey(true);
        }
        protected void ItemJaCadastrado(string item)
        {
            if (repositorio.ItemRepetido(item))
            {
                ExibirMensagem("\nEste item já existe. Tente novamente ", ConsoleColor.Red);
                Console.ReadKey(true);
            }
        }
        #endregion

        #endregion
    }
}
