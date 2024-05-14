using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public List<EntidadeBase> SelecionarEmprestimosDoMes()
        {
            List<EntidadeBase> emprestimosDoMes = new List<EntidadeBase>();

            foreach (Emprestimo e in registros)
            {
                if (e.Data.Month == DateTime.Today.Month)
                    emprestimosDoMes.Add(e);
            }

            return emprestimosDoMes;
        }

        public List<EntidadeBase> SelecionarEmprestimosDoDia()
        {
            List<EntidadeBase> emprestimosDoDia = new List<EntidadeBase>();

            foreach (Emprestimo e in registros)
            {
                if (e.Data.Month == DateTime.Today.Month && e.Data.Day == DateTime.Today.Day)
                    emprestimosDoDia.Add(e);
            }

            return emprestimosDoDia;
        }

        public List<EntidadeBase> SelecionarEmprestimosEmAberto()
        {
            List<EntidadeBase> emprestimosEmAberto = new List<EntidadeBase>();

            foreach (Emprestimo e in registros)
            {
                if (!e.Concluido)
                    emprestimosEmAberto.Add(e);
            }

            return emprestimosEmAberto;
        }
    }
}
