using AutoMapper;
using EcommerceBackEnd.DTOs;
using EcommerceBackEnd.Entity;
using EcommerceBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        // We inyect context,mapper, IfileStore to the constructor
        public OrdersController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrdersDTO>>> Get()
        {
            var orders = await context.OrdersDBTable.ToListAsync();
            return mapper.Map<List<OrdersDTO>>(orders);
        }

        [HttpGet ("{id}", Name = "getOrder")]
        public async Task<ActionResult<OrdersDTO>> Get(int id)
        {
            var order = await context.OrdersDBTable.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) { return NotFound(); }

            return mapper.Map<OrdersDTO>(order);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderCreationDTO orderCreationDTO)
        {
            var orderEntity = mapper.Map<OrdersEntity>(orderCreationDTO);
            orderEntity.CreationDate = DateTime.UtcNow; 

            context.Add(orderEntity);
            await context.SaveChangesAsync();

            var orderDTO = mapper.Map<OrdersDTO>(orderEntity);

            return new CreatedAtRouteResult("getOrder", new { id = orderDTO.Id }, orderDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OrderCreationDTO orderCreationDTO)
        {
            var orderEntity = await context.OrdersDBTable.FirstOrDefaultAsync(order => order.Id == id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            orderEntity = mapper.Map(orderCreationDTO, orderEntity);
            context.Entry(orderEntity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await context.OrdersDBTable.FirstOrDefaultAsync(order => order.Id == id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            context.OrdersDBTable.Remove(orderEntity);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
