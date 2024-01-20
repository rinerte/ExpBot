using DataLayer.Enums;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Service
{
    public partial class AppService 
    {
        public async Task<User?> GetUserAsync(long userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.TelegramUserId == userId);
        }

        public async Task SaveNewUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeUsersActionAsync(long telegramUserId, Actions action)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                user.Action = action;
                await _context.SaveChangesAsync();
            }
        }
        public async Task ChangeUsersCurrentDataAsync(long telegramUserId, int data)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                user.CurrentData = data;
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddRowsAsync(long telegramUserId, int rowsNumber)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                if (DayPassOver(user.RowsAdded))
                {
                    user.RowsThisDay = rowsNumber;
                } else
                {
                    user.RowsThisDay+= rowsNumber;
                }
                user.RowsAdded = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddPagesAsync(long telegramUserId, int pagesNumber)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                if (DayPassOver(user.PagesAdded))
                {
                    user.PagesThisDay = pagesNumber;
                }
                else
                {
                    user.PagesThisDay += pagesNumber;
                }
                user.PagesAdded = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddArticlesAsync(long telegramUserId, int articlesNumber)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                if (DayPassOver(user.ArticlesAdded))
                {
                    user.ArticlesThisDay = articlesNumber;
                }
                else
                {
                    user.ArticlesThisDay += articlesNumber;
                }
                user.ArticlesAdded = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> RefreshMultiplierAsync(long telegramUserId)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                if (DateTime.Now.ToUniversalTime().Subtract(user.LastActivity).TotalDays >= 1)
                {
                    if (user.Multiplier > 1)
                    {
                        user.Multiplier -= (int)DateTime.Now.ToUniversalTime().Subtract(user.LastActivity).TotalDays;
                    }
                    if (user.Multiplier < 0) { user.Multiplier = 1; }
                }
                else if (DayPassOver(user.LastActivity))
                {
                    if (user.Multiplier < 300)
                    {
                        user.Multiplier += 1;
                    }
                }

                user.LastActivity = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
                return user.Multiplier;
            }
            return 1;
        }
        public async Task SetExpierenceAsync(long telegramUserId, long expPoints)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                user.Expierence += expPoints;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SetLevelAsync(long telegramUserId, int level)
        {
            User? user = await GetUserAsync(telegramUserId);
            if (user != null)
            {
                user.Level += level;
                await _context.SaveChangesAsync();
            }
        }
        private bool DayPassOver(DateTime userTime) 
        {
            if(DateTime.Now.ToUniversalTime().Subtract(userTime).TotalDays >= 1d) return true;
            if(DateTime.Now.ToUniversalTime().DayOfYear == userTime.AddDays(1).DayOfYear) return true;
            return false;
        }
    }
}
