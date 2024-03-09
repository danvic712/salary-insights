// -----------------------------------------------------------------------
// <copyright file= "ChartManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-6 22:19
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Charts.Contracts;
using SalaryInsights.Applications.Charts.Dtos;
using SalaryInsights.Applications.Shared;
using SalaryInsights.Applications.Shared.Contracts;

namespace SalaryInsights.Applications.Charts;

public class ChartManager : BaseManager, IChartManager
{
    #region Initializes

    private readonly SalaryInsightsDbContext _dbContext;

    public ChartManager(
        ICurrentUser currentUser,
        SalaryInsightsDbContext dbContext) : base(currentUser)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public async Task<SummaryResponse> GetSummaryAsync()
    {
        var userId = _currentUser.UserId;

        int currentYear = DateTime.Now.Year;
        int salaryMonth = DateTime.Now.AddMonths(-1).Month;

        var query = _dbContext.Payrolls.AsNoTracking()
            .Where(i => i.UserId == userId)
            .Select(i => new
            {
                i.Month,
                i.NetPay
            })
            .GroupBy(i => 1) // Let all data into one group
            .Select(g => new SummaryResponse
            {
                TotalIncome = (decimal)g.Sum(i => (double)i.NetPay),
                YearlyIncome = (decimal)g.Where(i => i.Month.Year == currentYear).Sum(i => (double)i.NetPay),
                MonthlyIncome = (decimal)g.Where(i => i.Month.Year == currentYear && i.Month.Month == salaryMonth)
                    .Sum(i => (double)i.NetPay)
            });

        return await query.FirstOrDefaultAsync() ?? new SummaryResponse();
    }

    public async Task<IList<YearlyTrendsResponse>> GetYearlyTrendsAsync(int limit)
    {
        var userId = _currentUser.UserId;

        var query = _dbContext.Payrolls.AsNoTracking()
            .Where(i => i.UserId == userId)
            .Select(i => new
            {
                Year = i.Month.Year,
                i.NetPay,
                i.GrossPay
            })
            .GroupBy(i => i.Year)
            .Select(i => new YearlyTrendsResponse
            {
                Year = i.Key,
                GrossPay = (decimal)i.Sum(x => (double)x.GrossPay),
                NetPay = (decimal)i.Sum(x => (double)x.NetPay)
            })
            .OrderByDescending(i => i.Year);

        return await query.ToListAsync();
    }

    public async Task<IList<MonthlyTrendsResponse>> GetMonthlyTrendsAsync(int year)
    {
        var userId = _currentUser.UserId;
        
        var query = _dbContext.Payrolls.AsNoTracking()
            .Where(i => i.UserId == userId && i.Month.Year == year)
            .Select(i => new
            {
                i.Month,
                i.NetPay,
                i.GrossPay
            })
            .GroupBy(i => i.Month)
            .Select(i => new MonthlyTrendsResponse
            {
                Month = i.Key,
                GrossPay = (decimal)i.Sum(x => (double)x.GrossPay),
                NetPay = (decimal)i.Sum(x => (double)x.NetPay)
            })
            .OrderBy(i => i.Month);

        return await query.ToListAsync();
    }

    #endregion
}