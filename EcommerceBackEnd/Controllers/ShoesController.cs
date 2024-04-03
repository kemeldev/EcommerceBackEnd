using AutoMapper;
using EcommerceBackEnd.DTOs;
using EcommerceBackEnd.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd.Controllers
{
    [ApiController]
    [Route("api/shoes")]
    public class ShoesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        // We inyected the context and mapper to the constructor
        public ShoesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShoeDTO>>> Get()
        {
            var entities = await context.ShoesDBTable.ToListAsync();
            var dtos = mapper.Map<List<ShoeDTO>>(entities);

            return dtos;

        }

        [HttpGet("{id:int}", Name = "getShoeRoute")]
        public async Task<ActionResult<ShoeDTO>> Get(int id)
        {
            var entity = await context.ShoesDBTable.FirstOrDefaultAsync(shoe => shoe.Id == id);
            if (entity == null) { return NotFound(); }

            var dto = mapper.Map<ShoeDTO>(entity);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShoeCreationDTO shoeCreationDTO)
        {
            var entity = mapper.Map<ShoesEntity>(shoeCreationDTO);
            context.Add(entity);
            await context.SaveChangesAsync();
            var shoeDTO = mapper.Map<ShoeDTO>(entity);

            return new CreatedAtRouteResult("getShoeRoute", new { id = shoeDTO.Id }, shoeDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShoeCreationDTO shoeCreationDTO)
        {
            var entity = mapper.Map<ShoesEntity>(shoeCreationDTO);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.ShoesDBTable.AnyAsync(shoe => shoe.Id == id);
            if (!exist) return NotFound();

            context.Remove(new ShoesEntity() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
