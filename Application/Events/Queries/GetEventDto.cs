using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Events.Queries;

public class GetEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public DateTime Date { get; set; }

    public string Location { get; set; } = default!;

    public string? Info { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Event, GetEventDto>();
        }
    }
}

