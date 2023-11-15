using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models.Domain;

namespace server.Data
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto?> Get(int id);
        Task<UserDto> Add(UserDto User);
        Task<UserDto> Update(UserDto User);
        Task Delete(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly OfficeDbContextLokal context;

        private static UserDto EntityToDetailDto(User e)
        {
            return new UserDto(e.Id, e.Name, e.Email, e.PhoneNumber);
        }

        private static void DtoToEntity(UserDto dto, User e)
        {
            e.Id = dto.Id;
            e.Name = dto.Name;
            e.Email = dto.Email;
            e.PhoneNumber = dto.PhoneNumber;
  
        }

        public UserRepository(OfficeDbContextLokal context)
        {
            this.context = context;
        }

        public async Task<List<UserDto>> GetAll()
        {
            return await context.Users.Select(e => new UserDto(e.Id, e.Name,e.Email, e.PhoneNumber)).ToListAsync();
        }

        public async Task<UserDto?> Get(int id)
        {
            var entity = await context.Users.SingleOrDefaultAsync(h => h.Id == id);
            if (entity == null)
                return null;
            return EntityToDetailDto(entity);
        }

        public async Task<UserDto> Add(UserDto dto)
        {
            var entity = new User();
            DtoToEntity(dto, entity);
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            var entity = await context.Users.FindAsync(dto.Id);
            if (entity == null)
                throw new ArgumentException($"Trying to update User: entity with ID {dto.Id} not found.");
            DtoToEntity(dto, entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await context.Users.FindAsync(id);
            if (entity == null)
                throw new ArgumentException($"Trying to delete User: entity with ID {id} not found.");
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
