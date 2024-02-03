using APISquad7.Infraestrutura;
using APISquad7.Model;
using APISquad7.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace APISquad7.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository, IProjetoRepository projetoRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _projetoRepository = projetoRepository ?? throw new ArgumentNullException(nameof(projetoRepository));
        }

        /* Parâmetro usuarioView contém os dados que vieram do json da requisição http */
        /// <summary>
        /// Inclusão de usuário.
        /// </summary>
        /// <remarks>
        /// Realiza o cadastro de um usuário.
        /// <br/><br/>
        /// Dados de entrada: nome, sobrenome, email e senha.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Usuário cadastrado com sucesso.</response>
        /// <response code="409">Violação de chave única. Email já cadastrado.</response>
        /// <response code="500">Falha não tratada.</response>
        [HttpPost]
        public IActionResult Add(UsuarioViewModel usuarioView)
        {
            var usuario = new Usuario(usuarioView.Nome, usuarioView.Sobrenome, usuarioView.Email, usuarioView.Senha);

            int result = _usuarioRepository.Add(usuario);

            switch (result)
            {
                case 0:
                    return Ok("Inclusão realizada com sucesso!");
                    break;
                case 1:
                    return StatusCode(500, "Inclusão falhou.");
                    break;
                case 2:
                    return Conflict("Violação de chave única.");
                    break;
                default:
                    return StatusCode(500, "Falha não tratada.");
                    break;
            }
        }

        /// <summary>
        /// Busca de usuários cadastrados.
        /// </summary>
        /// <remarks>
        /// Realiza a busca de todos usuários cadastrados.
        /// <br/><br/>
        /// Dados de saída: idUsuario, nome, sobrenome e email.
        /// </remarks>
        /// <param name="usuarioView"></param>
        /// <returns></returns>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="500">Falha não tratada.</response>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _usuarioRepository.Get();

                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Obtenção falhou.");
            }
        }
        /// <summary>
        /// Validação de login do usuario.
        /// </summary>
        /// <remarks>
        /// Verifica se o usário tem permissão de acesso, através do email e senha.
        /// <br/><br/>
        /// Dados de entrada: email e senha.
        /// <br/><br/>
        /// Dados de saída: 
        /// <br/>
        /// Se usuário tem permissão, retorna os dados: idUsuario, nome, sobrenome e email.  
        /// Se usuário não tem permissão, retorna idUsuario = -1.
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        /// <response code="200">Validação realizada com sucesso.</response>
        /// <response code="500">Falha não tratada.</response>
        [HttpGet("validarLogin")]
        public IActionResult ValidarLogin([FromQuery] string email, [FromQuery] string senha)
        {
            try
            {
                var usuario = _usuarioRepository.GetByEmailSenha(email.ToLower(), senha);

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Validação do login falhou.");
            }
        }
        /// <summary>
        /// Busca de usuários cadastrados por idUsuario.
        /// </summary>
        /// <remarks>
        /// Realiza a busca de usuários cadastrados através do idUsuario.
        /// <br/><br/>
        /// Dados de entrada: idUsuario.
        /// <br/><br/>
        /// Dados de saída: idUsuario, nome, sobrenome e email.
        /// </remarks>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="500">Falha não tratada.</response>
        [HttpGet("getByIdUsuario")]
        public IActionResult GetByIdUsuario([FromQuery] int idUsuario)
        {
            try
            {
                var usuario = _usuarioRepository.GetByIdUsuario(idUsuario);

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Obtenção falhou.");
            }
        }
        /// <summary>
        /// Busca de usuario e seus projetos cadastrados pelo idUsuario.
        /// </summary>
        /// <remarks>
        /// Realiza a busca do usuário e seus projetos cadastrados através do idUsuario.
        /// <br/><br/>
        /// Dados de entrada: idUsuario.
        /// <br/><br/>
        /// Dados de saída: idUsuario, nome, sobrenome e email e lista de projetos: idProjeto, titulo, imagem, tag, link, descrição e dataCriacao e arquivoImagem.
        /// </remarks>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        /// <response code="200">Busca realizada com sucesso.</response>
        /// <response code="500">Falha não tratada.</response>
        [HttpGet("getUsuarioProjetoByIdUsuario")]
        public ActionResult<Usuario> GetUsuarioProjetoByIdUsuario([FromQuery] int idUsuario)
        {
            try
            {
                var usuario = _usuarioRepository.GetByIdUsuario(idUsuario);

                usuario.lstProjeto = _projetoRepository.GetByIdUsuario(idUsuario);

                byte[] dataBytes;

                foreach (Projeto projeto in usuario.lstProjeto)
                {
                    if (projeto.Imagem != "")
                    {
                        dataBytes = System.IO.File.ReadAllBytes(projeto.Imagem);

                        projeto.ArquivoImagem = File(dataBytes, "image/" + Path.GetExtension(projeto.Imagem).Replace('.'.ToString(), ""));
                    }
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Obtenção falhou.");
            }
        }
    }
}
