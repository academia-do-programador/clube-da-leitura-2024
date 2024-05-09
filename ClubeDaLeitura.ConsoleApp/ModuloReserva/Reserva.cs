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
        public DateTime Validade { get; set; }
        public bool Status { get; set; }

        public Reserva(DateTime validade, Amigo amigo, Revista revista, bool status)
        {
            Amigo = amigo;
            Revista = revista;
            Validade = validade;
            Status = status;
        }

        public override ArrayList Validar()
        {
            throw new NotImplementedException();
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Reserva reservaAtualizada = (Reserva)novoRegistro;

            Validade = reservaAtualizada.Validade;
            Amigo = reservaAtualizada.Amigo;
            Revista = reservaAtualizada.Revista;
            Status = reservaAtualizada.Status;
        }
    }
}