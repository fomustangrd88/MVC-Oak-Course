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
    public class PostBLL
    {
        PostDAO dao = new PostDAO();

        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.ShortContent = model.ShortContent;
            post.PostContent = model.PostContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.LanguageName;
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserID;
            int id = dao.AddPost(post);
            LogDAO.AddLog(General.ProcessType.PostAdded, General.TableName.Post, id);

            SavePostImage(model.PostImageList, id);
            AddTag(model.PostTag, id);

            return true;
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public void DeleteComment(int iD)
        {
            dao.DeleteComment(iD);
            LogDAO.AddLog(General.ProcessType.CommentDeleted, General.TableName.Comment, iD);
        }

        public void ApproveComment(int iD)
        {
            dao.ApproveComment(iD);
            LogDAO.AddLog(General.ProcessType.CommentApproved, General.TableName.Comment, iD);
        }

        public List<CommentDTO> GetComments()
        {
            return dao.GetComments();
        }

        private void AddTag(string postTag, int postID)
        {
            string[] tags;
            tags = postTag.Split(',');
            List<PostTag> tagList = new List<PostTag>();

            foreach (var item in tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = postID;
                tag.TagContent = item;
                tag.AddDate = DateTime.Now;
                tag.LastUpdateDate = DateTime.Now;
                tag.LastUpdateUserID = UserStatic.UserID;
                tagList.Add(tag);
            }

            foreach (var item in tagList)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdded, General.TableName.Tag, tagID);
            }
        }

        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment();
            comment.PostID = model.PostID;
            comment.PostSurname = model.Name;
            comment.Email = model.Email;
            comment.CommentContent = model.Message;
            comment.AddDate = DateTime.Now;

            dao.AddComment(comment);

            return true;            
        }

        public CountDTO GetAllCounts()
        {
            return dao.GetAllCounts();
        }

        public void SavePostImage(List<PostImageDTO> list, int postID)
        {
            List<PostImage> imageList = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage();
                image.PostID = postID;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                imageList.Add(image);
            }

            foreach (var item in imageList)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdded, General.TableName.Image, imageID);
            }

        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        public PostDTO GetPostById(int iD)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostById(iD);
            dto.PostImageList = dao.GetPostImagesByPostId(iD);
            List<PostTag> tagList = dao.GetTagsByPostId(iD);

            string tagValue = string.Empty;
            foreach (var item in tagList)
            {
                tagValue += item.TagContent;
                tagValue += ",";
            }

            dto.PostTag = tagValue;

            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.PostUpdated, General.TableName.Post, model.ID);
            if(model.PostImageList != null)
            {
                SavePostImage(model.PostImageList, model.ID);
            }

            dao.DeleteTags(model.ID);
            AddTag(model.PostTag, model.ID);

            return true;
        }

        public string DeletePostImage(int iD)
        {
            string imagePath = dao.DeletePostImage(iD);
            LogDAO.AddLog(General.ProcessType.ImageDeleted, General.TableName.Image, iD);
            return imagePath;
        }

        public List<PostImageDTO> DeletePost(int iD)
        {
            List<PostImageDTO> imageList = dao.DeletePost(iD);
            LogDAO.AddLog(General.ProcessType.PostDeleted, General.TableName.Post, iD);
            return imageList;
        }

        public CountDTO GetCounts()
        {
            CountDTO dto = new CountDTO();
            dto.MessageCount = dao.GetMessageCount();
            dto.CommentCount = dao.GetCommentCount();

            return dto;
        }
    }
}
