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
        public IActionResult Add(ProjetoViewModel projetoView)
        {
            var projeto = new Projeto(Convert.ToInt32(projetoView.IdUsuario), projetoView.Titulo, projetoView.Imagem, projetoView.Tag, projetoView.Link, projetoView.Descricao);

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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _projetoRepository.Delete(id);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }
    }
}
