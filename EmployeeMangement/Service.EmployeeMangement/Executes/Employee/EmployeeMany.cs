﻿using Azure;
using DBContext.EmployeeMangement;
using DBContext.EmployeeMangement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.EmployeeMangement.Executes.EmployeeManyModel;
namespace Service.EmployeeMangement.Executes
{
    public class EmployeeMany
    {
        private readonly EmployeeManagementContext _context;

        public EmployeeMany(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<EmployeeListResponse> GetEmployees(FilterListRequest filter)
        {
            IQueryable<Employee> query = _context.Employees
                .Include(e => e.JobPosition)
                .Include(e => e.Department)
                .Where(p => p.Status == 1);

            // Search keyword
            if (!string.IsNullOrWhiteSpace(filter.KeySearch))
            {
                string keyword = filter.KeySearch.Trim();
                string collate = "Vietnamese_CI_AI";

                bool isId = int.TryParse(keyword, out int id);

                query = query.Where(x =>
                    (isId && x.Id == id) ||
                    EF.Functions.Collate(x.Keyword, collate).Contains(keyword) ||
                    EF.Functions.Collate(x.Email, collate).Contains(keyword) ||
                    EF.Functions.Collate(x.Phone, collate).Contains(keyword) ||
                    EF.Functions.Collate(x.Fullname, collate).Contains(keyword)
                );
            }

            // Filter department
            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(p => p.DepartmentId == filter.DepartmentId.Value);
            }

            // Filter job position
            if (filter.JobpositionId.HasValue)
            {
                query = query.Where(p => p.JobPositionId == filter.JobpositionId.Value);
            }

            // Filter position 
            if (!string.IsNullOrWhiteSpace(filter.Position))
            {
                query = query.Where(p => p.Position == filter.Position);
            }

            // Filter create date range
            if (filter.CreateDateFrom.HasValue)
                query = query.Where(p => p.CreateDate >= filter.CreateDateFrom.Value.Date);

            if (filter.CreateDateTo.HasValue)
            {
                var endDate = filter.CreateDateTo.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.CreateDate <= endDate);
            }

            // Count total records
            int totalRecords = await query.CountAsync();

            // Paging
            int page = filter.Page <= 0 ? 1 : filter.Page;
            const int PAGE_SIZE = 10;

            var results = await query
            .OrderByDescending(x => x.CreateDate)
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .Select(e => new EmployeeResponse
            {
                Id = e.Id,
                Keyword = e.Keyword,
                Status = e.Status,
                CreateBy = e.CreateBy,
                CreateDate = e.CreateDate,
                UpdatedBy = e.UpdatedBy,
                UpdatedDate = e.UpdatedDate,
                Fullname = e.Fullname,
                Email = e.Email,
                Phone = e.Phone,
                Position = e.Position,

                Address = e.JobPosition.Address,
                JobPositionCode = e.JobPosition.Code,
                JobPositionName = e.JobPosition.Name,

                DepartmentName = e.Department.Name,
                DepartmentCode = e.Department.Code
            })
            .ToListAsync();

            return new EmployeeListResponse
            {
                Items = results,
                TotalRecords = totalRecords,
                CurrentPage = page,
                PageSize = PAGE_SIZE
            };
        }














    }
}
