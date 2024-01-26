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

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
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
            var qtd = _usuarioRepository.CountByEmailSenha(email.ToLower(), senha);

            bool retorno = false;

            if (qtd > 0)
            {
                retorno = true;
            }

            //!!! Verificar como retonar NÃO OK

            return Ok(retorno);
        }
    }
}
