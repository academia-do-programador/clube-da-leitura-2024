using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract partial class RepositorioBase<T> where T : EntidadeBase
    {
        private List<T> registros = [];
        private int contadorId = 0;

        public void Cadastrar(T novoRegistro)
        {
            contadorId++;
            novoRegistro.Id = contadorId;
            registros.Add(novoRegistro);
        }
        public void Editar(int id, T novaEntidade)
        {
            novaEntidade.Id = id;
            foreach (T registro in registros) 
                if (registro.Id == id) registro.AtualizarRegistro(novaEntidade);
        }
        public void Excluir(int id) => registros.Remove(SelecionarPorId(id));

        public T SelecionarPorId(int id)
        {
            foreach (T registro in registros)
                if (registro.Id == id) return registro;
            return null;
        }
        public List<T> SelecionarTodos() => registros;
        public bool ExistemItensCadastrados() => registros.Count == 0;
        public int CadastrandoID() => contadorId + 1;
        public bool Existe(int id)
        {
            foreach (T entidade in registros)
                if (entidade.Id == id) return true;
            return false;
        }
        public bool ItemRepetido(string nome)
        {
            foreach(T registro in registros)
            {
                if (registro.Nome == nome) return true;
                if (registro.Etiqueta == nome) return true;
            }
            return false;
        }
    }
}
