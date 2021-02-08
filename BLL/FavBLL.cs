using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class FavBLL
    {
        FavDAO dao = new FavDAO();

        public FavDTO GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO dto = new FavDTO();
            dto = dao.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.IconUpdated, General.TableName.Icon, dto.ID);
            return dto;
        }
    }
}
