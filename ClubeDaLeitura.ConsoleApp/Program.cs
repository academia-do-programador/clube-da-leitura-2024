﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Instâncias da Classe "Amigo"
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

            TelaAmigo telaAmigo = new TelaAmigo();
            telaAmigo.tipoEntidade = "Amigo";
            telaAmigo.repositorio = repositorioAmigo;

            telaAmigo.CadastrarEntidadeTeste();
            #endregion

            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();

            TelaCaixa telaCaixa = new TelaCaixa();
            telaCaixa.tipoEntidade = "Caixa";
            telaCaixa.repositorio = repositorioCaixa;

            telaCaixa.CadastrarEntidadeTeste();


            while (true)
            {
                char opcaoPrincipalEscolhida = TelaPrincipal.ApresentarMenuPrincipal();

                if (opcaoPrincipalEscolhida == 'S' || opcaoPrincipalEscolhida == 's')
                    break;

                TelaBase tela = null;

                if (opcaoPrincipalEscolhida == '1')
                    tela = telaAmigo;

                else if (opcaoPrincipalEscolhida == '2')
                    tela = telaCaixa;

                //else if (opcaoPrincipalEscolhida == '3')
                //    tela = telaMedicamento;

                //else if (opcaoPrincipalEscolhida == '4')
                //    tela = telaFuncionario;

                //else if (opcaoPrincipalEscolhida == '5')
                //    tela = telaRequisicaoEntrada;

                //else if (opcaoPrincipalEscolhida == '6')
                //    tela = telaRequisicaoSaida;

                char operacaoEscolhida = tela.ApresentarMenu();

                if (operacaoEscolhida == 'S' || operacaoEscolhida == 's')
                    continue;

                if (operacaoEscolhida == '1')
                    tela.Registrar();

                else if (operacaoEscolhida == '2')
                    tela.Editar();

                else if (operacaoEscolhida == '3')
                    tela.Excluir();

                else if (operacaoEscolhida == '4')
                    tela.VisualizarRegistros(true);
            }
        }
    }
}
