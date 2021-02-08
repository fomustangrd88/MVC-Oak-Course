using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;
using DTO;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();

        public bool AddCategory(CategoryDTO model)
        {
            Category cat = new Category();
            cat.CategoryName = model.CategoryName;
            cat.AddDate = DateTime.Now;
            cat.LastUpdateDate = DateTime.Now;
            cat.LastUpdateUserID = UserStatic.UserID;          
            int id = dao.AddCategory(cat);
            LogDAO.AddLog(General.ProcessType.CategoryAdded, General.TableName.Category, id);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }

        public List<CategoryDTO> GetCategoryList()
        {
            List<CategoryDTO> listDTO = new List<CategoryDTO>();
            listDTO = dao.GetCategoryList();
            return listDTO;
        }

        public CategoryDTO GetCategoryById(int iD)
        {
            CategoryDTO dto = new CategoryDTO();
            dto = dao.GetCategoryById(iD);
            return dto;
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            LogDAO.AddLog(General.ProcessType.CategoryUpdated, General.TableName.Category, model.ID);
            return true;
        }

        PostBLL postBLL = new PostBLL();

        public List<PostImageDTO> DeleteCategory(int iD)
        {
            List<Post> postList = dao.DeleteCategory(iD);
            LogDAO.AddLog(General.ProcessType.CategoryDeleted, General.TableName.Category, iD);

            List<PostImageDTO> imageList = new List<PostImageDTO>();
            foreach (var item in postList)
            {
                List<PostImageDTO> imageList2 = postBLL.DeletePost(item.ID);
                //LogDAO.AddLog(General.ProcessType.PostDeleted, General.TableName.Post, item.ID);

                foreach (var item2 in imageList2)
                {
                    imageList.Add(item2);
                }
            }

            return imageList;
        }
    }
}
