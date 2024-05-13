using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataDevolucao { get; set; }

        public bool Concluido { get; set; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;

            Data = DateTime.Now;
            DataDevolucao = Data.AddDays(Revista.Caixa.TempoEmprestimo);
            Concluido = false;

        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Revista == null)
                erros.Add("O campo \"revista\" é obrigatório");

            if (Amigo == null)
                erros.Add("O campo \"amigo\" é obrigatório");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {

        }

        public void Iniciar()
        {
            Revista.Emprestar();
        }

        public void Concluir()
        {
            Revista.Devolver();
            Concluido = true;
        }

        public Multa GerarMulta()
        {
            TimeSpan diferenca = DateTime.Now - DataDevolucao;

            decimal valorMulta = 5 * diferenca.Days;

            Multa multaGerada = new Multa(valorMulta, DateTime.Now);

            Amigo.Multar(multaGerada);

            return multaGerada;
        }
    }
}