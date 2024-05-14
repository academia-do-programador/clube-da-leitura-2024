using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase
    {
        public Amigo Amigo { get; set; }

        public Revista Revista { get; set; }

        public DateTime DataAbertura { get; set; }

        public bool Expirada
        {
            get
            {
                return (DateTime.Now - DataAbertura).Days > 2;
            }
        }

        public Reserva(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;

            DataAbertura = DateTime.Now;
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
            Reserva reserva = (Reserva)novoRegistro;

            this.Revista = reserva.Revista;
            this.Amigo = reserva.Amigo;
        }

        public void Iniciar()
        {
            Revista.Emprestar();
        }

        public void Concluir()
        {
            Revista.Devolver();
        }
    }
}