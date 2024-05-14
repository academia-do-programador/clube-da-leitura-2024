using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        public string Cor { get; set; }

        public string Etiqueta { get; set; }

        public int TempoEmprestimo { get; set; } // 3

        public ArrayList Revistas { get; set; } = new ArrayList();

        public Caixa(string etiqueta, string cor, int tempoEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            TempoEmprestimo = tempoEmprestimo;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Cor.Trim()))
                erros.Add("O campo \"cor\" é obrigatório");

            if (string.IsNullOrEmpty(Etiqueta.Trim()))
                erros.Add("O campo \"etiqueta\" é obrigatório");

            if (TempoEmprestimo < 1)
                erros.Add("O campo \"tempo de empréstimo\" é obrigatório");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Caixa caixa = (Caixa)novoRegistro;

            this.Etiqueta = caixa.Etiqueta;
            this.Cor = caixa.Cor;
            this.TempoEmprestimo = caixa.TempoEmprestimo;
        }
    }
}