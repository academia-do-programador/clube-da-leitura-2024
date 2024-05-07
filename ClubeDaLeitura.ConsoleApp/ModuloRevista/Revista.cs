using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        public string Titulo { get; set; }
        public string Edicao { get; set; }
        public int Ano { get; set; }
        public Caixa Caixa { get; set; }
        public Revista(string titulo, string edicao, int ano, Caixa caixa)
        {
            Titulo = titulo;
            Edicao = edicao;
            Ano = ano;
            Caixa = caixa;
        }

        public override ArrayList Validar()
        {
            throw new NotImplementedException();
        }
    }
}