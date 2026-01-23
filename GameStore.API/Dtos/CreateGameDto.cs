using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record CreateGameDto
(
    [Required] 
    [StringLength(30, MinimumLength = 3)]
    string Name,
    [Range(1, 50)]
    int GenreId,
    [Range(1, 100)]
    decimal Price,
    DateOnly ReleaseDate
);
