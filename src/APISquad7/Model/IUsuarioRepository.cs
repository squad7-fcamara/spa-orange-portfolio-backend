namespace APISquad7.Model
{
    public interface IUsuarioRepository
    {
        int Add(Usuario usuario);

        List<Usuario> Get();

        Usuario GetByIdUsuario(int idUsuario);

        Usuario GetByEmailSenha(string email, string senha);
    }
}
