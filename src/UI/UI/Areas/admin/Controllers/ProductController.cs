using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("{area}/{controller}/{action}")]
    public class ProductController(IProductService productService,IProductCollectionService productCollectionService,ITagService tagService) : Controller
    {
        public async Task<IActionResult> All()
        {
            var products = await productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid Id)
        {
            ViewBag.collectionsList = LoadCollections().GetAwaiter().GetResult();

            if (Id == Guid.Empty)
            {
                return View(new ProductDto());
            }

            var product = await productService.GetByIdAsync(Id);
            return View(product);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upsert(ProductDto productDto)
        {

            try
            {
                if(productDto.Id == Guid.Empty)
                {
                    await productService.CreateAsync(productDto);
                    
                }
                else
                {
                    await productService.UpdateAsync(productDto,productDto.Id);
                }

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
            }

            ViewBag.collectionsList = LoadCollections().GetAwaiter().GetResult();
            return View(productDto);
        }


        public async Task<IActionResult> Delete(Guid Id)
        {
            await productService.DeleteAsync(Id);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> AddProductTag(Guid productId)
        {
            var product = await productService.GetByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            var productTags = product.Tags!.Select(p => new TagsDto { Id = p.TagsId, Tag = p.Tags.Tag });

            ViewBag.Tags = LoadTags(productTags).GetAwaiter().GetResult();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductTag(Guid productId,Guid tagId)
        {
            try
            {
                await productService.AddTag(tagId, productId);
                return RedirectToAction(controllerName: "Product", actionName: "AddProductTag", routeValues: new { productId = productId });
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }

        public async Task<IActionResult> RemoveProductTag(Guid productId, Guid tagId)
        {
            try
            {
                await productService.RemoveTag(tagId, productId);
                return RedirectToAction(controllerName: "Product", actionName: "AddProductTag", routeValues: new { productId = productId });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IActionResult> ProductImages(Guid productId)
        {
            var product = await productService.GetByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            return View(product);
        }

        public async Task<IActionResult> AddProductImage(Guid productId, IFormFile file)
        {
            try
            {
                await productService.AddProductImage(productId, file);
                return RedirectToAction(controllerName: "Product", actionName: "ProductImages", routeValues: new { productId = productId });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> RemoveProductImage(Guid productId, string img)
        {
            try
            {
                await productService.RemoveProductImage(productId, img);
                return RedirectToAction(controllerName: "Product", actionName: "ProductImages", routeValues: new { productId = productId });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task<IEnumerable<SelectListItem>> LoadCollections()
        {
            var productCollection = await productCollectionService.GetAllAsync();

            return productCollection.Select(x => new SelectListItem { Text = x.CollectionName, Value = x.Id.ToString() });
        }

        private async Task<IEnumerable<SelectListItem>> LoadTags(IEnumerable<TagsDto> productTags)
        {
            var tags = await tagService.GetAllAsync();
            tags = tags.Where(t =>  !productTags.Select(t => t.Id).Contains(t.Id));


            return tags.Select(x => new SelectListItem { Text = x.Tag, Value = x.Id.ToString() });
        }
    }
}
