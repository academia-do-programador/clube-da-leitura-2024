using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    internal class RepositorioCaixa : RepositorioBase
    {
        public bool AdicionaRevista(int id, Caixa novaEntidade, Revista revistaSelecionada)
        {
            novaEntidade.Id = id;

            foreach (Caixa caixa in registros)
            {
                if (caixa == null)
                    continue;

                else if (caixa.Id == id)
                {
                    caixa.AdicionarRevista(novaEntidade, revistaSelecionada);

                    return true;
                }
            }

            return false;
        }
    }
}
