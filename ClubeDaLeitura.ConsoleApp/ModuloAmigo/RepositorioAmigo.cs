using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo : RepositorioBase
    {
        public ArrayList SelecionarAmigosComMulta()
        {
            ArrayList amigosComMulta = new ArrayList();

            foreach (Amigo a in registros)
            {
                if (a.TemMulta)
                    amigosComMulta.Add(a);
            }

            return amigosComMulta;
        }
    }
}