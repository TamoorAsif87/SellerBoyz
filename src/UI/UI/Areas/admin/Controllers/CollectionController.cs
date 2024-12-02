using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace UI.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("{area}/{controller}/{action}")]
    public class CollectionController(IProductCollectionService productCollectionService) : Controller
    {
        public async Task<ActionResult> All()
        {
            var productCollections = await productCollectionService.GetAllAsync();
            return View(productCollections);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid Id)
        {
            if(Id == Guid.Empty) return View(new ProductCollectionDto());

            try
            {
                var productCollectionDto = await productCollectionService.GetByIdAsync(Id);
                return View(productCollectionDto);
            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductCollectionDto productCollectionDto)
        {

            try
            {
                if(productCollectionDto.Id == Guid.Empty)
                {
                    await productCollectionService.CreateAsync(productCollectionDto);
                   
                }
                else
                {
                    await productCollectionService.UpdateAsync(productCollectionDto,productCollectionDto.Id);
                }

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }

            return View(productCollectionDto);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await productCollectionService.DeleteAsync(Id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
