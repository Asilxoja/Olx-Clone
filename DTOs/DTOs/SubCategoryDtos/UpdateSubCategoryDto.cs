

using DataAccesLayer.Models;

namespace DTO.DTOs.SubCategoryDtos;

public class UpdateSubCategoryDto:BaseDto
{
    public int? CategoryId { get; set; }

    public string? Name { get; set; }
}