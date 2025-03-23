using Domain.Entities;


namespace Application.Interfaces {
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message?> GetByIdAsync(int id);
        Task AddAsync(Message message);
        Task<bool> UpdateAsync(Message message);
        Task<bool> DeleteAsync(int id);
    }
}