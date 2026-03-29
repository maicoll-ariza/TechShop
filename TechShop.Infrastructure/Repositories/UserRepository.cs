
using TechShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TechShop.Application.Interfaces;
using TechShop.Infrastructure.Persistence;

namespace TechShop.Infrastructure.Repositories;
public class UserRepository(AppDbContext appDbContext) : IUserRepository
{

    private readonly AppDbContext _context = appDbContext;

    public async Task<User?> GetByIdAsync(int id)
    {
     return await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);
    } 

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}