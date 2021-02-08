using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();

        public bool AddSocialMedia(SocialMediaDTO model)
        {
            SocialMedia social = new SocialMedia();
            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.AddDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;
            social.LastUpdateDate = DateTime.Now;
            int id = dao.AddSocialMedia(social);
            LogDAO.AddLog(General.ProcessType.SocialAdded, General.TableName.Social, id);

            return true;
        }

        public List<SocialMediaDTO> GetSocialMediaData()
        {
            List<SocialMediaDTO> listDTO = new List<SocialMediaDTO>();
            listDTO = dao.GetSocialMediaData();
            return listDTO;
        }

        public SocialMediaDTO GetSocialMediaDataWithID(int id)
        {
            SocialMediaDTO socialDTO = new SocialMediaDTO();
            socialDTO = dao.GetSocialMediaDataWithID(id);
            return socialDTO;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            string oldImagepath = dao.UpdateSocialMedia(model);
            LogDAO.AddLog(General.ProcessType.SocialUpdated, General.TableName.Social, model.ID);
            return oldImagepath;
        }

        public string DeleteSocialMedia(int iD)
        {
            string imagePath = dao.DeleteSocialMedia(iD);
            LogDAO.AddLog(General.ProcessType.SocialDeleted, General.TableName.Social, iD);
            return imagePath;
        }
    }
}
