namespace APISquad7.ViewModel
{
    public class ProjetoViewModel
    {
        public string? IdProjeto { get; set; }
        public string? IdUsuario { get; set; }
        public string Titulo { get; set; }
        public IFormFile? Imagem { get; set; }
        public string Tag { get; set; }
        public string Link { get; set; }
        public string Descricao { get; set; }
    }
}
