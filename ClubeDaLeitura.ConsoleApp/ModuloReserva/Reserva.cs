using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase
    {
        public EntidadeBase Amigo { get; set; }
        public EntidadeBase Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Status { get; set; }

        public Reserva( EntidadeBase amigo, EntidadeBase revista, DateTime validade, bool status)
        {
            Amigo = amigo;
            Revista = revista;
            DataReserva = validade;
            Status = status;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            VerificaNulo(ref erros, Amigo);
            VerificaNulo(ref erros, Revista);

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Reserva reservaAtualizada = (Reserva)novoRegistro;

            DataReserva = reservaAtualizada.DataReserva;
            Amigo = reservaAtualizada.Amigo;
            Revista = reservaAtualizada.Revista;
            Status = reservaAtualizada.Status;
        }
    }
}