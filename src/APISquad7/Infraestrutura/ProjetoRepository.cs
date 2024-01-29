using APISquad7.Model;
using Dapper;

namespace APISquad7.Infraestrutura
{
    public class ProjetoRepository : IProjetoRepository
    {
        public bool Add(Projeto projeto)
        {
            using var conn = new DbConnection();

            string query = @"INSERT INTO projeto(
	                            id_usuario, titulo, imagem_projeto, tag, link, descricao, data_criacao)
	                            VALUES (@idUsuario, @titulo, @imagem, @tag, @link, @descricao, CURRENT_TIMESTAMP);";

            var result = conn.Connection.Execute(sql: query, param: projeto);

            return result == 1;
        }

        public List<Projeto> Get()
        {
            using var coon = new DbConnection();

            string query = @"SELECT p.id_projeto as IdProjeto, p.id_usuario as IdUsuario, p.titulo, p.imagem_projeto as Imagem, 
                                p.tag, p.link, p.descricao, p.data_criacao as DataCriacao, 
                                CONCAT(u.nome, ' ', u.sobrenome) as nomeCompleto FROM projeto p 
                                inner join usuario u on p.id_usuario = u.id_usuario order by DataCriacao DESC;";

            var result = coon.Connection.Query<Projeto>(sql: query);

            return result.ToList<Projeto>();
        }

        public List<Projeto> GetByIdUsuario(int idUsuario)
        {
            using var coon = new DbConnection();

            string query = @"SELECT id_projeto as IdProjeto, id_usuario as IdUsuario, titulo, imagem_projeto as Imagem, 
                                tag, link, descricao, data_criacao as DataCriacao FROM projeto
                                where id_usuario = @idUsuarioInformado;";

            var result = coon.Connection.Query<Projeto>(sql: query, param: new { idUsuarioInformado = idUsuario });

            return result.ToList<Projeto>();
        }

        public bool Delete(int idProjeto)
        {
            using var conn = new DbConnection();

            string query = @"DELETE from projeto where id_projeto = @idProjetoInformado;";

            var result = conn.Connection.Execute(sql: query, param: new { idProjetoInformado = idProjeto });

            return result == 1;
        }
    }
}
