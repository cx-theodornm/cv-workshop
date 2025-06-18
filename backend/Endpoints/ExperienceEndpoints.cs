using backend.Data.Mappers;
using backend.Services;

namespace backend.Endpoints;

public static class ExperienceEndpoints
{
    public static void MapExperienceEndpoints(this WebApplication app)
    {
        // GET /experiences
        app.MapGet(
                "/experiences",
                async (ICvService cvService) =>
                {
                    var experiences = await cvService.GetAllExperiencesAsync();
                    var expDtos = experiences.Select(e => e.ToDto()).ToList();

                    return Results.Ok(expDtos);
                }
            )
            .WithName("GetAllExperiences")
            .WithTags("Experiences");

        // GET /experiences/{id}
        app.MapGet(
                "/experiences/{id:guid}",
                async (Guid id, ICvService cvService) =>
                {
                    var experience = await cvService.GetExperienceByIdAsync(id);
                    var expDto = experience.ToDto();
                    // var expDto = experience.Select(e => e.toDto());
                    return Results.Ok(expDto);
                }
            )
            .WithName("GetExperienceById")
            .WithTags("Experiences");

        // GET /experiences/type/{type}
        app.MapGet(
                "/experiences/type/{type}",
                async (string type, ICvService cvService) =>
                {
                    // TODO: Oppgave 3

                    return Results.Ok();
                }
            )
            .WithName("GetExperiencesByType")
            .WithTags("Experiences");
    }
}
