using DataAccesLayer.Models;

namespace DTO.DTOs.SubCategoryDtos;

public class SubCategoryDto : BaseDto
{
    public int? CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual Category? Category { get; set; }
}