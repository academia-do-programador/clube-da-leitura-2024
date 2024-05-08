using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class TelaEmprestimo : TelaBase
    {
        TelaBase telaAmigo, telaRevista, telaMulta;
        public TelaEmprestimo(RepositorioBase repositorio, TelaBase telaAmigo, TelaBase telaRevista, TelaBase telaMulta, string tipoEntidade)
        {
            this.repositorio = repositorio;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
            this.telaMulta = telaMulta;
            this.tipoEntidade = tipoEntidade;
        }

        public override void ApresentarMenu(ref bool sair)
        {
            bool retornar = true;
            while (retornar)
            {
                ApresentarCabecalhoEntidade("");

                Console.WriteLine($"1 - Emprestar");
                Console.WriteLine($"2 - Editar empréstimo");
                Console.WriteLine($"3 - Devolver");
                Console.WriteLine($"4 - Visualizar empréstimos");
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
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            throw new NotImplementedException();
        }

        protected override EntidadeBase ObterRegistro(int id)
        {
            //Amigo, Revista, DataEmprestimo, diasParaDevover

            int idSelecionado = 0; 
            TimeSpan diasParaDevolver = new TimeSpan(2, 0, 0, 0);
            EntidadeBase amigoSelecionado = (Amigo)telaAmigo.repositorio.SelecionarPorId(0);
            EntidadeBase revistaSelecionada = (Amigo)telaRevista.repositorio.SelecionarPorId(0);
            EntidadeBase novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, diasParaDevolver, DateTime.Now);

            return novoEmprestimo;
/*            do
            {
                RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref etiqueta,
                    () => TabelaDeCadastro(id, "{0, -5} | ", etiqueta, cor, diasDeEmprestimo));
                ItemJaCadastrado(etiqueta);
            }
            while (repositorio.ItemRepetido(etiqueta));

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref cor,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | ", etiqueta, cor, diasDeEmprestimo));

            RecebeAtributo(() => novoRegistro = new Caixa(etiqueta, cor, diasDeEmprestimo), ref novoRegistro, ref diasDeEmprestimo,
                () => TabelaDeCadastro(id, "{0, -5} | {1, -15} | {2, -15} | ", etiqueta, cor, diasDeEmprestimo));

            return novoRegistro;
*/        }
    }
}
