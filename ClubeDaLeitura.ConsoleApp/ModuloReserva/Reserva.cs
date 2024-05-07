using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva
    {
        private DateTime dataAbertura;

        public Amigo Amigo
        {
            get => default;
            set
            {
            }
        }

        public Revista Revista
        {
            get => default;
            set
            {
            }
        }

        public bool Expirada
        {
            get => default;
            set
            {
            }
        }
        public Reserva(string nome, string nomedaRevista, string dataEmprestimo, string dataDevolução, string statusEmprestimo)
        {
            Reserva = nome;
            NomedaRevista = nomedaRevista;
            DataEmprestimo = dataEmprestimo;
            DataDevoluçao = dataDevolução;
            StatusEmprestimo = statusEmprestimo;


        }
    }
}