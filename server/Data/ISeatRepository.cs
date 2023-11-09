using Microsoft.EntityFrameworkCore;
using server.Context;

namespace server.Data
{
    public interface ISeatRepository
    {
        Task<List<SeatDto>> GetAll();
        Task<SeatDetailDto?> Get(int id);
        Task<SeatDetailDto> Add(SeatDetailDto Seat);
        Task<SeatDetailDto> Update(SeatDetailDto Seat);
        Task Delete(int id);
    }

    public class SeatRepository : ISeatRepository
    {
        private readonly OfficeDbContext context;

        private static SeatDetailDto EntityToDetailDto(SeatEntity e)
        {
            return new SeatDetailDto(e.Id, e.Room, e.Country, e.Description, e.Price, e.Photo);
        }

        private static void DtoToEntity(SeatDetailDto dto, SeatEntity e)
        {
            e.Room = dto.Room;
            e.Country = dto.Country;
            e.Description = dto.Description;
            e.Price = dto.Price;
            e.Photo = dto.Photo;
        }

        public SeatRepository(OfficeDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SeatDto>> GetAll()
        {
            return await context.Seats.Select(e => new SeatDto(e.Id, e.Room, e.Country, e.Price)).ToListAsync();
        }

        public async Task<SeatDetailDto?> Get(int id)
        {
            var entity = await context.Seats.SingleOrDefaultAsync(h => h.Id == id);
            if (entity == null)
                return null;
            return EntityToDetailDto(entity);
        }

        public async Task<SeatDetailDto> Add(SeatDetailDto dto)
        {
            var entity = new SeatEntity();
            DtoToEntity(dto, entity);
            context.Seats.Add(entity);
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        public async Task<SeatDetailDto> Update(SeatDetailDto dto)
        {
            var entity = await context.Seats.FindAsync(dto.Id);
            if (entity == null)
                throw new ArgumentException($"Trying to update Seat: entity with ID {dto.Id} not found.");
            DtoToEntity(dto, entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await context.Seats.FindAsync(id);
            if (entity == null)
                throw new ArgumentException($"Trying to delete Seat: entity with ID {id} not found.");
            context.Seats.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
