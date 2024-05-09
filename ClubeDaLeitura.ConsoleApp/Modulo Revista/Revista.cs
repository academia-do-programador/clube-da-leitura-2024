using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Modulo_Revista


{
    class Revista : EntidadeBase
    {
        public string NomedaRevista  { get; set; }

        public string Edicao { get; set; }

        public string Ano { get; set; }

        public Caixa Caixa { get; set; }



        public Revista (string nome, string edicao, string ano, Caixa caixa)
        {
            NomedaRevista = nome;
            Edicao = edicao;
            Ano = ano;
            Caixa = caixa;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(NomedaRevista.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");

            if (string.IsNullOrEmpty(Ano.Trim()))
                erros.Add("O campo \"ano\" é obrigatório");

            if (string.IsNullOrEmpty(Edicao.Trim()))
                erros.Add("O campo \"edição da revista\" é obrigatório");

            if (Caixa==null)
                erros.Add("O campo \"caixa\" é obrigatório");

            return erros;
        }


        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Revista novasInformacoes = (Revista)novoRegistro;

            this.NomedaRevista = novasInformacoes.NomedaRevista;
            this.Ano = novasInformacoes.Ano;
            this.Edicao = novasInformacoes.Edicao;
            this.Caixa = novasInformacoes.Caixa;
        }
    }
}