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
        public string Nome { get; set; }
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
            throw new NotImplementedException();
        }
    }
}