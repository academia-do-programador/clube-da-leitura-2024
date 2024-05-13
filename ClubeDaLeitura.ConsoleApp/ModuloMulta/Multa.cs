using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    internal class Multa : EntidadeBase
    {
        public Amigo Amigo {  get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }



        public Multa(Amigo amigoMultado, decimal valor)
        {
            Amigo = amigoMultado;
            Valor = valor;
            Status = "Pendente";
        }



        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (Valor == null)
                erros.Add("O campo \"Valor\" é obrigatório");
            

            return erros;
        }



        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Multa novasInformacoes = (Multa)novoRegistro;

            this.Amigo = novasInformacoes.Amigo;
            this.Valor = novasInformacoes.Valor;
            this.Status = novasInformacoes.Status;
        }
    }
}
