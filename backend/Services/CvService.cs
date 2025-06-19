using backend.Data;
using backend.Data.Mappers;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CvService(AppDbContext context) : ICvService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await context.Users.OrderBy(u => u.Name).ToListAsync();
    }

    // TODO: Oppgave 1
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        // TODO: Oppgave 2
        return await context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync();
        // return [];
    }

    public async Task<Experience?> GetExperienceByIdAsync(Guid id)
    {
        // TODO: Oppgave 2
        return await context.Experiences.FindAsync(id);

    }

    public async Task<IEnumerable<Experience>> GetExperiencesByTypeAsync(string type)
    {
        // TODO: Oppgave 3
        return await context.Experiences.Where(e => e.Type == type)
                    .OrderByDescending(e => e.StartDate)
                    .ToListAsync();

        // return [];
    }

    // TODO: Oppgave 4 ny metode (husk å legge den til i interfacet)
    public async Task<IEnumerable<User>> GetUsersWithDesiredSkills(IEnumerable<string> desiredTechnologies)
    {
        var users = GetAllUsersAsync();
        var filteredUsers = allUsers.Where(user =>
            UserMapper
                .ParseUserSkills(user.Skills)
                .Any(skill =>
                    desiredTechnologies
                        .Select(tech => tech.ToLower())
                        .Contains(skill.Technology.ToLower())
                )
        );
        return filteredUsers;
    }
}

public enum Type
{
    hobbyProject,
    education,
    voluntary,
    coach,
    work
}