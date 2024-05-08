using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase
    {
        public DateTime Validade { get; set; }
        public Amigo Amigo { get; set; }
        public int Revista { get; set; }
        public bool Status { get; set; }

        public Reserva(DateTime validade, Amigo amigo, int revista, bool status)
        {
            Validade = validade;
            Amigo = amigo;
            Revista = revista;
            Status = status;
        }

        public override ArrayList Validar()
        {
            throw new NotImplementedException();
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            throw new NotImplementedException();
        }
    }
}