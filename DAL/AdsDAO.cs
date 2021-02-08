using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class AdsDAO : PostContext
    {
        public int AddAds(Ad ad)
        {
            try
            {
                db.Ads.Add(ad);
                db.SaveChanges();
                return ad.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdsDTO> GetAdsList()
        {
            List<AdsDTO> adsListDTO = new List<AdsDTO>();
            List<Ad> adList = db.Ads.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in adList)
            {
                AdsDTO dto = new AdsDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.ImagePath = item.ImagePath;
                dto.Link = item.Link;
                dto.Size = item.Size;
                adsListDTO.Add(dto);
            }

            return adsListDTO;
        }

        public string UpdateUser(AdsDTO model)
        {
            try
            {
                Ad ad = db.Ads.First(x => x.ID == model.ID);
                string oldImagePath = ad.ImagePath;
                ad.Name = model.Name;
                ad.Link = model.Link;
                ad.Size = model.Size;
                if (model.ImagePath != null)
                {
                    ad.ImagePath = model.ImagePath;
                }
                ad.LastUpdateDate = DateTime.Now;
                ad.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteAds(int iD)
        {
            try
            {
                Ad ad = db.Ads.First(x => x.ID == iD);
                string imagePath = ad.ImagePath;
                ad.isDeleted = true;
                ad.DeletedDate = DateTime.Now;
                ad.LastUpdateDate = DateTime.Now;
                ad.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                return imagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdsDTO GetAdsById(int iD)
        {
            AdsDTO dto = new AdsDTO();
            Ad ad = db.Ads.First(x => x.ID == iD);
            dto.ID = ad.ID;
            dto.Name = ad.Name;
            dto.ImagePath = ad.ImagePath;
            dto.Link = ad.Link;
            dto.Size = ad.Size;
            return dto;
        }
    }
}
