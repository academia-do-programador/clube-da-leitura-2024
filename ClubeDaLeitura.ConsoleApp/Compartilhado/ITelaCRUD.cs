namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public interface ITelaCRUD
    {
        void ApresentarMenu(ref bool sair);

        void Registrar();
        void Editar(ref bool retornar);
        void Excluir(ref bool retornar);
        void VisualizarRegistros(bool exibirTitulo);
    }
}
