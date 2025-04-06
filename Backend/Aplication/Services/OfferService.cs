using Aplication.Interfaces;
using Aplication.Interfaces.Offers;
using Aplication.DTOs;
using Domain.Entities;
using Aplication.DTOs.General;


namespace Aplication.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<OfferResponseDTO?> GetOfferByIdAsync(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);

            if (offer is null)
            {
                return null;
            }

            return new OfferResponseDTO
            {
                Id = offer.Id,
                CounterOfferAmount = offer.CounterOfferAmount,
                UserCreator = offer.UserCreator,
                Post = offer.Post,
                IsCounterOffer = offer.IsCounterOffer,
                CreatedAt = offer.CreatedAt
            };
        }

        public async Task<ResponseDTO<IEnumerable<OfferResponseDTO>>> GetOffersByPostIdAsync(int idPost)
        {
            var offers = await _offerRepository.GetOffersByPostId(idPost);

            if (offers is null || !offers.Any())
            {
                return new ResponseDTO<IEnumerable<OfferResponseDTO>>
                {
                    Success = false,
                    Message = "No offers found for this post.",
                    Data = Enumerable.Empty<OfferResponseDTO>()
                };
            }

            return new ResponseDTO<IEnumerable<OfferResponseDTO>>
            {
                Success = true,
                Message = "Offers retrieved successfully.",
                Data = offers.Select(offer => new OfferResponseDTO
                {
                Id = offer.Id,
                CounterOfferAmount = offer.CounterOfferAmount,
                UserCreator = offer.UserCreator,
                Post = offer.Post,
                IsCounterOffer = offer.IsCounterOffer,
                CreatedAt = offer.CreatedAt
                })
            };
        }

        public async Task<OfferResponseDTO> CreateOfferAsync(OfferRequestDTO offerRequest)
        {
            var offer = new Offer
            {
                CounterOfferAmount = offerRequest.CounterOfferAmount,
                IdUserCreator = offerRequest.UserCreatorId,
                IdPost = offerRequest.PostId,
                IsCounterOffer = offerRequest.IsCounterOffer,
                CreatedAt = offerRequest.CreatedAt
            };

            await _offerRepository.AddAsync(offer);

            return new OfferResponseDTO
            {
                Id = offer.Id,
                CounterOfferAmount = offer.CounterOfferAmount,
                UserCreator = offer.UserCreator,
                Post = offer.Post,
                IsCounterOffer = offer.IsCounterOffer,
                CreatedAt = offer.CreatedAt
            };

        }

        public async Task<OfferResponseDTO?> UpdateOfferStateAsync(int id, bool state)
        {
            var offer = await _offerRepository.GetByIdAsync(id);

            if (offer is null)
            {
                return null;
            }

            offer.Accepted = state;

            await _offerRepository.UpdateAsync(offer);

            return new OfferResponseDTO
            {
                Id = offer.Id,
                CounterOfferAmount = offer.CounterOfferAmount,
                UserCreator = offer.UserCreator,
                Post = offer.Post,
                IsCounterOffer = offer.IsCounterOffer,
                CreatedAt = offer.CreatedAt
            };
        }

        public async Task<int> QuantityOffersAcceptedByUserAsync(int userId)
        {
            var offers = await _offerRepository.QuantityOffersAcceptedByUserAsync(userId);

            return offers.Count();
        }

        public async Task<bool> DeleteOfferAsync(int id)
        {
            var offer = _offerRepository.GetByIdAsync(id);

            if (offer is null)
            {
                return false;
            }

            return await _offerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OfferHistoryResponseDTO>> GetOffersAcceptedByUserAsync(int idUser)
        {
            var offers = await _offerRepository.GetOffersAcceptedByUserAsync(idUser);

            return offers.Select(offer => new OfferHistoryResponseDTO
            {
                PickUpLocation = offer.Post?.PickUpLocation?.Name ?? string.Empty,
                DeliveryLocation = offer.Post?.DeliveryLocation?.Name ?? string.Empty,
                RunnerName = offer.UserCreator?.Name ?? string.Empty
            });

        }
    }
}