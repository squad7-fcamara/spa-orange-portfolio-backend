using APISquad7.Model;
using Dapper;
using Npgsql;

namespace APISquad7.Infraestrutura
{
    public class UsuarioRepository : IUsuarioRepository
    {
        /*
         * Retornos possíveis:
         * 0 - Inclusão deu certo;
         * 1 - Inclusão falhou;
         * 2 - Violação de chave única;
         */
        public int Add(Usuario usuario)
        {
            try
            {
                using var conn = new DbConnection();

                string query = @"INSERT INTO usuario(
	                            nome, sobrenome, email, senha)
	                            VALUES (@nome,@sobrenome,@email,@senha);";

                var result = conn.Connection.Execute(sql: query, param: usuario);

                return 0;
            }
            catch (NpgsqlException ex)
            {
                if (ex.SqlState == "23505") //Código sql que significa violação de chave única
                {
                    return 2;
                }

                return 1;
            }
            catch (Exception)
            {
                return 1;
            }
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
