namespace APISquad7.Model
{
    public interface IProjetoRepository
    {
        bool Add(Projeto projeto);

        List<Projeto> Get();

        List<Projeto> GetByIdUsuario(int idUsuario);

        bool Delete(int idProjeto);
    }
}
