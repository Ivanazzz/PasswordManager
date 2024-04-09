using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PasswordManager.Models;
using PasswordManager.Models.CustomExceptions;
using PasswordManager.Models.Dtos.InfoDtos;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using PasswordManager.Repositories.Contracts;

namespace PasswordManager.Repositories.Services
{
    public class InfoRepository : IInfoRepository
    {
        private readonly PasswordManagerDbContext context;

        public InfoRepository(PasswordManagerDbContext context)
        {
            this.context = context;
        }

        // Ivana
        public async Task AddInfo(string email, InfoAddDto infoDto)
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            if (infoDto.Website.IsNullOrEmpty())
            {
                throw new BadRequestException("Въведете името на сайта");
            }

            if (infoDto.Password.IsNullOrEmpty())
            {
                throw new BadRequestException("Въведете паролата за сайта");
            }

            var info = new Info
            {
                Website = infoDto.Website,
                Username = infoDto.Username,
                Password = infoDto.Password,
                CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                UserId = user.Id
            };

            await context.AddAsync(info);
            await context.SaveChangesAsync();
        }

        // Ivana
        public async Task<List<InfoGetDto>> GetMyInfos(string email)
        {
            var user = await context.Users
                .Include(u => u.Infos)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var myInfos = user.Infos
                .Where(i => i.IsShared == false)
                .Select(i => new InfoGetDto
                {
                    Id = i.Id,
                    Website = i.Website,
                    Username= i.Username,
                    Password = i.Password,
                    CreateDate = i.CreateDate
                })
                .ToList();

            return myInfos;
        }

        // Alex
        public async Task<List<InfoGetDto>> GetSharedInfos(string email)
        {
            var user = await context.Users
                .Include(u => u.Infos)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var sharedInfos = user.Infos
                .Where(i => i.IsShared)
                .Select(i => new InfoGetDto
                {
                    Id = i.Id,
                    Website = i.Website,
                    Username = i.Username,
                    Password = i.Password,
                    CreateDate = i.CreateDate
                })
                .ToList();

            return sharedInfos;
        }

        // Ivana
        public async Task ShareInfo(string email, int friendId, int infoId)
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var friend = await context.Users
                .SingleOrDefaultAsync(u => u.Id == friendId && u.IsDeleted == false);

            if (friend == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var areFriends = await context.Friendships
                .AsNoTracking()
                .AnyAsync(u => (u.FirstUserId == friendId && u.SecondUserId == user.Id)
                    || (u.FirstUserId == user.Id && u.SecondUserId == friendId));

            if (!areFriends)
            {
                throw new NotFoundException("Няма такъв потребител във Вашите приятели");
            }

            var infoToShare = await context.Infos
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.Id == infoId && i.IsDeleted == false);

            if (infoToShare == null)
            {
                throw new NotFoundException("Невалидна информация за споделяне");
            }

            var info = new Info
            {
                Website = infoToShare.Website,
                Username = infoToShare.Username,
                Password = infoToShare.Password,
                CreateDate = infoToShare.CreateDate,
                UserId = friendId,
                IsShared = true
            };

            await context.AddAsync(info);
            await context.SaveChangesAsync();
        }

        // Ivana
        public async Task<List<InfoGetDto>> SortMyInfos(string email, SortingType type)
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var myInfos = context.Infos
                .AsNoTracking()
                .Where(i => i.UserId == user.Id 
                    && i.IsDeleted == false 
                    && i.IsShared == false)
                .Select(i => new InfoGetDto
                {
                    Id = i.Id,
                    Website = i.Website,
                    Username = i.Username,
                    Password = i.Password,
                    CreateDate = i.CreateDate
                });

            if (type == SortingType.Ascending)
            {
                myInfos = myInfos.OrderBy(i => i.CreateDate);
            }
            else
            {
                myInfos = myInfos.OrderByDescending(i => i.CreateDate);
            }

            return await myInfos.ToListAsync();
        }
    }
}
