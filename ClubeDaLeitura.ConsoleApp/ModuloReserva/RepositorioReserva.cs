using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : RepositorioBase
    {
        public ArrayList SelecionarReservasEmAberto()
        {
            ArrayList reservasEmAberto = new ArrayList();

            foreach (Reserva e in registros)
            {
                if (!e.Expirada)
                    reservasEmAberto.Add(e);
            }

            return reservasEmAberto;
        }
    }
}
