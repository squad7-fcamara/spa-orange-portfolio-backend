namespace APISquad7.Model
{
    public interface IProjetoRepository
    {
        bool Add(Projeto projeto);

        bool Update(Projeto projeto);

        List<Projeto> GetByIdUsuario(int idUsuario);

        Projeto GetByIdProjeto(int idProjeto);

        List<Projeto> GetByUsuarioTags(int idUsuario, string tags);

        List<Projeto> GetComunidade(int idUsuario, string tags);

        bool Delete(int idProjeto);
    }
}
