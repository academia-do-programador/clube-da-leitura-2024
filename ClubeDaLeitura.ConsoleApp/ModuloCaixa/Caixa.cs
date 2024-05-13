using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public string Cor { get; set; }

        public string Etiqueta { get; set; }

        public int TempoEmprestimo { get; set; }

        public ArrayList Revistas { get; set; } = new ArrayList();
    }
}