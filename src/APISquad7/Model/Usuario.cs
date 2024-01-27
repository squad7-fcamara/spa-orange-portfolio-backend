using System.ComponentModel.DataAnnotations.Schema;

namespace APISquad7.Model
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(string nome, string sobrenome, string email, string senha)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
        }

        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Projeto> lstProjeto { get; set; }
    }
}
