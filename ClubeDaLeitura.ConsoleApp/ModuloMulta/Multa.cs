﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta
{
    public class Multa : EntidadeBase
    {
        public DateTime VetarEmprestimo { get; set; }

        public override ArrayList Validar() => throw new NotImplementedException();

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            throw new NotImplementedException();
        }
    }
}