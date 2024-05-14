using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo : EntidadeBase
    {
        public string Nome { get; set; }

        public string NomeResponsavel { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public ArrayList Multas { get; set; } = new ArrayList();

        public bool TemMulta
        {
            get
            {
                for (int i = 0; i < Multas.Count; i++)
                {
                    Multa multa = (Multa)Multas[i];

                    if (!multa.EstaPaga)
                        return true;
                }

                return false;
            }
        }

        public decimal ValorMulta
        {
            get
            {
                decimal valor = 0;

                for (int i = 0; i < Multas.Count; i++)
                {
                    Multa multa = (Multa)Multas[i];

                    if (!multa.EstaPaga)
                        valor += multa.Valor;
                }

                return valor;
            }
        }

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
            Endereco = endereco;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");

            if (string.IsNullOrEmpty(NomeResponsavel.Trim()))
                erros.Add("O campo \"nome do responsável\" é obrigatório");

            if (string.IsNullOrEmpty(Telefone.Trim()))
                erros.Add("O campo \"telefone\" é obrigatório");

            if (string.IsNullOrEmpty(Endereco.Trim()))
                erros.Add("O campo \"endereço\" é obrigatório");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Amigo amigo = (Amigo)novoRegistro;

            this.Nome = amigo.Nome;
            this.NomeResponsavel = amigo.NomeResponsavel;
            this.Telefone = amigo.Telefone;
            this.Endereco = amigo.Endereco;
        }

        public void Multar(Multa multa)
        {
            Multas.Add(multa);
        }

        public void PagarMultas()
        {
            for (int i = 0; i < Multas.Count; i++)
            {
                Multa multa = (Multa)Multas[i];

                if (!multa.EstaPaga)
                    multa.Pagar();
            }
        }
    }
}