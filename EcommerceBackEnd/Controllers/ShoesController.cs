using AutoMapper;
using EcommerceBackEnd.DTOs;
using EcommerceBackEnd.Entity;
using EcommerceBackEnd.Helpers;
using EcommerceBackEnd.Services;
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
        private readonly IFileStorage fileStorage;
        // folder name to store on Azure MUST be LOWERCASE
        private readonly string container = "shoesfolder";

        // We inyect context,mapper, IfileStore to the constructor
        public ShoesController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShoeDTO>>> Get([FromQuery] PaginationDTO  paginationDTO)
        {
            // queryable to pass it to the Httpcontext extension
            var queryable = context.ShoesDBTable.AsQueryable();
            // call the extension method
            await HttpContext.InsertPaginationParams(queryable, paginationDTO.AmountRecordsPerPage);

            // without Pagination
            // var entities = await context.ShoesDBTable.ToListAsync();

            // with pagination
            var entities = await queryable.Paginate(paginationDTO).ToListAsync();
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
        public async Task<ActionResult> Post([FromForm] ShoeCreationDTO shoeCreationDTO)
        {
            var entity = mapper.Map<ShoesEntity>(shoeCreationDTO);

            if (shoeCreationDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // extract a byte array from Iformfile
                    await shoeCreationDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(shoeCreationDTO.Photo.FileName);
                    entity.Photo = await fileStorage.SaveFile(content, extension, container, shoeCreationDTO.Photo.ContentType);
                }
            }

            context.Add(entity);
            await context.SaveChangesAsync();
            var shoeDTO = mapper.Map<ShoeDTO>(entity);

            return new CreatedAtRouteResult("getShoeRoute", new { id = shoeDTO.Id }, shoeDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ShoeCreationDTO shoeCreationDTO)
        {
            //this was my original code to Put the shoe, when there was no image
            //var entity = mapper.Map<ShoesEntity>(shoeCreationDTO);
            //entity.Id = id;
            //context.Entry(entity).State = EntityState.Modified;

            var shoeDB = await context.ShoesDBTable.FirstOrDefaultAsync(shoe => shoe.Id == id);

            if (shoeDB == null) return NotFound();

            shoeDB = mapper.Map(shoeCreationDTO, shoeDB);

            if (shoeCreationDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // extract a byte array from Iformfile
                    await shoeCreationDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(shoeCreationDTO.Photo.FileName);
                    shoeDB.Photo = await fileStorage.EditFile(content, extension, container, shoeDB.Photo, shoeCreationDTO.Photo.ContentType);

                    await Console.Out.WriteLineAsync(shoeDB.Photo);
                }
            }

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
