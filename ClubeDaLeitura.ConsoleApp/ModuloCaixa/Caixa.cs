using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        public string Cor { get; set; }
        public int DiasDeEmprestimo { get; set; }
        public ArrayList revistas;

        public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
        }

        public override List<string> Validar()
        {
            List<string> erros = [];
            VerificaNulo(ref erros, Etiqueta, "etiqueta");
            VerificaNulo(ref erros, Cor, "cor");
            return erros;
        }   
        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Caixa caixaAtualizada = (Caixa)novoRegistro;

            Etiqueta = caixaAtualizada.Etiqueta;
            Cor = caixaAtualizada.Cor;
            DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
        }
    }
}