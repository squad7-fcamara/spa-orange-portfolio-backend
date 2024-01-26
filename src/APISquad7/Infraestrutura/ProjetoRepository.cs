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
	                            titulo, imagem_projeto, tag, link, descricao)
	                            VALUES (@titulo, @imagem, @tag, @link, @descricao);";

            var result = conn.Connection.Execute(sql: query, param: projeto);

            return result == 1;
        }

        public List<Projeto> Get()
        {
            using var coon = new DbConnection();
            
            string query = @"SELECT id_projeto, titulo, imagem_projeto, tag, link, descricao FROM projeto;";

            var result = coon.Connection.Query<Projeto>(sql: query);

            return result.ToList<Projeto>();
        }
    }
}
