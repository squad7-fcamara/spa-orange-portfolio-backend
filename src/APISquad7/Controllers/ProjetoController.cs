using APISquad7.Infraestrutura;
using APISquad7.Model;
using APISquad7.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace APISquad7.Controllers
{
    /* Classe que recebe as requisições HTTP do usuário */
    
    [ApiController]
    [Route("api/projeto")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoController(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository ?? throw new ArgumentNullException(nameof(projetoRepository));
        }

        /* Parâmetro projetoView contém os dados que vieram do json da requisição http */
        [HttpPost]
        public IActionResult Add([FromForm] ProjetoViewModel projetoView) // passa a aceitar em formato de formaluario, não mais JSON
        {
            var filePath = Path.Combine("Imagens", projetoView.Imagem.FileName); // caminho do arquivo

            using Stream fileStream = new FileStream(filePath, FileMode.Create); // classe que permite que salve o arquivo dentro da minha memoria e depois eu coloque dentro do meu sistema
            projetoView.Imagem.CopyTo(fileStream);

            var projeto = new Projeto(Convert.ToInt32(projetoView.IdUsuario), projetoView.Titulo, filePath, projetoView.Tag, projetoView.Link, projetoView.Descricao);

            _projetoRepository.Add(projeto);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var projetos = _projetoRepository.Get();

            //!!! Verificar como retonar NÃO OK

            return Ok(projetos);
        }

        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadImagem(int id)
        {
            var projeto = _projetoRepository.GetByIdProjeto(id);

            var dataBytes = System.IO.File.ReadAllBytes(projeto.Imagem);

            return File(dataBytes, "image/" + Path.GetExtension(projeto.Imagem).Replace('.'.ToString(), String.Empty));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _projetoRepository.Delete(id);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }
    }
}
