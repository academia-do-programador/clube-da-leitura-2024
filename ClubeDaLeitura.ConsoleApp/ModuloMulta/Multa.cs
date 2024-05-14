using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    public class Multa : EntidadeBase
    {
        public string Amigo { get; set; }
        public string Revista { get; set; }
        public int DiasDeAtraso { get; set; } = 0;

        public Multa(string amigo, string revista, int TempoAtraso)
        {
            Amigo = amigo;
            Revista = revista;
            DiasDeAtraso = TempoAtraso;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Multa multa = (Multa)novoRegistro;

            Amigo = multa.Amigo;
            Revista = multa.Revista;
            DiasDeAtraso = multa.DiasDeAtraso;
        }

        public override List<string> Validar()
        {
            throw new NotImplementedException();
        }
    }
}
