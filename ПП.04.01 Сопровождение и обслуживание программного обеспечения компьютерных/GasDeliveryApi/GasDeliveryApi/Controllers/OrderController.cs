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
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private GasDeliveryDBContext _context;

        /// <summary>
        /// Инициализация контекста данных
        /// </summary>
        /// <param name="context">контекст данных</param>
        public OrderController(GasDeliveryDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Регистрация нового заказа
        /// </summary>
        /// <param name="orderView">Объект, содержащий все данные, необходимые для регистрации</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult RegOrder(OrderView orderView)
        {
            try
            {
                var address = _context.AddressDeliveries.Where(a => a.Address == orderView.Address.Address &&
                                                     a.ApartmentNum == orderView.Address.ApartmentNum &&
                                                     a.FloorNum == orderView.Address.FloorNum &&
                                                     a.FrontDoorNum == orderView.Address.FrontDoorNum).FirstOrDefault();
                if (address == null)
                {
                    _context.AddressDeliveries.Add(orderView.Address);
                }

                var client = _context.Clients.Where(c => c.User.Phone == orderView.ClientPhone).FirstOrDefault();
                if (client == null)
                {
                    return StatusCode(500);
                }

                Ordered order = new Ordered()
                {
                    Address = orderView.Address,
                    DateDelivery = DateTime.Parse(orderView.DateDelivery),
                    DesiredTimeFrom = TimeSpan.Parse(orderView.DesiredTimeFrom),
                    DesiredTimeTo = TimeSpan.Parse(orderView.DesiredTimeTo),
                    Sum = 0.0,
                    Client = client,
                    Status = _context.OrderStatuses.Where(s => s.Name == "Обрабатывается").FirstOrDefault()
                };
                _context.Ordereds.Add(order);

                double allSumm = 0.0;
                foreach (var i in orderView.Products)
                {
                    OrderCompo orderCompos = new OrderCompo()
                    {
                        Order = order,
                        ProductId = i.Id,
                        Quantity = i.Quantity,
                        Sum = _context.Products.Where(p => p.Id == i.Id).Select(p => p.Price).FirstOrDefault() * i.Quantity
                    };
                    _context.OrderCompos.Add(orderCompos);
                    allSumm += orderCompos.Sum;
                }
                order.Sum = allSumm;

                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<OrderView> GetFreeOrders()
        {
            var result = (from order in _context.Ordereds
                          where order.Status == _context.OrderStatuses.Where(s => s.Name == "Обрабатывается").FirstOrDefault()
                          select new OrderView()
                          {
                              Id = order.Id,
                              Address = order.Address,
                              ClientLastName = order.Client.PersonalInfo.LastName,
                              ClientFirstName = order.Client.PersonalInfo.FirstName,
                              ClientPhone = order.Client.User.Phone,
                              DateDelivery = order.DateDelivery.ToString("dd.MM.yyyy"),
                              DesiredTimeFrom = order.DesiredTimeFrom.ToString(),
                              DesiredTimeTo = order.DesiredTimeTo.ToString(),
                              Summ = order.Sum,
                              OrderStatus = order.Status.Name,
                              Products = (from orderCompos in _context.OrderCompos
                                          where orderCompos.OrderId == order.Id
                                          select new OrderProductView()
                                          {
                                              Id = orderCompos.Product.Id,
                                              Name = orderCompos.Product.Name,
                                              Quantity = orderCompos.Quantity,
                                              Summ = orderCompos.Sum
                                          }).ToList()
                          }).ToList();
            return new ObjectResult(result);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Ordered> SetDriver(DriverOrderView driverOrder)
        {
            var orderDb = (from order in _context.Ordereds
                            where order.Id == driverOrder.OrderId
                            select order).FirstOrDefault();

            var driverDb = (from driver in _context.Drivers
                            where driver.User.Phone == driverOrder.DriverPhone
                            select driver).FirstOrDefault();

            orderDb.DriverId = driverDb.Id;
            orderDb.ExactTime = TimeSpan.Parse(driverOrder.ExactTime);
            orderDb.StatusId = _context.OrderStatuses.Where(s => s.Name == "Принят в работу").Select(s => s.Id).FirstOrDefault();
            _context.Ordereds.Update(orderDb);
            _context.SaveChanges();

            var result = (from order in _context.Ordereds
                            where order.Id == driverOrder.OrderId
                            select order).FirstOrDefault();

            return new ObjectResult(result);       
        }
    }
}
