using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using System.Collections;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase
    {
        private List<EntidadeBase> registros = [];
        private int contadorId = 0;

        public void Cadastrar(EntidadeBase novoRegistro)
        {
            contadorId++;
            novoRegistro.Id = contadorId;
            registros.Add(novoRegistro);
        }
        public void Cadastrar(EntidadeBase novoRegistro, TelaBase telaRevista)
        {
            contadorId++;
            novoRegistro.Id = contadorId++;
            registros.Add(novoRegistro);

            Reserva reserva = (Reserva)novoRegistro;

            if (!reserva.Status) return;

            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
                if (revista == reserva.Revista)
                {
                    revista.indiponivel = true;
                    telaRevista.repositorio.Editar(revista.Id, revista);
                }
        }
        public void Editar(int id, EntidadeBase novaEntidade)
        {
            novaEntidade.Id = id;
            foreach (EntidadeBase registro in registros) 
                if (registro.Id == id) registro.AtualizarRegistro(novaEntidade);
        }
        public void Excluir(int id) => registros.Remove(SelecionarPorId(id));
        public void Excluir(int id, TelaBase telaAmigo)
        {
            Emprestimo multa = (Emprestimo)SelecionarPorId(id);
            registros.Remove(multa);
            foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
            {
                if (amigo == multa.Amigo) amigo.multa = false;
                telaAmigo.repositorio.Editar(amigo.Id, amigo);
            }
        }
        public void Excluir(int id, DateTime devolucao, TelaBase telaAmigo, TelaBase telaMulta, TelaBase telaRevista)
        {
            Emprestimo emprestimo = (Emprestimo)SelecionarPorId(id);

            if (devolucao > emprestimo.DataDevolucao)
                foreach (Amigo amigo in telaAmigo.repositorio.SelecionarTodos())
                    if (amigo == emprestimo.Amigo) 
                    {
                        string[] tempo = (devolucao - emprestimo.DataDevolucao).ToString().Split('.');
                        if (tempo[0].Length > 3) tempo[0] = "1";
                        emprestimo.TempoAtraso = Convert.ToInt32(tempo[0]);
                        Editar(id, emprestimo);
                        amigo.multa = true;

                        telaMulta.repositorio.Cadastrar(emprestimo);
                        telaAmigo.repositorio.Editar(amigo.Id, amigo);
                    }
            
            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
            {
                if (revista == emprestimo.Revista) revista.indiponivel = false;
                telaRevista.repositorio.Editar(revista.Id, revista);
            }

            registros.Remove(emprestimo);
        }
        public void Excluir(int id, TelaBase telaRevista, int i)
        {
            Reserva reserva = (Reserva)SelecionarPorId(id);
            registros.Remove(reserva);

            foreach (Revista revista in telaRevista.repositorio.SelecionarTodos())
            {
                if (revista == reserva.Revista) revista.indiponivel = false;
                telaRevista.repositorio.Editar(revista.Id, revista);
            }
        }

        public EntidadeBase SelecionarPorId(int id)
        {
            foreach (EntidadeBase registro in registros)
                if (registro.Id == id) return registro;
            return null;
        }
        public List<EntidadeBase> SelecionarTodos() => registros;
        public bool ExistemItensCadastrados() => registros.Count == 0;
        public int CadastrandoID() => contadorId + 1;
        public bool Existe(int id, TelaBase tela)
        {
            foreach(EntidadeBase entidade in tela.repositorio.registros) 
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
    }
}
