using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;

namespace server.Data
{
    public interface ISeatRepository
    {
        Task<List<SeatDetailDto>> GetAll();
        Task<SeatDetailDto?> Get(int id);
        Task<SeatDto> Add(SeatDetailDto Seat);
        Task<SeatDto> Update(SeatDetailDto Seat);
        Task Delete(int id);
    }

    public class SeatRepository : ISeatRepository
    {
        private readonly OfficeDbContextLokal context;

        private static SeatDetailDto EntityToDetailDto(Seat e)
        {
            return new SeatDetailDto(e.Id, e.Room.Name, e.RoomId);
        }

        private static SeatDto EntityToDto(Seat e)
        {
            return new SeatDto(e.Id, e.RoomId);
        }

        private static void DtoToEntity(SeatDetailDto dto, Seat e)
        {
            e.RoomId = dto.RoomId;
        }

        public SeatRepository(OfficeDbContextLokal context)
        {
            this.context = context;
        }

        public async Task<List<SeatDetailDto>> GetAll()
        {
            return await context.Seats.Select(e => new SeatDetailDto(e.Id, e.Room.Name, e.RoomId)).ToListAsync();
        }

        public async Task<SeatDetailDto?> Get(int id)
        {
            var dto = await context.Seats.Where(h => h.Id == id)
                .Select(e => new SeatDetailDto(e.Id, e.Room.Name, e.RoomId)).SingleOrDefaultAsync();
            if (dto == null)
                return null;
            return dto;
        }

        public async Task<SeatDto> Add(SeatDetailDto dto)
        {
            var entity = new Seat();
            DtoToEntity(dto, entity);
            context.Seats.Add(entity);
            await context.SaveChangesAsync();
            return EntityToDto(entity);
        }

        public async Task<SeatDto> Update(SeatDetailDto dto)
        {
            var entity = await context.Seats.FindAsync(dto.Id);
            if (entity == null)
                throw new ArgumentException($"Trying to update Seat: entity with ID {dto.Id} not found.");
            DtoToEntity(dto, entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return EntityToDto(entity);
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
