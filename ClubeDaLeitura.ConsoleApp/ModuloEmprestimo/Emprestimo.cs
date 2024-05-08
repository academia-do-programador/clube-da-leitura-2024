using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public EntidadeBase Amigo { get; set; }
        public EntidadeBase Revista { get; set; }
        public EntidadeBase Multa { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Status { get; set; }

        public Emprestimo(EntidadeBase amigo, EntidadeBase revistaEmprestada, TimeSpan diasParaDevolver, DateTime dataEmprestimo)
        {
            Amigo = amigo;
            Revista = revistaEmprestada;
            DataDevolucao = dataEmprestimo.Add(diasParaDevolver);
            DataEmprestimo = dataEmprestimo;
            Status = "aberto";
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