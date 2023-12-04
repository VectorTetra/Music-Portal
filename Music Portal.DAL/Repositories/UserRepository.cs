using Microsoft.EntityFrameworkCore;
using Music_Portal.DAL.EF;
using Music_Portal.DAL.Entities;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly MusicPortalContext _context;
        public UserRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> Get(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user=> user.Id == id);
        }
        public async Task<IEnumerable<User>> GetByLogin(string login)
        {
            return await _context.Users.Where(o=> o.Login.Contains(login)).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetNotAuthorizedUsers()
        {
            return await _context.Users.Where(o => o.Role.Id == 0).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAuthorizedUsers()
        {
            return await _context.Users.Where(o => o.Role.Id == 1).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetBlockedUsers()
        {
            return await _context.Users.Where(o => o.Role.Id == -1).ToListAsync();
        }
        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user != null)
                _context.Users.Remove(user);
        }
    }
}
