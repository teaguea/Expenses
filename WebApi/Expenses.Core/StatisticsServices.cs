﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Core
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly DB.AppDbContext _context;
        private readonly DB.User _user;

        public StatisticsServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public IEnumerable<KeyValuePair<string, double>> GetExpenseAmountPerCategory() =>
            _context.Expenses
                .Where(e => e.User.Id == _user.Id)
                .AsEnumerable()
                .GroupBy(e => e.Description)
                .ToDictionary(e => e.Key, e => e.Sum(x => x.Amount))
                .Select(x => new KeyValuePair<string, double>(x.Key, x.Value));
    }
}
