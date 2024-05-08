using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo : EntidadeBase
    {
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
            Endereco = endereco;
        }


        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            VerificaNulo(ref erros, Nome, "Nome");
            VerificaNulo(ref erros, NomeResponsavel, "Responsável");
            VerificaNulo(ref erros, Telefone, "Telefone");
            VerificaNulo(ref erros, Endereco, "Endereço");
            return erros;
        }
        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Amigo novoAmigo = (Amigo)novoRegistro;

            Nome = novoAmigo.Nome;
            NomeResponsavel = novoAmigo.NomeResponsavel;
            Telefone = novoAmigo.Telefone;
            Endereco = novoAmigo.Endereco;
        }
    }
}