namespace ClubeDaLeitura.ConsoleApp.Compartilhado
{
    #region Interfaces
    //  Interfaces definem um contrato que todas as classes que a implementam deve seguir

    //      Uma interface declara "o que uma classe deve ter"
    //      Uma classe implementando a interface define "como deve ser feito"

    //      Benefícios = herança múltipla + extensibilidade do código

    //      Geralmente, interfaces são declaradas com o prefixo I, ex: ITelaCadastravel
    #endregion

    #region Generics
    //  Generics permitem criarmos código não-específico para tipos de dados abrangentes

    //      É possível adicionar o marcador <T> para: classes, métodos, campos, etc.

    //      Benefícios = reusabilidade de código para tipos de dados específicos
    //          + identificação de código genérico em tempo de desenvolvimento
    //          + diminuição o uso de cast (Tipo)
    #endregion

    public interface ITelaCadastravel
    {
        char ApresentarMenu();

        void Registrar();
        void Editar();
        void Excluir();
        void VisualizarRegistros(bool exibirTitulo);
    }
}