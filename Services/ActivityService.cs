using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAsmv.Data;
using ServerAsmv.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Services
{
    public class ActivityService
    {
        private readonly AppData _context;

        public ActivityService(AppData context)
        {
            _context = context;
        }

        public async Task<Activity> AddActivityAsync(ActivityDTO activityDto)
        {
            var coordinator = await _context.Users.FindAsync(activityDto.CoordinatorId);
            if (coordinator == null)
            {
                throw new ArgumentException("Invalid CoordinatorId provided.");
            }

            var activity = new Activity
            {
                Name = activityDto.Name,
                Date = activityDto.Date,
                CoordinatorId = activityDto.CoordinatorId,
                Coordinator = coordinator,
                AssignedUsers = activityDto.AssignedUserIds
                    .Select(userId => new ActivityUser { UserId = userId })
                    .ToList()
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return activity;
        }


        public async Task<List<Activity>> GetAllActivitiesAsync()
        {
            return await _context.Activities
                .Include(a => a.Coordinator)
                .Include(a => a.AssignedUsers)
                .ToListAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(long id) // Change id to long
        {
            return await _context.Activities
                .Include(a => a.Coordinator)
                .Include(a => a.AssignedUsers)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateActivityAsync(long id, ActivityDTO activityDto)
        {
            var activity = await _context.Activities
                .Include(a => a.AssignedUsers)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
            {
                return false;
            }

            activity.Name = activityDto.Name;
            activity.Date = activityDto.Date;
            activity.CoordinatorId = activityDto.CoordinatorId;

            // Remove existing assigned users
            activity.AssignedUsers.Clear();
            
            // Add updated assigned users
            activity.AssignedUsers = activityDto.AssignedUserIds
                .Select(userId => new ActivityUser { UserId = userId, ActivityId = id })
                .ToList();

            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteActivityAsync(long id) // Change id to long
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return false;
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
