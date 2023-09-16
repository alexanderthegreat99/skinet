// using API.Dtos;
// using AutoMapper;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductType> productTypeRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {

            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

           // var product = await _productsRepo.GetEntityWithSpec(spec);
            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        //     return products.Select(product => new ProductToReturnDto 
        //    {
        //     Id = product.Id,
        //     Name = product.Name,
        //     Description = product.Description,
        //     PictureUrl = product.PictureUrl,
        //     Price = product.Price,
        //     ProductBrand = product.ProductBrand.Name,
        //     ProductType = product.ProductType.Name,
            
        //    }).ToList();
        // return Ok(_mapper
        //     .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Product>> GetProduct(int id)
        // {
        //    var spec = new ProductsWithTypesAndBrandsSpecification(id);

        //    return await _productsRepo.GetEntityWithSpec(spec);
        // }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(id);

           var product = await _productsRepo.GetEntityWithSpec(spec);

           if (product == null) return NotFound(new ApiResponse(404));

        //    return new ProductToReturnDto 
        //    {
        //     Id = product.Id,
        //     Name = product.Name,
        //     Description = product.Description,
        //     PictureUrl = product.PictureUrl,
        //     Price = product.Price,
        //     ProductBrand = product.ProductBrand.Name,
        //     ProductType = product.ProductType.Name,
            
        //    };
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}