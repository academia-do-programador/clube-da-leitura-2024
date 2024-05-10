using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;


namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class Reserva : EntidadeBase
    {
        public string Status { get; set; }
     
        public DateTime DataReserva { get; set; }

        private DateTime dataLimite;

        private DateTime dataAtual = DateTime.Now;

        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
                

       
        public Reserva (Amigo amigo, Revista revista, DateTime dataReserva)
        {
            Amigo = amigo;
            Revista = revista;

            DataReserva = dataReserva;
            dataLimite = dataReserva.AddDays(2);


            Status = VerificaStatus();
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (Revista == null)
                erros.Add("O campo \"Revista\" é obrigatória");


            return erros;
        }



        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Reserva novasInformacoes = (Reserva)novoRegistro;

            this.Amigo = novasInformacoes.Amigo;
            this.Revista = novasInformacoes.Revista;
            this.DataReserva = novasInformacoes.DataReserva;
            this.Status = novasInformacoes.Status;
        }


        public string VerificaStatus()
        {
            if (dataAtual >= dataLimite)
                return "Expirada";


            else
                return "Válida";
        }
    }
}
