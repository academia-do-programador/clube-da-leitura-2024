using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using System.Collections;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase
    {
        private ArrayList registros = [];
        private int contadorId = 0;

        public void Cadastrar(EntidadeBase novoRegistro)
        {
            contadorId++;
            novoRegistro.Id = contadorId;
            registros.Add(novoRegistro);
        }
        public void Editar(int id, EntidadeBase novaEntidade)
        {
            novaEntidade.Id = id;
            foreach (EntidadeBase registro in registros) 
                if (registro.Id == id) registro.AtualizarRegistro(novaEntidade);
        }
        public void Excluir(int id) => registros.Remove(SelecionarPorId(id));
        public void Excluir(int id, DateTime devolucao, TelaBase telaAmigo, TelaBase telaMulta)
        {
            Emprestimo emprestimo = (Emprestimo)SelecionarPorId(id);

            if (devolucao > emprestimo.DataDevolucao)
                foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                    if (amigo == emprestimo.Amigo) 
                    {
                        telaMulta.repositorio.Cadastrar(amigo);
                        amigo.multa = true;
                    }

            registros.Remove(emprestimo);
        }

        public EntidadeBase SelecionarPorId(int id)
        {
            foreach (EntidadeBase registro in registros)
                if (registro.Id == id) return registro;
            return null;
        }
        public ArrayList SelecionarTodos() => registros;
        public bool ExistemItensCadastrados() => registros.Count == 0;
        public int CadastrandoID() => contadorId + 1;
        public bool Existe(int id)
        {
            foreach(EntidadeBase entidade in registros) 
                if (entidade.Id == id) return true;
            return false;
        }
        public bool ItemRepetido(string nome)
        {
            foreach(EntidadeBase registro in registros)
            {
                if (registro.Nome == nome) return true;
                if (registro.Etiqueta == nome) return true;
            }
            return false;
        }
        public void GerarMulta()
        {

        }
    }
}
