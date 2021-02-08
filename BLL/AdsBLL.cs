using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO dao = new AdsDAO();

        public bool AddAds(AdsDTO model)
        {
            Ad ad = new Ad();
            ad.Name = model.Name;
            ad.ImagePath = model.ImagePath;
            ad.Link = model.Link;
            ad.Size = model.Size;
            ad.AddDate = DateTime.Now;
            ad.LastUpdateUserID = UserStatic.UserID;
            ad.LastUpdateDate = DateTime.Now;
            int AdID = dao.AddAds(ad);
            LogDAO.AddLog(General.ProcessType.AdsAdded, General.TableName.Ads, AdID);

            return true;
        }

        public List<AdsDTO> GetAdsList()
        {
            return dao.GetAdsList();
        }

        public AdsDTO GetAdsById(int iD)
        {
            return dao.GetAdsById(iD);
        }

        public string UpdateUser(AdsDTO model)
        {
            string oldImagePath = dao.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.AdsUpdated, General.TableName.Ads, model.ID);
            return oldImagePath;
        }

        public string DeleteAds(int iD)
        {
            string imagePath = dao.DeleteAds(iD);
            LogDAO.AddLog(General.ProcessType.AdsDeleted, General.TableName.Ads, iD);
            return imagePath;
        }
    }
}
