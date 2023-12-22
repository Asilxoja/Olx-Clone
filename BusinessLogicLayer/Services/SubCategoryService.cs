using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DTO.DTOs.SubCategoryDtos;
using System.Net.Http.Headers;

namespace BusinessLogicLayer.Services;

public class SubCategoryService (IUnitOfWork unitOfWork,
                              IMapper mapper)
    : ISubSubCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task Add(AddSubCategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException("SubCategory null bo'lib qoldi!");
        }

        var subCategory = _mapper.Map<SubCategory>(categoryDto);
        if (!subCategory.IsValid())
        {
            throw new CustomException("Invalid SubCategory");
        }

        var categories = await _unitOfWork.SubCategoryInterface.GetAllAsync();
        if (subCategory.IsExist(categories))
        {
            throw new CustomException($"{subCategory.Name} uje bor!");
        }

        var list = await _unitOfWork.CategoryInterface.GetAllAsync();
        var categoryIdIsValid = list.Any(c => c.Id == subCategory.CategoryId);

        if (!categoryIdIsValid)
        {
            throw new CustomException("CategoryId not found");
        }

        await _unitOfWork.SubCategoryInterface.AddAsync(subCategory);
        await _unitOfWork.SaveAsync();

    }

    public async Task Delete(int id)
    {
        var subCategory = await _unitOfWork.SubCategoryInterface.GetByIdAsync(id);
        if (subCategory is null)
        {
            throw new ArgumentException("Bunday SubCategory mavjud emas!");
        }

        await _unitOfWork.SubCategoryInterface.DeleteAsync(subCategory);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<SubCategoryDto>> GetAll()
    {
        var categories = await _unitOfWork.SubCategoryInterface.GetAllAsync();
        return categories.Select(c => _mapper.Map<SubCategoryDto>(c))
                         .ToList();
    }

    public async Task<PagedList<SubCategoryDto>> GetAllPaged(int pageSize, int pageNumber)
    {
        var categories = await GetAll();
        PagedList<SubCategoryDto> pagedList = new(categories, categories.Count, pageNumber, pageSize);
        return pagedList.ToPagedList(categories,
                                      pageSize,
                                      pageNumber);
    }

    public async Task<SubCategoryDto> GetById(int id)
    {
        var subCategory = await _unitOfWork.SubCategoryInterface.GetByIdAsync(id);
        if (subCategory is null)
        {
            throw new ArgumentException("SubCategory topilmadi!");
        }
        return _mapper.Map<SubCategoryDto>(subCategory);
    }

    public async Task Update(UpdateSubCategoryDto categoryDto)
    {
        if (categoryDto is null)
        {
            throw new ArgumentNullException("SubCategory null bo'lib qoldi!");
        }       
        var subcategories = await _unitOfWork.SubCategoryInterface.GetAllAsync();
        var subCategory = subcategories.FirstOrDefault(c => c.Id == categoryDto.Id);

        if (subCategory is null)
        {
            throw new ArgumentNullException("SubCategory topilmadi!");
        }

        var updateSubCategory = _mapper.Map<SubCategory>(categoryDto);
        if (!updateSubCategory.IsValid())
        {
            throw new CustomException("SubCategory Invalid!");
        }

        if (updateSubCategory.IsExist(subcategories))
        {
            throw new CustomException("SubCategory uje bor");
        }

        await _unitOfWork.SubCategoryInterface.UpdateAsync(updateSubCategory);
        await _unitOfWork.SaveAsync();
    }
}
