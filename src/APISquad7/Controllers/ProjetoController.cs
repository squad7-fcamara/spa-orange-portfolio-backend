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
        /// <summary>
        /// Inclusão de projeto do usuário.
        /// </summary>
        /// <remarks>
        /// Realiza a inclusão de um projeto do usuário.
        /// <br/><br/>
        /// Dados de entrada: idUsuario, titulo, imagem, tag, link e descricao.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Projeto cadastrado com sucesso.</response>
        [HttpPost]
        public IActionResult Add([FromForm] ProjetoViewModel projetoView) // passa a aceitar em formato de formaluario, não mais JSON
        {
            var filePath = "";

            if (projetoView.Imagem != null)
            {
                filePath = Path.Combine("Imagens", projetoView.Imagem.FileName); // caminho do arquivo
                using Stream fileStream = new FileStream(filePath, FileMode.Create); // classe que permite que salve o arquivo dentro da minha memoria e depois eu coloque dentro do meu sistema
                projetoView.Imagem.CopyTo(fileStream);
            }

            var projeto = new Projeto(Convert.ToInt32(projetoView.IdProjeto), Convert.ToInt32(projetoView.IdUsuario), projetoView.Titulo, filePath, projetoView.Tag, projetoView.Link, projetoView.Descricao);

            _projetoRepository.Add(projeto);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }

        /* Parâmetro projetoView contém os dados que vieram do json da requisição http */
        /// <summary>
        /// Edição de projeto.
        /// </summary>
        /// <remarks>
        /// Realiza a edição de um projeto do usuário.
        /// <br/><br/>
        /// Dados de entrada: idProjeto, titulo, imagem, tag, link e descricao.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Projeto editado com sucesso.</response>
        [HttpPut]
        public IActionResult Put([FromForm] ProjetoViewModel projetoView) // passa a aceitar em formato de formaluario, não mais JSON
        {
            var filePath = "";

            if (projetoView.Imagem != null)
            {
                filePath = Path.Combine("Imagens", projetoView.Imagem.FileName); // caminho do arquivo
                using Stream fileStream = new FileStream(filePath, FileMode.Create); // classe que permite que salve o arquivo dentro da minha memoria e depois eu coloque dentro do meu sistema
                projetoView.Imagem.CopyTo(fileStream);
            }

            var projeto = new Projeto(Convert.ToInt32(projetoView.IdProjeto), Convert.ToInt32(projetoView.IdUsuario), projetoView.Titulo, filePath, projetoView.Tag, projetoView.Link, projetoView.Descricao);

            _projetoRepository.Update(projeto);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }

        /* serviço desabilitado
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadImagem(int id)
        {
            var projeto = _projetoRepository.GetByIdProjeto(id);

            var dataBytes = System.IO.File.ReadAllBytes(projeto.Imagem);

            return File(dataBytes, "image/" + Path.GetExtension(projeto.Imagem).Replace('.'.ToString(), ""));
        }  */

        /// <summary>
        /// Busca de projetos do usuario por tags.
        /// </summary>
        /// <remarks>
        /// Realiza a busca dos projetos do usuário que contenham alguma das tags informadas.
        /// <br/><br/>
        /// Dados de entrada: idUsuario e tags.
        /// <br/><br/>
        /// Dados de saída: idProjeto, idUsuario, titulo, imagem, tag, link, descricao e dataCriacao.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Busca realizada com sucesso.</response>
        [HttpGet("getByUsuarioTags")]
        public ActionResult<List<Projeto>> getByUsuarioTags([FromQuery] int idUsuario, [FromQuery] string tags)
        {
            var projetos = _projetoRepository.GetByUsuarioTags(idUsuario, tags);

            byte[] dataBytes;

            foreach (Projeto projeto in projetos)
            {
                if (projeto.Imagem != "")
                {
                    dataBytes = System.IO.File.ReadAllBytes(projeto.Imagem);

                    projeto.ArquivoImagem = File(dataBytes, "image/" + Path.GetExtension(projeto.Imagem).Replace('.'.ToString(), ""));
                }
            }

            //!!! Verificar como retonar NÃO OK

            return Ok(projetos);
        }

        /// <summary>
        /// Busca de todos os projetos cadastrados, exceto os do usuario logado.
        /// </summary>
        /// <remarks>
        /// Realiza a busca de todos os projetos cadastrados, exceto os do usuario logado. Se tags forem informadas, busca os projetos que contenham alguma das tags informadas.
        /// <br/><br/>
        /// Dados de entrada: idUsuario(logado) e tags
        /// <br/><br/>
        /// Dados de saída: idProjeto, idUsuario, titulo, imagem, tag, link, descricao, dataCriacao e nomeCompleto.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Busca realizada com sucesso.</response>
        [HttpGet("getComunidade")]
        public ActionResult<List<Projeto>> getComunidade([FromQuery] int idUsuario, [FromQuery] string? tags)
        {
            var projetos = _projetoRepository.GetComunidade(idUsuario, tags);

            byte[] dataBytes;

            foreach (Projeto projeto in projetos)
            {
                if (projeto.Imagem != "")
                {
                    dataBytes = System.IO.File.ReadAllBytes(projeto.Imagem);

                    projeto.ArquivoImagem = File(dataBytes, "image/" + Path.GetExtension(projeto.Imagem).Replace('.'.ToString(), ""));
                }
            }

            //!!! Verificar como retonar NÃO OK

            return Ok(projetos);
        }
        /// <summary>
        /// Exclusão de projeto.
        /// </summary>
        /// <remarks>
        /// Realiza a exclusão de um projeto por idProjeto.
        /// <br/><br/>
        /// Dados de entrada: idProjeto
        /// <br/><br/>
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Projeto excluído com sucesso.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _projetoRepository.Delete(id);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }
    }
}
