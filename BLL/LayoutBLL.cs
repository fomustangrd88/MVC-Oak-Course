using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        SocialMediaDAO socialDAO = new SocialMediaDAO();
        FavDAO favDAO = new FavDAO();
        MetaDAO metaDAO = new MetaDAO();
        AddressDAO addressDAO = new AddressDAO();
        PostDAO postDAO = new PostDAO();

        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categoryDAO.GetCategoryList();

            List<SocialMediaDTO> socialList = new List<SocialMediaDTO>();
            socialList = socialDAO.GetSocialMediaData();
            dto.Facebook = socialList.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialList.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialList.First(x => x.Link.Contains("instagram"));
            dto.YouTube = socialList.First(x => x.Link.Contains("youtube"));
            dto.LinkedIn = socialList.First(x => x.Link.Contains("linkedin"));

            FavDTO favDTO = new FavDTO();
            dto.Fav = favDAO.GetFav();

            List<MetaDTO> metaList = new List<MetaDTO>();
            dto.MetaList = metaDAO.GetMetaData();

            List<AddressDTO> addressList = addressDAO.GetAddresses();
            dto.Address = addressList.First();

            dto.hotNews = postDAO.GetHotNews();

            return dto;
        }
    }
}
