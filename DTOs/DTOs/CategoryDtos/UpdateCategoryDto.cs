

using DataAccesLayer.Models;

namespace DTO.DTOs.CategoryDtos;

public partial class UpdateCategoryDto : BaseDto
{
    public string? Name { get; set; }
}
