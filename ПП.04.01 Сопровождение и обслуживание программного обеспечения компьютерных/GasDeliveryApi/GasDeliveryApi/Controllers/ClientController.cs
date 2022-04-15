using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasDeliveryApi.Models;
using GasDeliveryApi.Models.Views;

namespace GasDeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Список пар: номер телефона + код авторизации, для верефикации телефона
        /// </summary>        
        private static List<PhoneValid> phones = new List<PhoneValid>();

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private GasDeliveryDBContext _context;

        /// <summary>
        /// Инициализация контекста данных
        /// </summary>
        /// <param name="context">контекст данных</param>
        public ClientController(GasDeliveryDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод, генерирующий код авторизации (в дальнейшем нужно код отправлять на номер телефона)
        /// </summary>
        /// <param name="phoneValid">Объект с номером телефона</param>
        /// <returns>Возвращает сгенерированный код, или http-код 404, если аккаунт с таким номером уже есть</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult<int> GetCode(PhoneValid phoneValid)
        {
            if (_context.Users.Where(c => c.Phone == phoneValid.Phone).FirstOrDefault() != null)
            {
                return NotFound();
            }

            var result = phones.Where(p => p.Phone == phoneValid.Phone).FirstOrDefault();
            if (result != null)
            {
                phones.Remove(result);
            }

            int code = new Random().Next(100000, 999999);

            phones.Add(new PhoneValid()
            {
                Phone = phoneValid.Phone,
                Code = code
            });

            return new ObjectResult(code);
        }

        /// <summary>
        /// Метод, проверяющий корректность ранее отправленного кода
        /// </summary>
        /// <param name="phoneValid">Объект с номером телефона и кодом авторизации</param>
        /// <returns>Возвращает http-код 200 или 404, в зависимости от успешности проверки кода</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult CheckCode(PhoneValid phoneValid)
        {
            var result = phones.Where(p => p.Phone == phoneValid.Phone && p.Code == phoneValid.Code).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            phones.Remove(result);
            return Ok();
        }
        
        /// <summary>
        /// Метод регистрации нового клиента
        /// </summary>
        /// <param name="clientView">Объект, содержащий все данные, необходимые для регистрации</param>
        /// <returns>Возвращает http-код 200, 404 или 500, в зависимости от выполнения регистрации</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult RegClient(ClientView clientView)
        {
            if (_context.Users.Where(c => c.Phone == clientView.Phone).FirstOrDefault() != null)
            {
                return NotFound();
            }

            try
            {
                PersonalInfo personalInfo = new PersonalInfo()
                {
                    LastName = clientView.LastName,
                    FirstName = clientView.FirstName,
                    Patronymic = clientView.Patronymic
                };
                _context.PersonalInfos.Add(personalInfo);

                User user = new User()
                {
                    Phone = clientView.Phone,
                    RoleId = _context.UserRoles.Where(u => u.Name == "Клиент").Select(u => u.Id).FirstOrDefault()
                };
                _context.Users.Add(user);

                Client client = new Client()
                {
                    PersonalInfo = personalInfo,
                    User = user
                };
                _context.Clients.Add(client);

                _context.SaveChanges();

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Тестовый метод, выводящий список пар: номер телефона + код авторизации
        /// </summary>
        /// <returns>Список пар: номер телефона + код авторизации</returns>
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<PhoneValid> GetPhones()
        {
            return phones;
        }
    }
}
