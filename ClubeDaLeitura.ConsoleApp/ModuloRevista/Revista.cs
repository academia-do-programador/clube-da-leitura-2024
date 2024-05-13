using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        public string Titulo { get; set; }

        public int NumeroEdicao { get; set; }

        public int Ano { get; set; }

        public bool Emprestada { get; set; }

        public Caixa Caixa { get; set; }

        public Revista(string titulo, int numeroEdicao, int ano, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            Ano = ano;
            Caixa = caixa;

            Emprestada = false;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Titulo.Trim()))
                erros.Add("O campo \"titulo\" é obrigatório");

            if (NumeroEdicao < 0)
                erros.Add("O campo \"número da edição\" precisa ser maior ou igual a zero");

            if (Ano < 1)
                erros.Add("O campo \"ano\" precisa ser válido");

            if (Caixa == null)
                erros.Add("O campo \"caixa\" é obrigatório");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            throw new NotImplementedException();
        }

        public void Emprestar()
        {
            Emprestada = true;
        }

        public void Devolver()
        {
            Emprestada = false;
        }
    }
}