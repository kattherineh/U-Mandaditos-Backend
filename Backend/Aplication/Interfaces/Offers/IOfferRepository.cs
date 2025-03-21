using Domain.Entities;

namespace Aplication.Interfaces.Offers
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task AddAsync(Offer offer);
        Task<bool> UpdateAsync(Offer offer);
        Task<bool> DeleteAsync(int id);
    }
}
