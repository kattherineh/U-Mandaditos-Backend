using Aplication.DTOs.Locations;
using Aplication.DTOs.Mandaditos;
using Aplication.DTOs.Media;
using Aplication.DTOs.Offers;
using Aplication.DTOs.Posts;
using Aplication.DTOs.Users;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Mandaditos;
using Domain.Entities;

namespace Aplication.Services;

public class MandaditoService : IMandaditoService
{
    private readonly IMandaditoRepository _mandaditoRepository;
    private readonly ICodeGeneratorService _codeGeneratorService;

    public MandaditoService(IMandaditoRepository mandaditoRepository, ICodeGeneratorService codeGeneratorService)
    {
        _mandaditoRepository = mandaditoRepository;
        _codeGeneratorService = codeGeneratorService;
    }

    public async Task<MandaditoResponseDTO?> GetByIdAsync(int id)
    {
        var mandadito = await _mandaditoRepository.GetByIdAsync(id);
        return mandadito is null
            ? null
            : new MandaditoResponseDTO
            {
                Id = mandadito.Id,
                SecurityCode = mandadito.SecurityCode,
                AcceptedAt = mandadito.AcceptedAt,
                AcceptedRate = mandadito.AcceptedRate,
                Offer = mandadito.Offer is null
                    ? null
                    : new OfferDTO
                    {
                        Id = mandadito.Offer.Id,
                        CounterOfferAmount = mandadito.Offer.CounterOfferAmount,
                        IdPost = mandadito.Offer.IdPost,
                        UserCreator = mandadito.Offer.UserCreator is null
                            ? null
                            : new UserResponseMandaditoDTO
                            {
                                Id = mandadito.Offer.UserCreator.Id,
                                Name = mandadito.Offer.UserCreator.Name,
                                LastLocation = mandadito.Offer.UserCreator.LastLocation?.Name ,
                                ProfilePicture = mandadito.Offer.UserCreator.ProfilePic?.Link
                            },
                        CreatedAt = mandadito.Offer.CreatedAt,
                        IsCounterOffer = mandadito.Offer.IsCounterOffer,
                        Accepted = mandadito.Offer.Accepted
                    },
                Post = mandadito.Post is null
                    ? null
                    : new PostMandaditoDTO
                    {
                        Id = mandadito.Post.Id,
                        SuggestedValue = mandadito.Post.SugestedValue,
                        Description = mandadito.Post.Description,
                        CreatedAt = mandadito.Post.CreatedAt,
                        PosterUser = new UserResponseMandaditoDTO
                        {
                            Id = mandadito.Post.PosterUser.Id,
                            Name = mandadito.Post.PosterUser.Name,
                            LastLocation = mandadito.Post.PosterUser.LastLocation?.Name,
                            ProfilePicture = mandadito.Post.PosterUser.ProfilePic?.Link
                        },
                        PickupLocation = mandadito.Post.PickUpLocation.Name,
                        DeliveryLocation = mandadito.Post.DeliveryLocation.Name
                    }
            };
    }

    public async Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId)
    {
        var mandaditosByUser = (await _mandaditoRepository.GetAllAsync())
            .Where(mandadito => mandadito.Post != null && mandadito.Post.PosterUser.Id == userId && mandadito.Post.Completed)
            .OrderByDescending(mandadito => mandadito.DeliveredAt);

        return mandaditosByUser.Select(man => new MandaditoHistoryResponseDTO
        {
            Id = man.Id,
            PickupLocationName = man.Post?.PickUpLocation.Name,
            DeliveryLocationName = man.Post?.DeliveryLocation.Name,
            Title = man.Post?.Description,
            RunnerName = man.Offer?.UserCreator?.Name,
            DeliveredAt = man.DeliveredAt
        });
    }

    public async Task<MandaditoResponseMinDTO?> CreateAsync(MandaditoRequestDTO dto)
    {
        string securityCode = _codeGeneratorService.GenerateMandaditoCode(6);
        var mandadito = new Mandadito(
            securityCode: securityCode,
            acceptedRate: dto.AcceptedRate,
            idPost: dto.PostId,
            idOffer: dto.OfferId,
            acceptedAt: DateTime.Now);

        await _mandaditoRepository.AddAsync(mandadito);

        return new MandaditoResponseMinDTO
        {
            Id = mandadito.Id,
            SecurityCode = mandadito.SecurityCode,
            AcceptedAt = mandadito.AcceptedAt,
            AcceptedRate = mandadito.AcceptedRate,
            OfferId = mandadito.IdOffer,
            PostId = mandadito.IdPost
        };
    }
}