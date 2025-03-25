using Domain.Entities;

namespace Aplication.Interfaces.Offers
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetOffersByPostId(int idPost);
        Task<Offer?> GetByIdAsync(int id);
        Task AddAsync(Offer offer);
        Task<bool> UpdateAsync(Offer offer);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Offer>> QuantityOffersAcceptedByUserAsync(int idUser);
        Task<bool> UpdateOfferStateAsync(int id, bool isAccepted);
        Task<IEnumerable<Offer>> GetOffersAcceptedByUserAsync(int idUser);
    }
}
