using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        public Emprestimo(EntidadeBase amigo, EntidadeBase revistaEmprestada, DateTime dataEmprestimo, DateTime dataDevolucao)
        {
            Amigo = amigo;
            Revista = revistaEmprestada;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
        }

        public override List<string> Validar()
        {
            List<string> erros = [];
            VerificaNulo(ref erros, Amigo);
            VerificaNulo(ref erros, Revista);
            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Emprestimo emprestimo = (Emprestimo)novoRegistro;

            Amigo = emprestimo.Amigo;
            Revista = emprestimo.Revista;
            DataEmprestimo = emprestimo.DataEmprestimo;
            DataDevolucao = emprestimo.DataDevolucao;
        }
    }
}