using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DTO;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return cat.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDTO> GetCategoryList()
        {
            List<CategoryDTO> catListDTO = new List<CategoryDTO>();
            List<Category> catList = db.Categories.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in catList)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                catListDTO.Add(dto);
            }

            return catListDTO;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x => x.isDeleted == false).OrderBy(x => x.CategoryName).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.ID.ToString(),
                Selected = true
            }).ToList();

            return categoryList;
        }

        public CategoryDTO GetCategoryById(int iD)
        {
            CategoryDTO dto = new CategoryDTO();
            Category cat = db.Categories.First(x => x.ID == iD);
            dto.ID = cat.ID;
            dto.CategoryName = cat.CategoryName;
            return dto;
        }

        public List<Post> DeleteCategory(int iD)
        {
            try
            {
                Category cat = db.Categories.First(x => x.ID == iD);
                cat.isDeleted = true;
                cat.DeletedDate = DateTime.Now;
                cat.LastUpdateDate = DateTime.Now;
                cat.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                List<Post> postList = db.Posts.Where(x => x.isDeleted == false && x.CategoryID == iD).ToList();

                return postList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category cat = db.Categories.First(x => x.ID == model.ID);
                cat.CategoryName = model.CategoryName;
                cat.LastUpdateDate = DateTime.Now;
                cat.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
