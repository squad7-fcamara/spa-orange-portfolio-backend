namespace APISquad7.Model
{
    public interface IProjetoRepository
    {
        bool Add(Projeto projeto);

        bool Update(Projeto projeto);

        List<Projeto> Get();

        List<Projeto> GetByIdUsuario(int idUsuario);

        Projeto GetByIdProjeto(int idProjeto);

        List<Projeto> GetByTags(int idUsuario, string tags);

        bool Delete(int idProjeto);
    }
}
