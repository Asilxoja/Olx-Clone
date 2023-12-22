using BusinessLogicLayer.Extended;
using DTO.DTOs.SubCategoryDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ISubSubCategoryService
{
    Task<List<SubCategoryDto>> GetAll();
    Task<PagedList<SubCategoryDto>> GetAllPaged(int pageSize, int pageNumber);
    Task<SubCategoryDto> GetById(int id);
    Task Add(AddSubCategoryDto SubCategoryDto);
    Task Update(UpdateSubCategoryDto SubCategoryDto);
    Task Delete(int id);
}
