using APISquad7.Infraestrutura;
using APISquad7.Model;
using APISquad7.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace APISquad7.Controllers
{
    /* Classe que recebe as requisições HTTP do usuário */
    
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
            var usuario = new Usuario(usuarioView.Nome, usuarioView.Sobrenome, usuarioView.Email.ToLower(), usuarioView.Senha);

            _usuarioRepository.Add(usuario);

            //!!! Verificar como retonar NÃO OK

            return Ok();
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuarioRepository.Get();

            //!!! Verificar como retonar NÃO OK

            return Ok(usuarios);
        }

        [HttpGet("validarLogin")]
        public IActionResult ValidarLogin([FromQuery] string email, [FromQuery] string senha)
        {
            var idUsuario = _usuarioRepository.GetByEmailSenha(email.ToLower(), senha);

            //!!! Verificar como retonar NÃO OK

            return Ok(idUsuario);
        }

        [HttpGet("getUsuarioProjetoByIdUsuario")]
        public IActionResult GetUsuarioProjetoByIdUsuario([FromQuery] int idUsuario)
        {
            var usuario = _usuarioRepository.GetByIdUsuario(idUsuario);

            usuario.lstProjeto = _projetoRepository.GetByIdUsuario(idUsuario);

            //!!! Verificar como retonar NÃO OK

            return Ok(usuario);
        }
    }
}
