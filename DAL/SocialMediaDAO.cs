using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia social)
        {
            try
            {
                db.SocialMedias.Add(social);
                db.SaveChanges();
                return social.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMediaData()
        {
            List<SocialMediaDTO> socialDTOList = new List<SocialMediaDTO>();
            List<SocialMedia> socialList = db.SocialMedias.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            foreach (var item in socialList)
            {
                SocialMediaDTO dto = new SocialMediaDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.ImagePath = item.ImagePath;
                dto.Link = item.Link;
                socialDTOList.Add(dto);
            }

            return socialDTOList;
        }

        public SocialMediaDTO GetSocialMediaDataWithID(int id)
        {
            SocialMedia socialMedia = db.SocialMedias.First(x => x.ID == id);
            SocialMediaDTO socialMediaDTO = new SocialMediaDTO();
            socialMediaDTO.ID = socialMedia.ID;
            socialMediaDTO.Name = socialMedia.Name;
            socialMediaDTO.Link = socialMedia.Link;
            socialMediaDTO.ImagePath = socialMedia.ImagePath;

            return socialMediaDTO;
        }

        public string DeleteSocialMedia(int iD)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == iD);
                string imagePath = social.ImagePath;
                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return imagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == model.ID);
                string oldImagePath = social.ImagePath;
                social.Name = model.Name;
                social.Link = model.Link;
                if (model.ImagePath != null)
                {
                    social.ImagePath = model.ImagePath;
                }
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return oldImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
