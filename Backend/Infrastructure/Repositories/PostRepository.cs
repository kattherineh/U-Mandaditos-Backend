using Aplication.Interfaces.Posts;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository: IPostRepository
{
    private BackendDbContext _context;
    
    public PostRepository(BackendDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.PosterUser) 
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation) 
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.PosterUser) // Incluir usuario
            .Include(p => p.PickUpLocation) // Incluir ubicación de recogida
            .Include(p => p.DeliveryLocation) // Incluir ubicación de entrega
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task AddAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post is null) return false;
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }
    
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000; 
        double dLat = (lat2 - lat1) * (Math.PI / 180);
        double dLon = (lon2 - lon1) * (Math.PI / 180);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c; 
    }


    public async Task<IEnumerable<Post>> GetPostsNearLocationAsync(int idLocation, double radiusMeters = 70)
    {
        var referenceLocation = await _context.Locations.FindAsync(idLocation);
        if (referenceLocation == null)
            return new List<Post>(); 

        double lat = referenceLocation.Latitude;
        double lon = referenceLocation.Longitude;

        double degreeRadius = radiusMeters / 111320.0; 

        // Filtrar posts cercanos en base a la distancia
        var nearbyPosts = await _context.Posts
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation)
            .Where(p => 
                p.PickUpLocation != null &&
                p.DeliveryLocation != null &&
                CalculateDistance(lat, lon, p.PickUpLocation.Latitude, p.PickUpLocation.Longitude) <= radiusMeters ||
                CalculateDistance(lat, lon, p.DeliveryLocation.Latitude, p.DeliveryLocation.Longitude) <= radiusMeters
            )
            .ToListAsync();

        return nearbyPosts;
    }
    
    public async Task<IEnumerable<Post>> GetPostsByPosterUserId(int idPosterUser)
    {
        return await _context.Posts
            .Include(p => p.PosterUser)
            .Include(p => p.PosterUser!.ProfilePic) // Carga la relación ProfilePic
            .Include(p => p.PickUpLocation)
            .Include(p => p.DeliveryLocation)
            .Where(p => p.IdPosterUser == idPosterUser)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new Post 
            {
                Id = p.Id,
            
                PosterUser = p.PosterUser != null ? new User
                {
                    Name = p.PosterUser.Name,
                    LastLocation = p.PosterUser.LastLocation,
                    Rating = p.PosterUser.Rating,
                    ProfilePic = p.PosterUser.ProfilePic 
                } : null,
            
                PickUpLocation = p.PickUpLocation != null ? new Location
                {
                    Latitude = p.PickUpLocation.Latitude,
                    Longitude = p.PickUpLocation.Longitude,
                    Name = p.PickUpLocation.Name
                } : null,
            
                DeliveryLocation = p.DeliveryLocation != null ? new Location
                {
                    Latitude = p.DeliveryLocation.Latitude,
                    Longitude = p.DeliveryLocation.Longitude,
                    Name = p.DeliveryLocation.Name
                } : null,
            
                // Resto de propiedades
                CreatedAt = p.CreatedAt,
                // ...
            })
            .ToListAsync();
    }

}