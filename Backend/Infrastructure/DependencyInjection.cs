﻿using Aplication.Interfaces;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Locations;
using Aplication.Interfaces.Mandaditos;
using Aplication.Interfaces.Medias;
using Aplication.Interfaces.Offers;
using Aplication.Interfaces.Posts;
using Aplication.Interfaces.Users;
using Aplication.Interfaces.SessionLogs;
using Aplication.Services;
using Application.Interfaces;
using Infraestructure.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //Inyeccion de dependencia de Entidades
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();

        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMediaService, MediaService>();
        
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<ICareerService, CareerService>();

        services.AddScoped<IOfferRepository, OfferRepository>();
        
        services.AddScoped<IMandaditoRepository, MandaditoRepository>();
        services.AddScoped<IMandaditoService, MandaditoService>();
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();

        services.AddScoped<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>();

        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IRatingRepository, RatingRepository>();
        
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IManagementService, ManagementService>();
        services.AddScoped<IManagementRepository, ManagementRepository>();

        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IRatingRepository, RatingRepository>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISessionLogRepository, SessionLogRepository>();

        services.AddScoped<IAuthService, AuthService>();
        
        //Inyeccion de dependencia de Infraestructura
        services.AddHttpContextAccessor();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
        services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        
        return services;
    }
}