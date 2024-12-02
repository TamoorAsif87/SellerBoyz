using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace UI.Areas.admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("{area}/{controller}/{action}")]
    public class TagController(ITagService tagService) : Controller
    {
        public async Task<ActionResult> All()
        {
            var tags = await tagService.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid Id)
        {
            if(Id == Guid.Empty) return View(new TagsDto());

            try
            {
                var tagDto = await tagService.GetByIdAsync(Id);
                return View(tagDto);
            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(TagsDto tagsDto)
        {

            try
            {
                if(tagsDto.Id == Guid.Empty)
                {
                    await tagService.CreateAsync(tagsDto);
                   
                }
                else
                {
                    await tagService.UpdateAsync(tagsDto,tagsDto.Id);
                }

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }

            return View(tagsDto);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await tagService.DeleteAsync(Id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
