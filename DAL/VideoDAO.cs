using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> GetVideoList()
        {
            List<VideoDTO> videoListDTO = new List<VideoDTO>();
            List<Video> videoList = db.Videos.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

            foreach (var item in videoList)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OrigialVideoPath;
                videoListDTO.Add(dto);
            }

            return videoListDTO;
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == model.ID);
                video.Title = model.Title;
                video.VideoPath = model.VideoPath;
                video.OrigialVideoPath = model.OriginalVideoPath;
                video.LastUpdateUserID = UserStatic.UserID;
                video.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteVideo(int iD)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == iD);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public VideoDTO GetVideoById(int id)
        {
            VideoDTO dto = new VideoDTO();
            Video video = db.Videos.First(x => x.ID == id);

            dto.ID = video.ID;
            dto.Title = video.Title;
            dto.VideoPath = video.VideoPath;
            dto.OriginalVideoPath = video.OrigialVideoPath;

            return dto;
        }
    }
}
