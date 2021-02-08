using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public FavDTO GetFav()
        {
            FavLogoTitle fav = db.FavLogoTitles.First();
            FavDTO dto = new FavDTO();
            dto.ID = fav.ID;
            dto.Title = fav.Title;
            dto.Fav = fav.Fav;
            dto.Logo = fav.Logo;

            return dto;
        }

        public FavDTO UpdateUser(FavDTO model)
        {
            try
            {
                FavLogoTitle fav = db.FavLogoTitles.First();
                FavDTO dto = new FavDTO();
                dto.ID = fav.ID;
                fav.Title = model.Title;
                dto.Fav = fav.Fav;
                dto.Logo = fav.Logo;
                if (model.Fav != null)
                {
                    fav.Fav = model.Fav;
                }
                if (model.Logo != null)
                {
                    fav.Logo = model.Logo;
                }
                fav.LastUpdateDate = DateTime.Now;
                fav.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }        
        }
    }
}
