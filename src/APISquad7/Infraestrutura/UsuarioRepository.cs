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

        public Int64 CountByEmailSenha(string email, string senha)
        {
            using var coon = new DbConnection();

            string query = @"SELECT COUNT(*) FROM usuario where email = @emailInformado and senha = @senhaInformada;";

            Int64 qtd = (Int64) coon.Connection.ExecuteScalar(sql: query, param: new { emailInformado = email, senhaInformada = senha });

            return qtd;
        }
    }

}
