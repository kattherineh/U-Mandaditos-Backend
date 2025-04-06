using Aplication.Interfaces;
using Aplication.Interfaces.Offers;
using Aplication.DTOs;
using Aplication.DTOs.General;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Posts;
using Domain.Entities;


namespace Aplication.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IPostRepository _postRepository;

        public OfferService(IOfferRepository offerRepository, IAuthenticatedUserService authenticatedUserService, IPostRepository postRepository)
        {
            _offerRepository = offerRepository;
            _authenticatedUserService = authenticatedUserService;
            _postRepository = postRepository;
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

        public async Task<IEnumerable<OfferResponseDTO>> GetOffersByPostIdAsync(int idPost)
        {
            var offers = await _offerRepository.GetOffersByPostId(idPost);

            return offers.Select(offer => new OfferResponseDTO
            {
                Id = offer.Id,
                CounterOfferAmount = offer.CounterOfferAmount,
                UserCreator = offer.UserCreator,
                Post = offer.Post,
                IsCounterOffer = offer.IsCounterOffer,
                CreatedAt = offer.CreatedAt
            });
        }

        public async Task<ResponseDTO<OfferResponseDTO>> CreateOfferAsync(OfferRequestDTO offerRequest)
        {
            var userId = _authenticatedUserService.GetAuthenticatedUserId();

            // Validar que el Post existe
            var post = await _postRepository.GetByIdAsync(offerRequest.PostId);
            if (post == null)
            {
                return new ResponseDTO<OfferResponseDTO>
                {
                    Success = false,
                    Message = $"No existe un post con el ID {offerRequest.PostId}",
                    Data = null
                };
            }

            // Crear y guardar la oferta
            var offer = new Offer
            {
                CounterOfferAmount = offerRequest.CounterOfferAmount,
                IdUserCreator = userId,
                IdPost = offerRequest.PostId,
                IsCounterOffer = offerRequest.IsCounterOffer,
                CreatedAt = DateTime.Now
            };

            await _offerRepository.AddAsync(offer);

            return new ResponseDTO<OfferResponseDTO>
                {
                    Success = true,
                    Message = "La oferta se realizo correctament",
                    Data = new OfferResponseDTO{
                        Id = offer.Id,
                        CounterOfferAmount = offer.CounterOfferAmount,
                        UserCreator = offer.UserCreator,
                        Post = offer.Post,
                        IsCounterOffer = offer.IsCounterOffer,
                        CreatedAt = offer.CreatedAt
                    }
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