using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        public string Edicao { get; set; }
        public string Ano { get; set; }
        public EntidadeBase Caixa { get; set; }
        public bool indiponivel; //reservada ou emprestada

        public Revista(string titulo, string edicao, string ano, EntidadeBase caixa)
        {
            Titulo = titulo;
            Edicao = edicao;
            Ano = ano;
            Caixa = caixa;
        }

        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            VerificaNulo(ref erros, Titulo, "titulo");
            VerificaNulo(ref erros, Edicao, "edição");
            VerificaNulo(ref erros, Ano, "ano");
            VerificaNulo(ref erros, Caixa);
            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Revista revistaAtualizada = (Revista)novoRegistro;

            Titulo = revistaAtualizada.Titulo;
            Edicao = revistaAtualizada.Edicao;
            Ano = revistaAtualizada.Ano;
            Caixa = revistaAtualizada.Caixa;
            indiponivel = revistaAtualizada.indiponivel;
        }
    }
}