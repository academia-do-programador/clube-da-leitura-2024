using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class Caixa : EntidadeBase
    {
        public string Etiqueta { get; set; }
        public string Cor {  get; set; }
        public int QuantidadeDiasEmprestado { get; set; }


        public Revista[] Revistas { get; set; }


        //Faltando Adicionar as Revistas

        public Caixa(string etiqueta, string cor, int tempoEscolhido)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            QuantidadeDiasEmprestado = tempoEscolhido;
            
            Revistas = new Revista[100];
        }


        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Etiqueta.Trim()))
                erros.Add("O campo \"etiqueta\" é obrigatória");

            if (string.IsNullOrEmpty(Cor.Trim()))
                erros.Add("O campo \"Cor Da Caixa\" é obrigatória");
            
            if(QuantidadeDiasEmprestado == null)
            {
                erros.Add("O campo de Dias para Emprestimo é obrigatório");
            }

            return erros;
        }


        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Caixa novasInformacoes = (Caixa)novoRegistro;

            this.Etiqueta = novasInformacoes.Etiqueta;
            this.Cor = novasInformacoes.Cor;
            this.QuantidadeDiasEmprestado = novasInformacoes.QuantidadeDiasEmprestado;
        }



        public bool AdicionouRevista(Revista novaRevista)
        {


            return false;
        }
    }
}
