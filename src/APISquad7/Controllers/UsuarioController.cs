using APISquad7.Infraestrutura;
using APISquad7.Model;
using APISquad7.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("getUsuarioProjetoByIdUsuario")]
        public IActionResult GetUsuarioProjetoByIdUsuario([FromQuery] int idUsuario)
        {
            try
            {
                var usuario = _usuarioRepository.GetByIdUsuario(idUsuario);

                usuario.lstProjeto = _projetoRepository.GetByIdUsuario(idUsuario);

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Obtenção falhou.");
            }
        }
    }
}
