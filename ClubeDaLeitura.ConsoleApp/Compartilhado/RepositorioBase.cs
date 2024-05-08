using System.Collections;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase
    {
        private ArrayList registros = [];
        public int contadorId = 0;

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
        public void Excluir(int id)
        {
            foreach (EntidadeBase registro in registros)
                if (registro.Id == id) registros.Remove(registro);
        }

        public EntidadeBase SelecionarPorId(int id)
        {
            foreach (EntidadeBase registro in registros)
                if (registro.Id == id) return registro;
            return null;
        }
        public bool Existe(int id)
        {
            foreach(EntidadeBase entidade in registros) 
                if (entidade.Id == id) return true;
            return false;
        }
    }

}
