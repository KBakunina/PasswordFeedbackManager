using Microsoft.AspNetCore.Mvc;
using PasswordFeedbackManager.Models;
using PasswordFeedbackManager.Services;


namespace PasswordFeedbackManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordFeedbackController : ControllerBase
    {
        private readonly IPasswordFeedbackService _service;

        public PasswordFeedbackController(IPasswordFeedbackService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adding a new password-revocation combination / Добавление новой комбинации пароль-отзыв
        /// </summary>
        /// <param name="passwordFeedback"></param>
        /// <returns></returns>
        [HttpPost("addCombination")]
        public IActionResult AddCombination(PasswordFeedbackModel passwordFeedback)
        {
            return Ok(_service.AddCombination(passwordFeedback));
        }

        /// <summary>
        /// Getting feedback by password / Получение отзыва по паролю 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("getFeedback")]
        public IActionResult GetFeedback([FromQuery] string password)
        {
            var feedback = _service.GetFeedback(password);
            return feedback != null ? Ok(feedback) : NotFound();
        }

        /// <summary>
        /// Getting password by feedback / Получение паролю по отзыву
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpGet("getPasswordHash")]
        public IActionResult GetPasswordHash([FromQuery] string feedback)
        {
            var password = _service.GetPasswordHash(feedback);
            return password != null ? Ok(password) : NotFound();
        }

        /// <summary>
        /// Getting the number of combinations / Получение количества комбинаций
        /// </summary>
        /// <returns></returns>
        [HttpGet("getCount")]
        public IActionResult GetCombinationsCount()
        {
            return Ok(_service.GetCombinationsCount());
        }
    }
}