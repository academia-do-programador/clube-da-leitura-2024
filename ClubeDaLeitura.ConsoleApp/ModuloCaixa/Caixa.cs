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
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public string DiasDeEmprestimo { get; set; }
        public Revista[] Revistas { get; set; }
        public Caixa(string etiqueta, string cor, string diasDeEmprestimo, Revista revistas)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
            Revistas = revistas;
        }

        public override ArrayList Validar()
        {
            throw new NotImplementedException();
        }
    }
}