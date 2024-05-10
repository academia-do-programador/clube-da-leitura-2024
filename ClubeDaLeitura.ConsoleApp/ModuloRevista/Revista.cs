using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista


{
    class Revista : EntidadeBase
    {
        public string Nome  { get; set; }

        public string Edicao { get; set; }

        public string Ano { get; set; }

        public Caixa Caixa { get; set; }



        public Revista (string nome, string edicao, string ano, Caixa caixa)
        {
            Nome = nome;
            Edicao = edicao;
            Ano = ano;
            Caixa = caixa;
        }



        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");

            if (string.IsNullOrEmpty(Ano.Trim()))
                erros.Add("O campo \"ano\" é obrigatório");

            if (string.IsNullOrEmpty(Edicao.Trim()))
                erros.Add("O campo \"edição da revista\" é obrigatório");

            if (Caixa == null)
                erros.Add("O campo \"caixa\" é obrigatório");

            return erros;
        }



        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Revista novasInformacoes = (Revista)novoRegistro;

            this.Nome = novasInformacoes.Nome;
            this.Ano = novasInformacoes.Ano;
            this.Edicao = novasInformacoes.Edicao;
            this.Caixa = novasInformacoes.Caixa;
        }
    }
}