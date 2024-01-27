using APISquad7.Model;
using Dapper;

namespace APISquad7.Infraestrutura
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public bool Add(Usuario usuario)
        {
            using var conn = new DbConnection();

            string query = @"INSERT INTO usuario(
	                            nome, sobrenome, email, senha)
	                            VALUES (@nome,@sobrenome,@email,@senha);";

            var result = conn.Connection.Execute(sql: query, param: usuario);

            return result == 1;
        }

        public List<Usuario> Get()
        {
            using var coon = new DbConnection();

            string query = @"SELECT id_usuario as IdUsuario, nome, sobrenome, email FROM usuario;";

            var result = coon.Connection.Query<Usuario>(sql: query);

            return result.ToList<Usuario>();
        }

        public Usuario GetByIdUsuario(int idUsuario)
        {
            using var coon = new DbConnection();

            string query = @"SELECT id_usuario as IdUsuario, nome, sobrenome, email FROM usuario 
                                where id_usuario = @idUsuarioInformado;";

            var result = coon.Connection.Query<Usuario>(sql: query, param: new { idUsuarioInformado = idUsuario });

            return result.ToList<Usuario>()[0];
        }

        public int GetByEmailSenha(string email, string senha)
        {
            using var coon = new DbConnection();

            string query = @"SELECT id_usuario FROM usuario where email = @emailInformado and senha = @senhaInformada;";

            var result = coon.Connection.ExecuteScalar(sql: query, param: new { emailInformado = email, senhaInformada = senha });

            int idUsuario = -1;
            
            if (result != null)
            {
                idUsuario = (int)result;
            }

            return idUsuario;
        }
    }

}
