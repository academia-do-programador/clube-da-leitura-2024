using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo Amigo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Status { get; set; }
        public Revista Revista { get; set; }

        public Emprestimo(Amigo amigo, Revista revistaEmprestada, DateTime dataEmprestimo, DateTime dataDevolucao, bool status)
        {
            Amigo = amigo;
            Revista = revistaEmprestada;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
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