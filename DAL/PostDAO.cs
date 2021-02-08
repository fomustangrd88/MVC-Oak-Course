using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
                return post.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddImage(PostImage image)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostImages.Add(image);
                    db.SaveChanges();
                }
                return image.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddTag(PostTag tag)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostTags.Add(tag);
                    db.SaveChanges();
                }
                return tag.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostDTO> GetPosts()
        {
            List<PostDTO> dtoList = new List<PostDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    CategoryName = c.CategoryName,
                                    AddDate = p.AddDate
                                }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.CategoryName;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public void DeleteComment(int iD)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Comment comment = db.Comments.First(x => x.ID == iD);
                    comment.isDeleted = true;
                    comment.DeletedDate = DateTime.Now;
                    comment.LastUpdateDate = DateTime.Now;
                    comment.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApproveComment(int iD)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Comment comment = db.Comments.First(x => x.ID == iD);
                    comment.isApproved = true;
                    comment.ApproveDate = DateTime.Now;
                    comment.ApproveUserID = UserStatic.UserID;
                    comment.LastUpdateDate = DateTime.Now;
                    comment.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CountDTO GetAllCounts()
        {
            CountDTO dto = new CountDTO();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }
            return dto;
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtoList = new List<CommentDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostID = p.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                isApproved = c.isApproved
                            }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostID = item.PostID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dto.isApproved = item.isApproved;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtoList = new List<CommentDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostID = p.ID,
                                PostTitle = p.Title,
                                Email = c.Email,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                            }).OrderByDescending(x => x.AddDate).ToList();

                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO();
                    dto.ID = item.ID;
                    dto.PostID = item.PostID;
                    dto.PostTitle = item.PostTitle;
                    dto.Email = item.Email;
                    dto.CommentContent = item.Content;
                    dto.AddDate = item.AddDate;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtoList = new List<PostDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var postList = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    CategoryName = c.CategoryName,
                                    AddDate = p.AddDate,
                                    seoLink = p.SeoLink
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();



                foreach (var item in postList)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.CategoryName;
                    dto.AddDate = item.AddDate;
                    dto.SeoLink = item.seoLink;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public int GetCommentCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
        }

        public int GetMessageCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PostDTO GetPostById(int iD)
        {
            PostDTO dto = new PostDTO();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == iD);
                dto.ID = post.ID;
                dto.Title = post.Title;
                dto.ShortContent = post.ShortContent;
                dto.PostContent = post.PostContent;
                dto.LanguageName = post.LanguageName;
                dto.Notification = post.Notification;
                dto.SeoLink = post.SeoLink;
                dto.Slider = post.Slider;
                dto.Area1 = post.Area1;
                dto.Area2 = post.Area2;
                dto.Area3 = post.Area3;
                dto.CategoryID = post.CategoryID;
            }
            return dto;
        }

        public List<PostImageDTO> GetPostImagesByPostId(int iD)
        {
            List<PostImageDTO> imageListDTO = new List<PostImageDTO>();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostImage> imageList = db.PostImages.Where(x => x.isDeleted == false && x.PostID == iD).ToList();

                foreach (var item in imageList)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    imageListDTO.Add(dto);
                }
            }
            return imageListDTO;
        }

        public List<PostTag> GetTagsByPostId(int iD)
        {
            List<PostTag> list = new List<PostTag>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                list = db.PostTags.Where(x => x.isDeleted == false && x.PostID == iD).ToList();
            }
            return list;
        }

        public void UpdatePost(PostDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.First(x => x.ID == model.ID);
                post.Title = model.Title;
                post.Area1 = model.Area1;
                post.Area2 = model.Area2;
                post.Area3 = model.Area3;
                post.CategoryID = model.CategoryID;
                post.LanguageName = model.LanguageName;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                post.Notification = model.Notification;
                post.PostContent = model.PostContent;
                post.SeoLink = model.SeoLink;
                post.ShortContent = model.ShortContent;
                post.Slider = model.Slider;
                db.SaveChanges();
            }
        }

        public string DeletePostImage(int iD)
        {
            try
            {
                string imagePath = string.Empty;

                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    PostImage image = db.PostImages.First(x => x.ID == iD);
                    imagePath = image.ImagePath;
                    image.isDeleted = true;
                    image.DeletedDate = DateTime.Now;
                    image.LastUpdateDate = DateTime.Now;
                    image.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }

                return imagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int iD)
        {
            try
            {
                List<PostImageDTO> imageListDTO = new List<PostImageDTO>();

                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    Post post = db.Posts.First(x => x.ID == iD);
                    post.isDeleted = true;
                    post.DeletedDate = DateTime.Now;
                    post.LastUpdateDate = DateTime.Now;
                    post.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();

                    List<PostImage> imageList = db.PostImages.Where(x => x.PostID == iD).ToList();

                    foreach (var item in imageList)
                    {
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = item.ImagePath;
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Now;
                        item.LastUpdateDate = DateTime.Now;
                        item.LastUpdateUserID = UserStatic.UserID;
                        imageListDTO.Add(dto);
                    }
                    db.SaveChanges();
                }

                return imageListDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteTags(int PostID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostTag> tagList = db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
                foreach (var item in tagList)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                }
                db.SaveChanges();
            }
        }
    }
}
