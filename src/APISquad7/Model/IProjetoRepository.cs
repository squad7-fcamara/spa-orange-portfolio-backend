namespace APISquad7.Model
{
    public interface IProjetoRepository
    {
        bool Add(Projeto projeto);

        List<Projeto> Get();

        List<Projeto> GetByIdUsuario(int idUsuario);

        Projeto GetByIdProjeto(int idProjeto);

        bool Delete(int idProjeto);
    }
}
