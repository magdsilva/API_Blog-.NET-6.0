namespace blog.ViewModels
{
    public class ResultViewModel<T>
    {
        //Case 1: Em caso de realização de consulta retorna Dados e erros
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }
        //Case 2: Em caso de sucesso na consulta, retorna apenas os dados.
        public ResultViewModel(T data)
        {
            Data=data;
        }
        //Case 3: Em caso de insucesso na consulta, retorna a lista de erros
        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }
        //Case 4: Em caso de apenas 1 erro, vai retornar o único erro.
        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}
