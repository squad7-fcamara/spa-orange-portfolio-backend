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

        public bool Update(Projeto projeto)
        {
            using var conn = new DbConnection();

            string update = @"UPDATE projeto set titulo = @titulo, tag = @tag, link = @link, descricao = @descricao";

            if (projeto.Imagem != "") // Em uma edição, só altera imagem se ela for informada
            {
                update += ", imagem_projeto = @imagem";
            }

            string query = update + " where id_projeto = @idProjeto;";

            var result = conn.Connection.Execute(sql: query, param: projeto);

            if (result == 1)
            {
                return true;
            }

            return false;
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

        public Projeto GetByIdProjeto(int idProjeto)
        {
            using var coon = new DbConnection();

            string query = @"SELECT id_projeto as IdProjeto, id_usuario as IdUsuario, titulo, imagem_projeto as Imagem, 
                                tag, link, descricao, data_criacao as DataCriacao FROM projeto
                                where id_projeto = @idProjetoInformado;";

            var result = coon.Connection.Query<Projeto>(sql: query, param: new { idProjetoInformado = idProjeto });

            return result.ToList<Projeto>()[0];
        }

        /* Formato esperado das tags: nomeTag1;nomeTag2 */
        public List<Projeto> GetByUsuarioTags(int idUsuario, string tags)
        {
            using var coon = new DbConnection();

            List<string> lstTags = tags.Split(';').ToList();

            string consultaTag = "";

            foreach (string tag in lstTags)
            {
                // Consulta para cobrir todas as possibilidades de tags na base de dados
                // Foi feito para impedir que uma busca pela tag java retorne projetos com a tag javascript
                consultaTag += "tag like '" + tag.ToLower() + ";%' or ";
                consultaTag += "tag like '%;" + tag.ToLower() + ";%' or ";
                consultaTag += "tag like '%;" + tag.ToLower() + "' or ";
                consultaTag += "tag like '" + tag.ToLower() + "' or ";
            }

            consultaTag = consultaTag.Substring(0, consultaTag.Length - 4); // Remove o último " or "

            string where = "where (" + consultaTag + ") and id_usuario = @idUsuarioInformado;";

            string query = @"SELECT id_projeto as IdProjeto, id_usuario as IdUsuario, titulo, imagem_projeto as Imagem, 
                                tag, link, descricao, data_criacao as DataCriacao FROM projeto " + where;

            var result = coon.Connection.Query<Projeto>(sql: query, param: new { idUsuarioInformado = idUsuario });

            return result.ToList<Projeto>();
        }

        /* Formato esperado das tags: nomeTag1;nomeTag2 */
        public List<Projeto> GetComunidade(int idUsuario, string tags)
        {
            using var coon = new DbConnection();

            string consultaTag = "";

            string where = "where p.id_usuario <> @idUsuarioInformado";

            if (tags != null)
            {
                List<string> lstTags = tags.Split(';').ToList();

                foreach (string tag in lstTags)
                {
                    // Consulta para cobrir todas as possibilidades de tags na base de dados
                    // Foi feito para impedir que uma busca pela tag java retorne projetos com a tag javascript
                    consultaTag += "tag like '" + tag.ToLower() + ";%' or ";
                    consultaTag += "tag like '%;" + tag.ToLower() + ";%' or ";
                    consultaTag += "tag like '%;" + tag.ToLower() + "' or ";
                    consultaTag += "tag like '" + tag.ToLower() + "' or ";
                }

                consultaTag = consultaTag.Substring(0, consultaTag.Length - 4); // Remove o último " or "

                where += " and (" + consultaTag + ");";
            }

            string query = @"SELECT id_projeto as IdProjeto, u.id_usuario as IdUsuario, titulo, imagem_projeto as Imagem, 
                                tag, link, descricao, data_criacao as DataCriacao,
                                CONCAT(u.nome, ' ', u.sobrenome) as nomeCompleto FROM 
                                projeto p inner join usuario u on p.id_usuario = u.id_usuario "
                                + where;

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
