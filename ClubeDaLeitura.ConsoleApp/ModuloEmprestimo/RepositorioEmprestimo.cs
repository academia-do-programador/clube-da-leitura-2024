using ControleMedicamentos.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public ArrayList SelecionarEmprestimosDoMes()
        {
            ArrayList emprestimosDoMes = new ArrayList();

            foreach (Emprestimo e in registros)
            {
                if (e.Data.Month == DateTime.Today.Month)
                    emprestimosDoMes.Add(e);
            }

            return emprestimosDoMes;
        }

        public ArrayList SelecionarEmprestimosDoDia()
        {
            ArrayList emprestimosDoDia = new ArrayList();

            foreach (Emprestimo e in registros)
            {
                if (e.Data.Month == DateTime.Today.Month && e.Data.Day == DateTime.Today.Day)
                    emprestimosDoDia.Add(e);
            }

            return emprestimosDoDia;
        }
    }
}
