namespace APISquad7.Model
{
    public interface IUsuarioRepository
    {
        bool Add(Usuario usuario);

        List<Usuario> Get();
    }
}
