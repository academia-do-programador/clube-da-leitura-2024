using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public Revista[] Revistas
        {
            get => default;
            set
            {
            }
        }

        public string Cor
        {
            get => default;
            set
            {
            }
        }

        public string Etiqueta
        {
            get => default;
            set
            {
            }
        }

        public string CorDaCaixa { get; }
        public string DiasdeEmprestimo { get; }
        public string Revista { get; }

        public int TempoEmprestimo
        {
            get => default;
            set
            {
            }
        }
        public Caixa(string etiqueta, string corCaixa, string diasdeEmprestimo, string revista)
        {
            Etiqueta = etiqueta;
            CorDaCaixa = corCaixa;
            DiasdeEmprestimo = diasdeEmprestimo;
            Revista = revista;
        }

         public void Validar()
        {

        }
       }
      }

