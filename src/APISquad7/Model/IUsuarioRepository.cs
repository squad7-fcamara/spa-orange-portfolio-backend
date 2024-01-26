namespace APISquad7.Model
{
    public interface IUsuarioRepository
    {
        bool Add(Usuario usuario);

        List<Usuario> Get();

        Int64 CountByEmailSenha(string email, string senha);
    }
}
