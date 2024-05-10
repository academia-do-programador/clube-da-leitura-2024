using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;


using System;
using System.Collections;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    internal class Reserva : EntidadeBase
    {
        public string Status { get; set; }
        public Reserva reserva { get; set; }
 
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        
        public int ReservaValida { get; internal set; }
        public object ValidadeReserva { get; internal set; }

        public Reserva reservaValida; 
        public Reserva validadeReserva; 
        

       
        public Reserva (Reserva reserva, DateTime dataEmprestimo)
        {
            Reserva = reserva;  
            
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;

            Status = VerificaStatus();
        }

        public override ArrayList Validar()
        {

            ArrayList erros = new ArrayList();

            if (Reserva == null)
                erros.Add("O campo \"Reserva\" é obrigatório");

            if (DataEmprestimo == null)
                erros.Add("O campo \"Data de Emprestimo\" é obrigatório");


            return erros;
        }


        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            throw new NotImplementedException();
        }


        private DateTime dataAtual = DateTime.Now;
        private DateTime dataDevolucao;

        public string VerificaStatus()
        {
            if (DataDevolucao < dataAtual)
                return "Válido";


            else
                return "Atrasado";
        }
    }
}
