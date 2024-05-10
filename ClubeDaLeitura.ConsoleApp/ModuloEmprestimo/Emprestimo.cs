using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    internal class Emprestimo : EntidadeBase
    {
        public string Status { get; set; }

        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        

        //public Revista Revista { get; set; }
        public Amigo Amigo { get; set; }


        public Emprestimo(Amigo amigo, DateTime dataEmprestimo)
        {
            Amigo = amigo;
            //Revista = revista;

            DataEmprestimo = dataEmprestimo;
            
            
            DataDevolucao = DataDevolucao;


            Status = VerificaStatus();
        }




        public override ArrayList Validar()
        {

            ArrayList erros = new ArrayList();

            if (Amigo == null)
                erros.Add("O campo \"Amigo\" é obrigatório");

            if (DataEmprestimo == null)
                erros.Add("O campo \"Data de Emprestimo\" é obrigatório");


            return erros;
        }


        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            throw new NotImplementedException();
        }


        private DateTime dataAtual = DateTime.Now;

        public string VerificaStatus()
        {
            if(DataDevolucao < dataAtual)
                return "Válido";


            else
                return "Atrasado";
        }
    }
}
